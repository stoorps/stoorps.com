use super::*;
pub fn mesh(
    nx: usize,
    ny: usize,
    nz: usize,
    grid: Vec<Vec3>,
    values: Vec<f64>,
    sdf: impl Fn(Vec3) -> f64,
) -> Result<MeshGL> {
    let index = |x: usize, y: usize, z: usize| x + (nx + 1) * (y + (ny + 1) * z);
    let mut vertices: Vec<Vec3> = vec![];
    let mut sources: Vec<[i32; 2]> = vec![];
    let mut triangles: Vec<u32> = vec![];
    let mut edges = HashMap::new();
    // Trace the surface across cube faces. Shared face decisions give matching
    // contours on adjacent cubes, including ambiguous four-crossing faces.
    // This avoids the dense internal diagonals of six tetrahedra per cube.
    let cube_edges = [
        [0, 1],
        [1, 3],
        [3, 2],
        [2, 0],
        [4, 5],
        [5, 7],
        [7, 6],
        [6, 4],
        [0, 4],
        [1, 5],
        [3, 7],
        [2, 6],
    ];
    let cube_faces = [
        ([0, 1, 3, 2], [0, 1, 2, 3]),
        ([4, 5, 7, 6], [4, 5, 6, 7]),
        ([0, 1, 5, 4], [0, 9, 4, 8]),
        ([2, 3, 7, 6], [2, 10, 6, 11]),
        ([0, 2, 6, 4], [3, 11, 7, 8]),
        ([1, 3, 7, 5], [1, 10, 5, 9]),
    ];
    for z in 0..nz {
        for y in 0..ny {
            for x in 0..nx {
                let cube = [
                    index(x, y, z),
                    index(x + 1, y, z),
                    index(x, y + 1, z),
                    index(x + 1, y + 1, z),
                    index(x, y, z + 1),
                    index(x + 1, y, z + 1),
                    index(x, y + 1, z + 1),
                    index(x + 1, y + 1, z + 1),
                ];
                let mut cuts = [None; 12];
                for (e, [a, b]) in cube_edges.iter().enumerate() {
                    let (a, b) = (cube[*a], cube[*b]);
                    if (values[a] > 0.) == (values[b] > 0.) {
                        continue;
                    }
                    let key = (a.min(b), a.max(b));
                    cuts[e] = Some(*edges.entry(key).or_insert_with(|| {
                        let (mut lo, mut hi) = (0., 1.);
                        for _ in 0..16 {
                            let t = (lo + hi) / 2.;
                            let q = grid[a] + (grid[b] - grid[a]) * t;
                            let v = sdf(q);
                            if (v > 0.) == (values[a] > 0.) {
                                lo = t;
                            } else {
                                hi = t;
                            }
                        }
                        let q = grid[a] + (grid[b] - grid[a]) * ((lo + hi) / 2.);
                        let id = vertices.len() as u32;
                        vertices.push(q);
                        sources.push([a as i32, b as i32]);
                        id
                    }));
                }
                if cuts.iter().all(Option::is_none) {
                    continue;
                }
                let mut links = [
                    Vec::<usize>::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                    Vec::new(),
                ];
                for (corners, es) in cube_faces {
                    let active: Vec<_> =
                        es.iter().copied().filter(|e| cuts[*e].is_some()).collect();
                    let mut connect = |a: usize, b: usize| {
                        links[a].push(b);
                        links[b].push(a);
                    };
                    if active.len() == 2 {
                        connect(active[0], active[1]);
                    }
                    if active.len() == 4 {
                        let q = corners
                            .iter()
                            .fold(Vec3::new(0., 0., 0.), |q, c| q + grid[cube[*c]])
                            * 0.25;
                        let inside = sdf(q) > 0.;
                        for i in 0..4 {
                            if (values[cube[corners[i]]] > 0.) != inside {
                                connect(es[(i + 3) % 4], es[i]);
                            }
                        }
                    }
                }
                let mut visited = [false; 12];
                for first in 0..12 {
                    if cuts[first].is_none() || visited[first] {
                        continue;
                    }
                    let mut ring = vec![];
                    let (mut current, mut previous) = (first, usize::MAX);
                    loop {
                        visited[current] = true;
                        ring.push(cuts[current].unwrap());
                        let next = *links[current]
                            .iter()
                            .find(|&&e| e != previous)
                            .ok_or("Invalid rounded cell contour")?;
                        previous = current;
                        current = next;
                        if current == first {
                            break;
                        }
                        if visited[current] {
                            return Err("Invalid rounded cell contour loop".into());
                        }
                    }
                    let centre = ring
                        .iter()
                        .fold(Vec3::new(0., 0., 0.), |q, &i| q + vertices[i as usize])
                        * (1. / ring.len() as f64);
                    let eps = 1e-4;
                    let field = |q: Vec3| sdf(q);
                    let outward = Vec3::new(
                        field(centre - Vec3::new(eps, 0., 0.))
                            - field(centre + Vec3::new(eps, 0., 0.)),
                        field(centre - Vec3::new(0., eps, 0.))
                            - field(centre + Vec3::new(0., eps, 0.)),
                        field(centre - Vec3::new(0., 0., eps))
                            - field(centre + Vec3::new(0., 0., eps)),
                    );
                    let normal = (0..ring.len()).fold(Vec3::new(0., 0., 0.), |n, j| {
                        n + super::cross(
                            vertices[ring[j] as usize] - centre,
                            vertices[ring[(j + 1) % ring.len()] as usize] - centre,
                        )
                    });
                    if dot(normal, outward) < 0. {
                        ring.reverse();
                    }
                    // Fan from the boundary for triangles/quads; larger contours
                    // get a centre to avoid long thin diagonal triangles.
                    if ring.len() <= 4 {
                        for j in 1..ring.len() - 1 {
                            triangles.extend([ring[0], ring[j], ring[j + 1]]);
                        }
                    } else {
                        let id = vertices.len() as u32;
                        vertices.push(centre);
                        sources.push([-1, -1]);
                        for j in 0..ring.len() {
                            triangles.extend([id, ring[j], ring[(j + 1) % ring.len()]]);
                        }
                    }
                }
            }
        }
    }
    // Gradient direction is ambiguous at fused tube junctions. Propagate
    // orientation through shared edges, retaining the majority outward sign.
    let mut edge_faces = HashMap::new();
    let mut adjacent = vec![vec![]; triangles.len() / 3];
    for (face, t) in triangles.chunks_exact(3).enumerate() {
        for j in 0..3 {
            let a = t[j];
            let b = t[(j + 1) % 3];
            let key = (a.min(b), a.max(b));
            if let Some((other, forward)) = edge_faces.insert(key, (face, a < b)) {
                let same = forward == (a < b);
                adjacent[face].push((other, same));
                adjacent[other].push((face, same));
            }
        }
    }
    let mut orientation = vec![None; adjacent.len()];
    for start in 0..adjacent.len() {
        if orientation[start].is_some() {
            continue;
        }
        orientation[start] = Some(false);
        let mut stack = vec![start];
        let mut component = vec![];
        while let Some(face) = stack.pop() {
            component.push(face);
            let flip = orientation[face].unwrap();
            for &(next, same) in &adjacent[face] {
                let required = flip ^ same;
                if let Some(actual) = orientation[next] {
                    if actual != required {
                        return Err("Non-orientable strand surface".into());
                    }
                } else {
                    orientation[next] = Some(required);
                    stack.push(next);
                }
            }
        }
        let reverse = component
            .iter()
            .filter(|&&i| orientation[i] == Some(true))
            .count()
            > component.len() / 2;
        for face in component {
            if orientation[face].unwrap() ^ reverse {
                triangles.swap(face * 3 + 1, face * 3 + 2);
            }
        }
    }
    Ok(MeshGL {
        num_prop: 5,
        vert_properties: vertices
            .iter()
            .zip(&sources)
            .flat_map(|(q, s)| [q.x as f32, q.y as f32, q.z as f32, s[0] as f32, s[1] as f32])
            .collect(),
        tri_verts: triangles,
        ..Default::default()
    })
}

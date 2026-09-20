use super::*;
use manifold_rust::cross_section::CrossSection;
type Pt = [f64; 2];
type Tri = [usize; 3];
fn cross(a: Pt, b: Pt, c: Pt) -> f64 {
    (b[0] - a[0]) * (c[1] - a[1]) - (b[1] - a[1]) * (c[0] - a[0])
}
fn on(a: Pt, b: Pt, c: Pt) -> bool {
    cross(a, b, c).abs() < 1e-9
        && c[0] >= a[0].min(b[0]) - 1e-9
        && c[0] <= a[0].max(b[0]) + 1e-9
        && c[1] >= a[1].min(b[1]) - 1e-9
        && c[1] <= a[1].max(b[1]) + 1e-9
}
pub fn opening(p: &Params) -> Result<Vec<Pt>> {
    let mut nodes = vec![];
    for i in 0..p["cell_nodes"] as usize {
        let get = |key: &str| p[&format!("cell_{i}_{key}")];
        let (x, y, hx, hy, s) = (get("x"), get("y"), get("hx"), get("hy"), get("smooth"));
        if x.abs() > 1.
            || y.abs() > 1.
            || (s != 0.
                && [
                    (x + hx).abs(),
                    (x - hx).abs(),
                    (y + hy).abs(),
                    (y - hy).abs(),
                ]
                .iter()
                .any(|v| *v > 1.))
        {
            return Err("Keep nodes and curve handles inside the cell boundary.".into());
        }
        nodes.push([
            x,
            y,
            if s != 0. { hx } else { 0. },
            if s != 0. { hy } else { 0. },
        ]);
    }
    let mut pts = vec![];
    for i in 0..nodes.len() {
        let a = nodes[i];
        let b = nodes[(i + 1) % nodes.len()];
        for j in 0..12 {
            let t = j as f64 / 12.;
            let u = 1. - t;
            let mut q = [0.; 2];
            for k in 0..2 {
                q[k] = u * u * u * a[k]
                    + 3. * u * u * t * (a[k] + a[k + 2])
                    + 3. * u * t * t * (b[k] - b[k + 2])
                    + t * t * t * b[k]
            }
            let angle = p["cell_rotation"] * PI / 180.;
            pts.push([
                q[0] * angle.cos() - q[1] * angle.sin(),
                q[0] * angle.sin() + q[1] * angle.cos(),
            ]);
        }
    }
    let mut min = [f64::INFINITY; 2];
    let mut max = [f64::NEG_INFINITY; 2];
    for q in &pts {
        for k in 0..2 {
            min[k] = min[k].min(q[k]);
            max[k] = max[k].max(q[k]);
        }
    }
    let center = [(min[0] + max[0]) / 2., (min[1] + max[1]) / 2.];
    let scale = [(max[0] - min[0]) / 2., (max[1] - min[1]) / 2.];
    if scale.iter().any(|s| *s < 1e-9) {
        return Err("Separate neighbouring cell nodes.".into());
    }
    for q in &mut pts {
        for k in 0..2 {
            q[k] = (q[k] - center[k]) / scale[k];
        }
    }
    for i in 0..pts.len() {
        let (a, b) = (pts[i], pts[(i + 1) % pts.len()]);
        if (a[0] - b[0]).hypot(a[1] - b[1]) < 1e-6 {
            return Err("Separate neighbouring cell nodes.".into());
        }
        for j in i + 2..pts.len() {
            if i == 0 && j == pts.len() - 1 {
                continue;
            }
            let (c, d) = (pts[j], pts[(j + 1) % pts.len()]);
            if (cross(a, b, c) * cross(a, b, d) < 0. && cross(c, d, a) * cross(c, d, b) < 0.)
                || on(a, b, c)
                || on(a, b, d)
                || on(c, d, a)
                || on(c, d, b)
            {
                return Err("Cell edges cannot cross or touch. Move the selected node or shorten its handles.".into());
            }
        }
    }
    let area = pts
        .iter()
        .enumerate()
        .map(|(i, a)| {
            let b = pts[(i + 1) % pts.len()];
            a[0] * b[1] - a[1] * b[0]
        })
        .sum::<f64>()
        / 2.;
    if area.abs() < 0.08 {
        return Err("Make the cell opening larger.".into());
    }
    if area < 0. {
        pts.reverse()
    }
    Ok(pts)
}
fn triangulate(boundary: &[Pt], hole: &[Pt]) -> Result<Vec<Tri>> {
    let points: Vec<_> = boundary.iter().chain(hole).copied().collect();
    let mut chains = vec![];
    let mut corners = |lp: &[Pt], offset: usize| {
        let kept: Vec<_> = (0..lp.len())
            .filter(|&i| {
                cross(
                    lp[(i + lp.len() - 1) % lp.len()],
                    lp[i],
                    lp[(i + 1) % lp.len()],
                )
                .abs()
                    > 1e-10
            })
            .collect();
        for (k, &start) in kept.iter().enumerate() {
            let end = kept[(k + 1) % kept.len()];
            let mut chain = vec![offset + start];
            let mut i = (start + 1) % lp.len();
            while i != end {
                chain.push(offset + i);
                i = (i + 1) % lp.len();
            }
            chain.push(offset + end);
            if chain.len() > 2 {
                chains.push(chain)
            }
        }
        kept.iter().map(|i| offset + i).collect::<Vec<_>>()
    };
    let outer = corners(boundary, 0);
    let inner = corners(hole, boundary.len());
    let ids: Vec<_> = outer.iter().chain(&inner).copied().collect();
    let flat: Vec<_> = ids.iter().flat_map(|&i| points[i]).collect();
    let indices = earcutr::earcut(&flat, &[outer.len()], 2)
        .map_err(|e| format!("Cannot triangulate cell: {e:?}"))?;
    let mut faces: Vec<Tri> = indices
        .chunks_exact(3)
        .map(|t| [ids[t[0]], ids[t[1]], ids[t[2]]])
        .collect();
    for chain in chains {
        let a = chain[0];
        let b = *chain.last().unwrap();
        let mut next = vec![];
        for face in faces {
            let edge = (0..3).find(|&i| {
                (face[i] == a && face[(i + 1) % 3] == b) || (face[i] == b && face[(i + 1) % 3] == a)
            });
            if let Some(edge) = edge {
                let order: Vec<_> = if face[edge] == a {
                    chain.clone()
                } else {
                    chain.iter().rev().copied().collect()
                };
                for q in order.windows(2) {
                    next.push([q[0], q[1], face[(edge + 2) % 3]])
                }
            } else {
                next.push(face)
            }
        }
        faces = next;
    }
    Ok(faces)
}
fn improve(
    points: &[Pt],
    triangles: &mut [Tri],
    width: f64,
    height: f64,
    allowed: impl Fn(usize, usize) -> bool,
) {
    let xy: Vec<Pt> = points
        .iter()
        .map(|p| [p[0] * width, p[1] * height])
        .collect();
    let cot = |a: usize, b: usize, c: usize| {
        let u = [xy[a][0] - xy[c][0], xy[a][1] - xy[c][1]];
        let v = [xy[b][0] - xy[c][0], xy[b][1] - xy[c][1]];
        (u[0] * v[0] + u[1] * v[1]) / (u[0] * v[1] - u[1] * v[0]).abs()
    };
    for _ in 0..100 {
        let mut edges = HashMap::new();
        let mut touched = vec![false; triangles.len()];
        let mut changed = false;
        for i in 0..triangles.len() {
            let face = triangles[i];
            for k in 0..3 {
                let (a, b, c) = (face[k], face[(k + 1) % 3], face[(k + 2) % 3]);
                let key = (a.min(b), a.max(b));
                if let Some(&(d, j)) = edges.get(&key) {
                    if touched[i] || touched[j] {
                        continue;
                    }
                    if cross(xy[c], xy[d], xy[b]) <= 1e-10 || cross(xy[d], xy[c], xy[a]) <= 1e-10 {
                        continue;
                    }
                    if cot(a, b, c) + cot(a, b, d) >= -1e-8 || !allowed(c, d) {
                        continue;
                    }
                    triangles[i] = [c, d, b];
                    triangles[j] = [d, c, a];
                    touched[i] = true;
                    touched[j] = true;
                    changed = true;
                } else {
                    edges.insert(key, (c, i));
                }
            }
        }
        if !changed {
            break;
        }
    }
}
fn midpoint(
    local: &mut Vec<Pt>,
    mids: &mut HashMap<(usize, usize), usize>,
    a: usize,
    b: usize,
) -> usize {
    let key = (a.min(b), a.max(b));
    if let Some(i) = mids.get(&key) {
        return *i;
    }
    let id = local.len();
    local.push([
        (local[a][0] + local[b][0]) / 2.,
        (local[a][1] + local[b][1]) / 2.,
    ]);
    mids.insert(key, id);
    id
}
fn segments(
    a: usize,
    b: usize,
    mids: &HashMap<(usize, usize), usize>,
    out: &mut Vec<(usize, usize)>,
) {
    if let Some(&m) = mids.get(&(a.min(b), a.max(b))) {
        segments(a, m, mids, out);
        segments(m, b, mids, out);
    } else {
        out.push((a, b));
    }
}
pub fn connected(p: &Params) -> Result<(Vec<[f64; 3]>, Vec<Tri>)> {
    let surface = crate::surface::Surface::new(p);
    let (_, mut cols, rows) = surface.counts(p);
    let hole = opening(p)?;
    let height = surface.height / rows as f64;
    let max_radius = p["bottom_diameter"]
        .max(p["middle_diameter"])
        .max(p["top_diameter"])
        / 2.
        + p["ripple_depth"];
    let shear = p["twist"].abs() * PI / 180. * max_radius / p["height"];
    let margin = p["thickness"] * (1. + shear * shear).sqrt();
    let mut width = surface.min_circumference / cols as f64;
    while cols >= 8 && width <= margin + 0.5 {
        cols -= 1;
        width = surface.min_circumference / cols as f64;
    }
    if cols < 8 || height <= p["thickness"] + 0.25 {
        return Err("Reduce strand thickness or twist, or widen the narrowest part of the shade, to leave space for cell openings.".into());
    }
    let size = (width - margin).min((height - p["thickness"]) / p["cell_aspect"]);
    let sx = size / width;
    let sy = size * p["cell_aspect"] / height;
    let h: Vec<Pt> = hole
        .iter()
        .map(|q| [0.5 + q[0] * sx * 0.5, 0.5 + q[1] * sy * 0.5])
        .collect();
    let sub = 2
        .max((p["lobes"] / cols as f64 * 12.).ceil() as usize)
        .max((height / 3.).ceil() as usize);
    let seam = sub.div_ceil(20) * 20;
    let mut boundary = vec![];
    for i in 0..seam {
        boundary.push([i as f64 / seam as f64, 0.])
    }
    for i in 0..sub {
        boundary.push([1., i as f64 / sub as f64])
    }
    for i in 0..seam {
        boundary.push([1. - i as f64 / seam as f64, 1.])
    }
    for i in 0..sub {
        boundary.push([0., 1. - i as f64 / sub as f64])
    }
    let mut triangles = triangulate(&boundary, &h)?;
    let mut local: Vec<_> = boundary.iter().chain(&h).copied().collect();
    improve(&local, &mut triangles, width, height, |_, _| true);
    let range = (p["middle_diameter"] - p["bottom_diameter"])
        .abs()
        .max((p["top_diameter"] - p["middle_diameter"]).abs());
    let slope = range / p["height"] * (PI / 2.);
    let curvature = p["curve"] * range * PI * PI / p["height"].powi(2);
    let needs = |a: Pt, b: Pt| {
        let dx = a[0] - b[0];
        let dz = (a[1] - b[1]) * height;
        let angular = (TAU * dx / cols as f64 + p["twist"] * PI / 180. * dz / p["height"]).abs();
        let phase = (p["lobes"] * TAU * dx / cols as f64).abs();
        let radial = slope * dz.abs() + p["ripple_depth"] * phase;
        let (ya, yb) = ((a[1] - 0.5) * height, (b[1] - 0.5) * height);
        let kink = if rows % 2 == 1 && ya * yb < 0. {
            (1. - p["curve"])
                * (p["top_diameter"] - 2. * p["middle_diameter"] + p["bottom_diameter"]).abs()
                / p["height"]
                * (ya * yb).abs()
                / (ya.abs() + yb.abs())
        } else {
            0.
        };
        let error = kink
            + (curvature * dz * dz
                + p["ripple_depth"] * phase * phase
                + 2. * angular * radial
                + (max_radius + p["thickness"] / 2.) * angular * angular)
                / 8.;
        error > 0.02
            || dz.abs() > 6.
            || phase > PI / 3.
            // Arc-length remapping adds a 0.05 mm chord limit. Using the
            // tighter 0.02 mm bound globally over-refines every panel to the
            // sharpest ripple at the narrowest height.
            || dx.abs() * surface.circumference / cols as f64 > surface.horizontal_step * 2.5f64.sqrt()
    };
    let mut mids = HashMap::new();
    for _ in 0..10 {
        let mut changed = false;
        let mut next = vec![];
        for [a, b, c] in triangles {
            let (ab, bc, ca) = (
                needs(local[a], local[b]),
                needs(local[b], local[c]),
                needs(local[c], local[a]),
            );
            if !ab && !bc && !ca {
                next.push([a, b, c]);
                continue;
            }
            changed = true;
            let x = if ab {
                midpoint(&mut local, &mut mids, a, b)
            } else {
                0
            };
            let y = if bc {
                midpoint(&mut local, &mut mids, b, c)
            } else {
                0
            };
            let z = if ca {
                midpoint(&mut local, &mut mids, c, a)
            } else {
                0
            };
            if ab && bc && ca {
                next.extend([[a, x, z], [x, b, y], [z, y, c], [x, y, z]])
            } else if ab && bc {
                next.extend([[x, b, y], [a, x, c], [x, y, c]])
            } else if bc && ca {
                next.extend([[y, c, z], [b, y, a], [y, z, a]])
            } else if ca && ab {
                next.extend([[z, a, x], [c, z, b], [z, x, b]])
            } else if ab {
                next.extend([[a, x, c], [x, b, c]])
            } else if bc {
                next.extend([[b, y, a], [y, c, a]])
            } else {
                next.extend([[c, z, b], [z, a, b]])
            }
        }
        triangles = next;
        if !changed {
            break;
        }
    }
    improve(&local, &mut triangles, width, height, |a, b| {
        !needs(local[a], local[b])
    });
    let mut hole_edges = vec![];
    for i in 0..h.len() {
        segments(
            boundary.len() + i,
            boundary.len() + (i + 1) % h.len(),
            &mids,
            &mut hole_edges,
        )
    }
    let mut rim_edges = vec![];
    for i in 0..boundary.len() {
        segments(i, (i + 1) % boundary.len(), &mids, &mut rim_edges)
    }
    let mut vertices = vec![];
    let mut faces = vec![];
    let mut ids = HashMap::new();
    for row in 0..rows {
        for col in 0..cols {
            let mut indices = [vec![], vec![]];
            for side in 0..2 {
                for q in &local {
                    let u = (col as f64 + q[0] + p["cell_offset"] * (row % 2) as f64)
                        .rem_euclid(cols as f64);
                    let v = row as f64 + q[1];
                    let key = ((u * 1e8).round() as i64, (v * 1e8).round() as i64, side);
                    let id = *ids.entry(key).or_insert_with(|| {
                        let z = surface.z(v / rows as f64);
                        let pt = surface.point(
                            p,
                            u / cols as f64,
                            z,
                            if side == 1 {
                                p["thickness"] / 2.
                            } else {
                                -p["thickness"] / 2.
                            },
                        );
                        let id = vertices.len();
                        vertices.push(pt);
                        id
                    });
                    indices[side].push(id);
                }
            }
            for &[a, b, c] in &triangles {
                faces.extend([
                    [indices[1][a], indices[1][b], indices[1][c]],
                    [indices[0][a], indices[0][c], indices[0][b]],
                ]);
            }
            for &(a, b) in &hole_edges {
                faces.extend([
                    [indices[0][a], indices[1][a], indices[1][b]],
                    [indices[0][a], indices[1][b], indices[0][b]],
                ]);
            }
            for &(i, j) in &rim_edges {
                let (a, b) = (local[i], local[j]);
                if (row == 0 && a[1] == 0. && b[1] == 0.)
                    || (row == rows - 1 && a[1] == 1. && b[1] == 1.)
                {
                    faces.extend([
                        [indices[0][i], indices[0][j], indices[1][j]],
                        [indices[0][i], indices[1][j], indices[1][i]],
                    ]);
                }
            }
        }
    }
    Ok((vertices, faces))
}
// Sample a union of circular tubes in the flat repeat. A spatial index keeps
// distance evaluation local even for dense patterns and long shades.
fn rounded_strip(p: &Params, width: f64, height: f64, rows: usize, limit: f64) -> Result<MeshGL> {
    let r = p["thickness"] / 2.;
    let total = height * rows as f64;
    let points: Vec<Pt> = opening(p)?
        .iter()
        .map(|q| {
            [
                q[0] * width * p["cell_scale"] / 2.,
                q[1] * height * p["cell_scale"] / 2.,
            ]
        })
        .collect();
    let bin = p["thickness"];
    let nx = (width / bin).ceil() as usize + 1;
    let ny = (total / bin).ceil() as usize + 1;
    let mut bins = vec![Vec::<(Pt, Pt)>::new(); nx * ny];
    let reach = (p["cell_scale"] / 2. + r / width + 2.).ceil() as i32;
    for col in -reach..=reach {
        for row in 0..rows {
            let x = (col as f64 + 0.5 + p["cell_offset"] * (row % 2) as f64) * width;
            let y = (row as f64 + 0.5) * height;
            for i in 0..points.len() {
                let a = [points[i][0] + x, points[i][1] + y];
                let b = [
                    points[(i + 1) % points.len()][0] + x,
                    points[(i + 1) % points.len()][1] + y,
                ];
                let xmin = a[0].min(b[0]) - r;
                let xmax = a[0].max(b[0]) + r;
                let ymin = a[1].min(b[1]) - r;
                let ymax = a[1].max(b[1]) + r;
                if xmax < 0. || xmin > width || ymax < 0. || ymin > total {
                    continue;
                }
                for iy in
                    ((ymin.max(0.) / bin) as usize)..=((ymax.max(0.) / bin) as usize).min(ny - 1)
                {
                    for ix in ((xmin.max(0.) / bin) as usize)
                        ..=((xmax.max(0.) / bin) as usize).min(nx - 1)
                    {
                        bins[iy * nx + ix].push((a, b));
                    }
                }
            }
        }
    }
    let sdf = |mut q: Vec3| {
        q.x = q.x.rem_euclid(width);
        let ix = ((q.x.max(0.) / bin) as usize).min(nx - 1);
        let iy = ((q.y.max(0.) / bin) as usize).min(ny - 1);
        let mut d2 = (r + bin).powi(2);
        for &(a, b) in &bins[iy * nx + ix] {
            let dx = b[0] - a[0];
            let dy = b[1] - a[1];
            let t = (((q.x - a[0]) * dx + (q.y - a[1]) * dy) / (dx * dx + dy * dy).max(1e-20))
                .clamp(0., 1.);
            d2 = d2.min((q.x - a[0] - t * dx).powi(2) + (q.y - a[1] - t * dy).powi(2));
        }
        r - (d2 + (q.z - r).powi(2)).sqrt()
    };
    // Use the same tetrahedral grid on both sides of the periodic seam.
    // Shared edge samples then weld exactly, without 3D Boolean strip joins.
    let edge = (r * 1.2).min(limit).min(1.2);
    let nx = (width / edge).ceil() as usize;
    let inner_y = (total / edge).ceil() as usize;
    let ny = inner_y + 2;
    let nz = 6.max((2.5 * r / edge).ceil() as usize);
    let dx = width / nx as f64;
    let dy = total / inner_y as f64;
    let dz = 2.5 * r / nz as f64;
    let index = |x: usize, y: usize, z: usize| x + (nx + 1) * (y + (ny + 1) * z);
    let mut grid = vec![];
    let mut values = vec![];
    for z in 0..=nz {
        for y in 0..=ny {
            for x in 0..=nx {
                let q = Vec3::new(x as f64 * dx, (y as f64 - 1.) * dy, -r / 4. + z as f64 * dz);
                let v = sdf(q).min(q.y).min(total - q.y);
                grid.push(q);
                values.push(if v.abs() < 1e-9 { -1e-9 } else { v });
            }
        }
    }
    let mut vertices: Vec<Vec3> = vec![];
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
                            let v = sdf(q).min(q.y).min(total - q.y);
                            if (v > 0.) == (values[a] > 0.) {
                                lo = t;
                            } else {
                                hi = t;
                            }
                        }
                        let q = grid[a] + (grid[b] - grid[a]) * ((lo + hi) / 2.);
                        let id = vertices.len() as u32;
                        vertices.push(q);
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
                        let inside = sdf(q).min(q.y).min(total - q.y) > 0.;
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
                    let field = |q: Vec3| sdf(q).min(q.y).min(total - q.y);
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
                        for j in 0..ring.len() {
                            triangles.extend([id, ring[j], ring[(j + 1) % ring.len()]]);
                        }
                    }
                }
            }
        }
    }
    Ok(MeshGL {
        num_prop: 3,
        vert_properties: vertices
            .iter()
            .flat_map(|q| [q.x as f32, q.y as f32, q.z as f32])
            .collect(),
        tri_verts: triangles,
        ..Default::default()
    })
}

pub fn strands(p: &Params) -> Result<Vec<Manifold>> {
    let surface = crate::surface::Surface::new(p);
    let (_, cols, rows) = surface.counts(p);
    let width = surface.circumference / cols as f64;
    let height = surface.height / rows as f64;
    let contour = CrossSection::new(vec![opening(p)?
        .iter()
        .map(|q| {
            Vec2::new(
                q[0] * width * p["cell_scale"] / 2.,
                q[1] * height * p["cell_scale"] / 2.,
            )
        })
        .collect()]);
    let outer = contour.offset_with_params(p["thickness"] / 2., 1, 2., 12);
    let material = if p["cell_cut_inside"] != 0. {
        let inner = contour.offset_with_params(-p["thickness"] / 2., 1, 2., 12);
        if inner.is_empty() {
            return Err("The strand thickness fills the cell centre. Reduce thickness or increase cell size.".into());
        }
        outer.difference(&inner)
    } else {
        outer
    };
    // Fuse neighbouring repeats in the flat pattern first, then cut out one
    // periodic column. Overlaps are resolved once in 2D instead of between
    // hundreds of thousands of curved triangles after wrapping.
    let reach = (p["cell_scale"] / 2. + p["thickness"] / width + 2.).ceil() as i32;
    let mut repeats = vec![];
    for col in -reach..=reach {
        for row in 0..rows {
            repeats.push(material.translate(Vec2::new(
                (col as f64 + 0.5 + p["cell_offset"] * (row % 2) as f64) * width,
                (row as f64 + 0.5) * height,
            )));
        }
    }
    let pattern = CrossSection::batch_boolean(&repeats, OpType::Add);
    // Shared periodic seam faces are removed and their vertices welded below.
    let clip = CrossSection::square_vec2(Vec2::new(width, surface.height), false);
    let clipped = pattern.intersection(&clip);
    let flat = Manifold::extrude(
        &clipped.to_polygons(),
        p["thickness"],
        0,
        0.,
        Vec2::new(1., 1.),
    );
    let length = 3f64.min(width / 4.).min(surface.horizontal_step);
    // Keep angular detail, but do not impose that spacing on extrusion depth
    // or the much gentler vertical profile.
    let diameter_change = (p["middle_diameter"] - p["bottom_diameter"])
        .abs()
        .max((p["middle_diameter"] - p["top_diameter"]).abs());
    let slope = diameter_change * PI / (2. * p["height"]);
    let curvature = p["curve"] * diameter_change * PI * PI / p["height"].powi(2);
    let twist_rate = p["twist"].abs() * PI / (180. * p["height"]);
    let max_radius = p["bottom_diameter"]
        .max(p["middle_diameter"])
        .max(p["top_diameter"])
        / 2.
        + p["ripple_depth"]
        + p["thickness"] / 2.;
    let vertical_curvature = curvature + 2. * twist_rate * slope + max_radius * twist_rate.powi(2);
    let vertical_limit = 3f64.min((8. * 0.02 / vertical_curvature.max(1e-12)).sqrt());
    let vertical = length / vertical_limit;
    let depth = length / p["thickness"].max(length);
    let rounded = p["cell_rounded"] != 0. && p["cell_cut_inside"] != 0.;
    let mut template = if rounded {
        rounded_strip(
            p,
            width,
            height,
            rows,
            surface.horizontal_step * 2.5f64.sqrt(),
        )?
    } else {
        flat.scale(Vec3::new(1., vertical, depth))
            .refine_to_length(length)
            .get_mesh_gl(-1)
    };
    if !rounded {
        for q in template.vert_properties.chunks_exact_mut(3) {
            q[1] /= vertical as f32;
            q[2] /= depth as f32;
        }
    }
    let mut coords: Vec<[f64; 3]> = template
        .vert_properties
        .chunks_exact(3)
        .map(|q| [q[0] as f64, q[1] as f64, q[2] as f64])
        .collect();
    let on_seam = |x: f64| x.abs() < 1e-5 || (x - width).abs() < 1e-5;
    // Triangulation/refinement can put different samples on opposite cuts.
    // Split boundary edges at the union of both sample sets before welding.
    let samples: Vec<[f64; 3]> = coords.iter().copied().filter(|q| on_seam(q[0])).collect();
    let mut triangles = vec![];
    for t in template.tri_verts.chunks_exact(3) {
        let t = [t[0] as usize, t[1] as usize, t[2] as usize];
        if rounded {
            triangles.push(t);
            continue;
        }
        let xs = [coords[t[0]][0], coords[t[1]][0], coords[t[2]][0]];
        if xs.iter().all(|x| on_seam(*x)) && xs.iter().all(|x| (*x - xs[0]).abs() < 1e-5) {
            continue;
        }
        let edge = (0..3).find(|i| on_seam(xs[*i]) && (xs[*i] - xs[(*i + 1) % 3]).abs() < 1e-4);
        if let Some(i) = edge {
            let a = t[i];
            let b = t[(i + 1) % 3];
            let c = t[(i + 2) % 3];
            let qa = coords[a];
            let qb = coords[b];
            let dy = qb[1] - qa[1];
            let dz = qb[2] - qa[2];
            let len = dy * dy + dz * dz;
            let mut points: Vec<(f64, [f64; 3])> = samples
                .iter()
                .filter_map(|q| {
                    let u = ((q[1] - qa[1]) * dy + (q[2] - qa[2]) * dz) / len;
                    if u > 1e-6
                        && u < 1. - 1e-6
                        && ((q[1] - qa[1]) * dz - (q[2] - qa[2]) * dy).abs() < 1e-4 * len.sqrt()
                    {
                        Some((u, [qa[0], q[1], q[2]]))
                    } else {
                        None
                    }
                })
                .collect();
            points.sort_by(|a, b| a.0.total_cmp(&b.0));
            points.dedup_by(|a, b| (a.0 - b.0).abs() * len.sqrt() < 1e-4);
            let mut prev = a;
            for (_, q) in points {
                let id = coords.len();
                coords.push(q);
                triangles.push([prev, id, c]);
                prev = id;
            }
            triangles.push([prev, b, c]);
        } else {
            triangles.push(t);
        }
    }
    let mut vertices = vec![];
    let mut faces = vec![];
    let mut ids = HashMap::new();
    for col in 0..cols {
        let mut indices = vec![];
        for (local_id, q) in coords.iter().enumerate() {
            let x = if q[0].abs() < 1e-5 {
                0.
            } else if (q[0] - width).abs() < 1e-5 {
                width
            } else {
                q[0]
            };
            let u = (col as f64 + x / width).rem_euclid(cols as f64);
            let scale = if rounded { 1e8 } else { 1e4 };
            let key = if rounded && !on_seam(x) {
                (-(col as i64) - 1, local_id as i64, -1)
            } else {
                (
                    (u * 1e6).round() as i64,
                    (q[1] * scale).round() as i64,
                    (q[2] * scale).round() as i64,
                )
            };
            let id = *ids.entry(key).or_insert_with(|| {
                let z = surface.z(q[1] / surface.height);
                let id = vertices.len();
                vertices.push(surface.point(p, u / cols as f64, z, q[2] - p["thickness"] / 2.));
                id
            });
            indices.push(id);
        }
        for t in &triangles {
            let face = [indices[t[0]], indices[t[1]], indices[t[2]]];
            if face[0] != face[1] && face[1] != face[2] && face[2] != face[0] {
                faces.push(face);
            }
        }
    }
    let mesh = raw(&vertices, &faces);
    if mesh.status() != Error::NoError {
        return Err(format!(
            "The cell repeat seams could not be joined ({:?}). Adjust cell size or row offset.",
            mesh.status()
        ));
    }
    Ok(vec![mesh])
}

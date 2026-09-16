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
    let scale = (max[0] - min[0]).max(max[1] - min[1]) / 2.;
    if scale < 1e-9 {
        return Err("Separate neighbouring cell nodes.".into());
    }
    for q in &mut pts {
        for k in 0..2 {
            q[k] = (q[k] - center[k]) / scale;
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
fn layout(p: &Params) -> (f64, usize, usize) {
    let spacing = (32. - 26. * p["density"] / 100.).max(2. * p["thickness"] + 2.);
    let diameter = (p["bottom_diameter"] + 2. * p["middle_diameter"] + p["top_diameter"]) / 4.;
    (
        spacing,
        8.max((PI * diameter / spacing).round() as usize),
        3.max((p["height"] / spacing).round() as usize),
    )
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
    let (spacing, cells, layout_rows) = layout(p);
    let mut cols = cells;
    let rows = 3
        .max((layout_rows as f64 / p["cell_aspect"]).round() as usize)
        .min((p["height"] / (p["thickness"] + 0.5)).floor() as usize);
    let hole = opening(p)?;
    let min_radius = p["bottom_diameter"]
        .min(p["middle_diameter"])
        .min(p["top_diameter"])
        / 2.
        - p["ripple_depth"];
    cols = cols.min(8.max((TAU * min_radius / spacing).round() as usize));
    let height = p["height"] / rows as f64;
    let max_radius = p["bottom_diameter"]
        .max(p["middle_diameter"])
        .max(p["top_diameter"])
        / 2.
        + p["ripple_depth"];
    let shear = p["twist"].abs() * PI / 180. * max_radius / p["height"];
    let margin = p["thickness"] * (1. + shear * shear).sqrt();
    let mut width = TAU * min_radius / cols as f64;
    while cols >= 8 && width <= margin + 0.5 {
        cols -= 1;
        width = TAU * min_radius / cols as f64;
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
        error > 0.02 || dz.abs() > 6. || phase > PI / 3.
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
                        let z = v / rows as f64 * p["height"];
                        let a = u / cols as f64 * TAU + p["twist"] * PI / 180. * z / p["height"];
                        let r = radius(p, a, z)
                            + if side == 1 {
                                p["thickness"] / 2.
                            } else {
                                -p["thickness"] / 2.
                            };
                        let id = vertices.len();
                        vertices.push([r * a.cos(), r * a.sin(), z]);
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
pub fn strands(p: &Params) -> Result<Vec<Manifold>> {
    let (spacing, _, layout_rows) = layout(p);
    let r = p["bottom_diameter"]
        .min(p["middle_diameter"])
        .min(p["top_diameter"])
        / 2.
        - p["ripple_depth"];
    let cols = 8.max((TAU * r / spacing).round() as usize);
    let rows = 3.max((layout_rows as f64 / p["cell_aspect"]).round() as usize);
    let width = TAU * r / cols as f64;
    let height = p["height"] / rows as f64;
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
    let repeats: Vec<_> = (0..rows)
        .map(|row| {
            material.translate(Vec2::new(
                (0.5 + p["cell_offset"] * (row % 2) as f64) * width,
                (row as f64 + 0.5) * height,
            ))
        })
        .collect();
    let column = CrossSection::batch_boolean(&repeats, OpType::Add);
    let clip = CrossSection::square_vec2(Vec2::new(width * 8., p["height"]), false)
        .translate(Vec2::new(-width * 3., 0.));
    let clipped = column.intersection(&clip);
    let flat = Manifold::extrude(
        &clipped.to_polygons(),
        p["thickness"],
        0,
        0.,
        Vec2::new(1., 1.),
    );
    let length = 3f64.min(width / 4.).min(if p["ripple_depth"] > 0. {
        width * cols as f64 / p["lobes"].max(1.) / 12.
    } else {
        3.
    });
    let template = flat.refine_to_length(length).get_mesh_gl(-1);
    let mut pieces = vec![];
    for col in 0..cols {
        let mut mesh = template.clone();
        for q in mesh
            .vert_properties
            .chunks_exact_mut(mesh.num_prop as usize)
        {
            let (x, z, depth) = (q[0] as f64, q[1] as f64, q[2] as f64);
            let a = TAU * (col as f64 + x / width) / cols as f64
                + p["twist"] * PI / 180. * z / p["height"];
            let r = radius(p, a, z) + depth - p["thickness"] / 2.;
            q[0] = (r * a.cos()) as f32;
            q[1] = (r * a.sin()) as f32;
            q[2] = z as f32;
        }
        pieces.push(Manifold::from_mesh_gl(&mesh));
    }
    Ok(pieces)
}

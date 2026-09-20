//! Periodic wave paths swept with circular sections in physical 3D space.
use super::*;
fn wave(p: &Params, t: f64) -> f64 {
    let t = t.rem_euclid(1.);
    if p["wave_arch"] == 1. {
        return (PI * t).sin();
    }
    let count = p["wave_nodes"] as usize;
    for i in 0..count {
        let j = (i + 1) % count;
        let x = p[&format!("wave_{i}_x")];
        let end = if j == 0 {
            1.
        } else {
            p[&format!("wave_{j}_x")]
        };
        if t < x || t > end {
            continue;
        }
        let u = (t - x) / (end - x);
        let y = p[&format!("wave_{i}_y")];
        let b = p[&format!("wave_{j}_y")];
        let m = p[&format!("wave_{i}_slope")];
        let n = p[&format!("wave_{j}_slope")]
            * if j == 0 && p["wave_arch"] == 2. {
                -1.
            } else {
                1.
            };
        return (2. * u.powi(3) - 3. * u * u + 1.) * y
            + (u.powi(3) - 2. * u * u + u) * m * (end - x)
            + (-2. * u.powi(3) + 3. * u * u) * b
            + (u.powi(3) - u * u) * n * (end - x);
    }
    0.
}
fn unit(v: Vec3) -> Vec3 {
    v * (1. / dot(v, v).sqrt().max(1e-12))
}
pub fn build(p: &Params) -> Result<Manifold> {
    let count = p["wave_nodes"] as usize;
    if p["wave_0_x"] != 0. {
        return Err("The first wave point must start at X = 0.".into());
    }
    for i in 1..count {
        if p[&format!("wave_{i}_x")] - p[&format!("wave_{}_x", i - 1)] < 0.02 {
            return Err("Keep wave points ordered from left to right, at least 0.02 apart.".into());
        }
    }
    let surface = surface::Surface::new(p);
    let amplitude = p["wave_height"] / 2.;
    let r = p["thickness"] / 2.;
    let samples: Vec<f64> = (0..1024).map(|i| wave(p, i as f64 / 1024.)).collect();
    let low = samples.iter().copied().fold(f64::INFINITY, f64::min);
    let high = samples.iter().copied().fold(f64::NEG_INFINITY, f64::max);
    let span = high - low;
    if span < 0.01 {
        return Err("Give the wave some height so neighbouring layers can join.".into());
    }
    // Normalize the edited wave to a predictable peak-to-peak measurement.
    let shape = |t: f64| (wave(p, t) - low) / span * 2. - 1.;
    let available = p["height"] - 2. * amplitude - 2. * r;
    if available < 1. {
        return Err("Reduce wave height to leave room for strand layers.".into());
    }
    let delta = (0..1024)
        .map(|i| {
            let t = i as f64 / 1024.;
            amplitude * (shape(t) - shape(t + 0.5))
        })
        .fold(0f64, f64::max);
    let d = p["thickness"];
    let joint = (p["extrusion_width"] / d).max(0.3).min(0.95);
    let min_merge = (100. * (1. - (1. - joint * joint).sqrt())).ceil();
    let min_spacing = delta.max((p["wave_height"] + d + 0.1) / 2.);
    let max_merge = (100. * (1. - (min_spacing - delta) / d)).floor().min(100.);
    let max_spacing = delta + d * (1. - min_merge / 100.);
    let min_intervals = (available / max_spacing).ceil().max(1.) as usize;
    let max_intervals = (available / min_spacing).floor() as usize;
    if max_merge < min_merge || max_intervals < min_intervals {
        return Err(
            "This wave cannot join safely at this shade height. Adjust wave height or shape."
                .into(),
        );
    }
    let merge = p["wave_merge"].clamp(min_merge, max_merge);
    let target = delta + d * (1. - merge / 100.);
    let intervals = ((available / target).round() as usize).clamp(min_intervals, max_intervals);
    let layers = intervals + 1;
    let spacing = available / intervals as f64;
    let peaks = (surface.max_circumference / p["wave_width"])
        .round()
        .max(3.) as usize;
    let sides = ((PI / (1. - 0.04 / r).clamp(-1., 1.).acos()).ceil() as usize).max(12);
    let arch = p["wave_arch"] != 0.;
    let periodic = !arch
        && (p["bottom_diameter"] - p["middle_diameter"]).abs() < 1e-9
        && (p["top_diameter"] - p["middle_diameter"]).abs() < 1e-9
        && layers > 32;
    let period = 2. * spacing;
    let buffer = ((amplitude + r + 0.1) / spacing).ceil() as usize + 2;
    let first = r + amplitude + buffer as f64 * spacing;
    let repeats = if periodic {
        ((p["height"] - amplitude - r - buffer as f64 * spacing - first) / period)
            .floor()
            .max(0.) as usize
    } else {
        0
    };
    let last = first + repeats as f64 * period;
    let periodic = periodic && repeats > 2;
    let mut ropes = vec![];
    let mut labels = vec![];
    let mut templates: Vec<Manifold> = vec![];
    let mut paths: Vec<Vec<Vec3>> = vec![];
    for layer in 0..layers {
        let centre = r + amplitude + layer as f64 * spacing;
        if periodic
            && centre - amplitude - r > first + period + 0.02
            && centre + amplitude + r < last - 0.02
        {
            continue;
        }
        if periodic && layer >= 2 {
            let dz = (layer - layer % 2) as f64 * spacing;
            ropes.push(
                templates[layer % 2]
                    .rotate(0., 0., p["twist"] * dz / p["height"])
                    .translate(Vec3::new(0., 0., dz)),
            );
            labels.push(layer);
            continue;
        }
        let centre = r + amplitude + layer as f64 * spacing;
        let phase = (layer % 2) as f64 * 0.5;
        let point = |t: f64| {
            let z = centre + amplitude * shape(t * peaks as f64 + phase);
            let q = surface.point(p, t, z, 0.);
            Vec3::new(q[0], q[1], q[2])
        };
        // Subdivide by chord error and turning detail, sharing one closed seam.
        fn segment(f: &impl Fn(f64) -> Vec3, a: f64, b: f64, depth: usize, out: &mut Vec<f64>) {
            let pa = f(a);
            let pb = f(b);
            let mid = (a + b) / 2.;
            let d = f(mid) - (pa + pb) * 0.5;
            if depth < 8 && (dot(d, d) > 0.035f64.powi(2) || dot(pb - pa, pb - pa) > 9.) {
                segment(f, a, mid, depth + 1, out);
                segment(f, mid, b, depth + 1, out);
            } else {
                out.push(a);
            }
        }
        let mut times = vec![];
        for i in 0..peaks * 16 {
            segment(
                &point,
                i as f64 / (peaks * 16) as f64,
                (i + 1) as f64 / (peaks * 16) as f64,
                0,
                &mut times,
            );
        }
        if arch {
            paths.push(times.iter().map(|&t| point(t)).collect());
            continue;
        }
        for i in 0..times.len() {
            let a = point(times[(i + times.len() - 1) % times.len()]);
            let b = point(times[i]);
            let c = point(times[(i + 1) % times.len()]);
            let ab = b - a;
            let bc = c - b;
            let ac = c - a;
            let area = cross(ab, bc);
            let curvature = 2. * dot(area, area).sqrt()
                / (dot(ab, ab) * dot(bc, bc) * dot(ac, ac)).sqrt().max(1e-12);
            if curvature * r > 0.9 {
                return Err("The strand is too thick for these tight bends. Increase wave width, or reduce wave height, ripple depth/count, or strand diameter.".into());
            }
        }
        if periodic {
            paths.push(times.iter().map(|&t| point(t)).collect());
        }
        let mut vertices = vec![];
        let mut faces = vec![];
        for &t in &times {
            let c = point(t);
            let tangent = unit(point(t + 1e-5) - point(t - 1e-5));
            let radial = unit(Vec3::new(c.x, c.y, 0.));
            let normal = unit(radial - tangent * dot(radial, tangent));
            let binormal = cross(tangent, normal);
            for j in 0..sides {
                let a = TAU * j as f64 / sides as f64;
                let q = c + (normal * a.cos() + binormal * a.sin()) * r;
                vertices.push([q.x, q.y, q.z]);
            }
        }
        for i in 0..times.len() {
            for j in 0..sides {
                let a = i * sides + j;
                let b = i * sides + (j + 1) % sides;
                let c = ((i + 1) % times.len()) * sides + (j + 1) % sides;
                let d = ((i + 1) % times.len()) * sides + j;
                faces.extend([[a, b, c], [a, c, d]]);
            }
        }
        let rope = raw(&vertices, &faces);
        if rope.status() != Error::NoError {
            return Err("The wave could not form a closed strand.".into());
        }
        if rope.has_self_intersections() {
            return Err("A strand folds into itself. Reduce ripple depth/count or wave peaks/height, or use a thinner strand.".into());
        }
        if periodic {
            templates.push(rope.clone());
        }
        ropes.push(rope);
        labels.push(layer);
    }
    if arch {
        return dense_periodic_wall(
            p,
            &surface,
            &paths,
            -r,
            p["height"] + 2. * r,
            1,
            spacing,
            r,
            true,
        );
    }
    fn fuse(mut pieces: Vec<Manifold>) -> Manifold {
        while pieces.len() > 1 {
            pieces = pieces
                .chunks(2)
                .map(|p| {
                    if p.len() == 2 {
                        p[0].union(&p[1])
                    } else {
                        p[0].clone()
                    }
                })
                .collect();
        }
        pieces.pop().unwrap()
    }
    if periodic {
        let extent = p["bottom_diameter"] + 2. * p["ripple_depth"] + 20.;
        let clip = |lo: f64, hi: f64| {
            Manifold::cube(Vec3::new(extent, extent, hi - lo), false).translate(Vec3::new(
                -extent / 2.,
                -extent / 2.,
                lo,
            ))
        };
        let mut bottom = vec![];
        let mut top = vec![];
        for (layer, rope) in labels.into_iter().zip(ropes) {
            let centre = r + amplitude + layer as f64 * spacing;
            if centre - amplitude - r <= first + period + 0.02 {
                bottom.push(rope.clone());
            }
            if centre + amplitude + r >= last - 0.02 {
                top.push(rope);
            }
        }
        let bottom = fuse(bottom);
        let body = dense_periodic_wall(
            p, &surface, &paths, first, period, repeats, spacing, r, false,
        )?;
        let end = fuse(top).intersection(&clip(last - 0.05, p["height"] + r));
        Ok(fuse(vec![
            bottom.intersection(&clip(-r, first + 0.05)),
            body,
            end,
        ]))
    } else {
        Ok(fuse(ropes))
    }
}

// Mesh one periodic union of the circular strand paths. A spatial index keeps
// signed-distance queries local; shared grid edges weld exact periodic cuts.
// Only constant profiles repeat under an axial translation and twist rotation.
fn dense_periodic_wall(
    p: &Params,
    surface: &surface::Surface,
    paths: &[Vec<Vec3>],
    first: f64,
    period: f64,
    repeats: usize,
    spacing: f64,
    r: f64,
    whole: bool,
) -> Result<Manifold> {
    let tw = p["twist"] * PI / 180. / p["height"];
    let mut segments = vec![];
    let cell = p["thickness"];
    let mut bins = HashMap::<(i32, i32, i32), Vec<usize>>::new();
    let bin = |v: Vec3| {
        (
            (v.x / cell).floor() as i32,
            (v.y / cell).floor() as i32,
            (v.z / cell).floor() as i32,
        )
    };
    let layer_count = if whole {
        paths.len()
    } else {
        ((first + period + p["wave_height"] + 2. * r) / spacing).ceil() as usize
    };
    for layer in 0..layer_count {
        let dz = if whole {
            0.
        } else {
            (layer - layer % 2) as f64 * spacing
        };
        let a = tw * dz;
        let points: Vec<_> = paths[if whole { layer } else { layer % 2 }]
            .iter()
            .map(|q| {
                Vec3::new(
                    q.x * a.cos() - q.y * a.sin(),
                    q.x * a.sin() + q.y * a.cos(),
                    q.z + dz,
                )
            })
            .collect();
        for j in 0..points.len() {
            let a = points[j];
            let b = points[(j + 1) % points.len()];
            if a.z.min(b.z) > first + period + r || a.z.max(b.z) < first - r {
                continue;
            }
            let id = segments.len();
            segments.push((a, b));
            let lo = bin(Vec3::new(
                a.x.min(b.x) - r,
                a.y.min(b.y) - r,
                a.z.min(b.z) - r,
            ));
            let hi = bin(Vec3::new(
                a.x.max(b.x) + r,
                a.y.max(b.y) + r,
                a.z.max(b.z) + r,
            ));
            for x in lo.0..=hi.0 {
                for y in lo.1..=hi.1 {
                    for z in lo.2..=hi.2 {
                        bins.entry((x, y, z)).or_default().push(id);
                    }
                }
            }
        }
    }
    let band = p["thickness"]
        * (2. + p["ripple_depth"] * p["lobes"] / (p["bottom_diameter"] / 2. - p["ripple_depth"]));
    let columns = (surface.circumference
        / surface
            .horizontal_step
            .min(
                surface.circumference
                    / (surface.max_circumference / p["wave_width"])
                        .round()
                        .max(3.)
                    / 16.,
            )
            .min(1.))
    .ceil() as usize;
    let rows = (period / (r * 0.5)).ceil().max(4.) as usize;
    let depths = (2. * band / (r * 0.5)).ceil() as usize;
    let field = |q: Vec3| {
        if q.y < 0. || q.y > period {
            return -1000.;
        }
        let world = surface.point(
            p,
            q.x.rem_euclid(surface.circumference) / surface.circumference,
            first + q.y.rem_euclid(period),
            q.z,
        );
        let v = Vec3::new(world[0], world[1], world[2]);
        let mut d2 = (2. * r).powi(2);
        if let Some(ids) = bins.get(&bin(v)) {
            for &i in ids {
                let (a, b) = segments[i];
                let ab = b - a;
                let t = (dot(v - a, ab) / dot(ab, ab)).clamp(0., 1.);
                let d = v - a - ab * t;
                d2 = d2.min(dot(d, d));
            }
        }
        r - d2.sqrt()
    };
    let mut grid = vec![];
    let mut values = vec![];
    for z in 0..=depths {
        for y in 0..=rows + 2 {
            for x in 0..=columns {
                let q = Vec3::new(
                    surface.circumference * x as f64 / columns as f64,
                    period * (y as f64 - 1.) / rows as f64,
                    -band + 2. * band * z as f64 / depths as f64,
                );
                let value = field(q);
                grid.push(q);
                values.push(if value.abs() < 1e-9 { -1e-9 } else { value });
            }
        }
    }
    let template = crate::iso::mesh(columns, rows + 2, depths, grid, values, field)?;
    let mut vertices = vec![];
    let mut faces = vec![];
    let mut weld = HashMap::new();
    let decode = |n: i32| {
        let n = n as usize;
        let x = n % (columns + 1);
        let y = (n / (columns + 1)) % (rows + 3);
        let z = n / ((columns + 1) * (rows + 3));
        (x, y, z)
    };
    let coords: Vec<_> = template
        .vert_properties
        .chunks_exact(5)
        .map(|q| {
            let mut point = [q[0] as f64, q[1] as f64, q[2] as f64];
            let a = q[3] as i32;
            let b = q[4] as i32;
            let mut cap = None;
            let mut seam = false;
            if a >= 0 {
                let aa = decode(a);
                let bb = decode(b);
                if (aa.0 == 0 && bb.0 == 0) || (aa.0 == columns && bb.0 == columns) {
                    seam = true;
                    point[0] = 0.;
                }
                if aa.1 <= 1 && bb.1 <= 1 {
                    cap = Some(0);
                    point[1] = 0.;
                } else if aa.1 >= rows + 1 && bb.1 >= rows + 1 {
                    cap = Some(rows);
                    point[1] = period;
                }
            } else {
                if point[1].abs() < 1e-4 {
                    cap = Some(0);
                    point[1] = 0.;
                } else if (point[1] - period).abs() < 1e-4 {
                    cap = Some(rows);
                    point[1] = period;
                }
            }
            (point, a, b, cap, seam)
        })
        .collect();
    for repeat in 0..repeats {
        let mut ids = vec![];
        for &(q, a, b, cap, seam) in &coords {
            let y = q[1] + repeat as f64 * period;
            let make = |vertices: &mut Vec<[f64; 3]>| {
                let id = vertices.len();
                vertices.push(surface.point(p, q[0] / surface.circumference, first + y, q[2]));
                id
            };
            let id = if a >= 0 && (seam || cap.is_some()) {
                let code = |index: i32| {
                    let (x, grid_y, z) = decode(index);
                    let yy = cap.unwrap_or(grid_y.saturating_sub(1)) + repeat * rows;
                    ((z * (rows * repeats + 1) + yy) * columns + x % columns) as i64
                };
                let aa = code(a);
                let bb = code(b);
                let key = (aa.min(bb), aa.max(bb));
                *weld.entry(key).or_insert_with(|| make(&mut vertices))
            } else {
                make(&mut vertices)
            };
            ids.push(id);
        }
        for t in template.tri_verts.chunks_exact(3) {
            if (repeat > 0 && t.iter().all(|&i| coords[i as usize].3 == Some(0)))
                || (repeat + 1 < repeats && t.iter().all(|&i| coords[i as usize].3 == Some(rows)))
            {
                continue;
            }
            let t = [ids[t[0] as usize], ids[t[1] as usize], ids[t[2] as usize]];
            if t[0] != t[1] && t[1] != t[2] && t[2] != t[0] {
                faces.push(t);
            }
        }
    }
    let result = raw(&vertices, &faces);
    if result.status() != Error::NoError {
        return Err("The dense strand surface could not be joined.".into());
    }
    Ok(result)
}

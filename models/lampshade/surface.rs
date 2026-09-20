//! Equal-distance coordinates on the shade's centre surface.
use super::*;
const NZ: usize = 128;
const NA: usize = 1024;
pub struct Surface {
    arcs: Vec<Vec<f64>>,
    vertical: Vec<f64>,
    pub circumference: f64,
    pub min_circumference: f64,
    pub max_circumference: f64,
    pub height: f64,
    pub horizontal_step: f64,
    axial_height: f64,
}
impl Surface {
    pub fn new(p: &Params) -> Self {
        let mut arcs = vec![];
        let mut max_curvature = 0f64;
        let mut lengths = vec![];
        for j in 0..=NZ {
            let r = profile(p, j as f64 / NZ as f64);
            let mut arc = vec![0.];
            for i in 0..NA {
                let a = TAU * (i as f64 + 0.5) / NA as f64;
                let phase = p["lobes"] * a;
                let radius = r + p["ripple_depth"] * phase.sin();
                let slope = p["ripple_depth"] * p["lobes"] * phase.cos();
                let second = -p["ripple_depth"] * p["lobes"].powi(2) * phase.sin();
                // Parallel inner skin has greater curvature on convex bends.
                let skin_radius = radius - p["thickness"] / 2.;
                let k = (skin_radius * skin_radius + 2. * slope * slope - skin_radius * second)
                    .abs()
                    / (skin_radius * skin_radius + slope * slope).powf(1.5);
                max_curvature = max_curvature.max(k);
                arc.push(arc[i] + radius.hypot(slope) * TAU / NA as f64);
            }
            let length = arc[NA];
            for v in &mut arc {
                *v /= length;
            }
            arcs.push(arc);
            lengths.push(length);
        }
        let circumference =
            (lengths.iter().sum::<f64>() - (lengths[0] + lengths[NZ]) / 2.) / NZ as f64;
        let min_circumference = lengths.iter().copied().fold(f64::INFINITY, f64::min);
        let max_circumference = lengths.iter().copied().fold(0f64, f64::max);
        let horizontal_step =
            (8. * 0.02 / max_curvature.max(1e-12)).sqrt() * circumference / max_circumference;
        let mut s = Self {
            arcs,
            vertical: vec![0.],
            circumference,
            min_circumference,
            max_circumference,
            height: 0.,
            horizontal_step,
            axial_height: p["height"],
        };
        for j in 1..=NZ {
            let z0 = p["height"] * (j - 1) as f64 / NZ as f64;
            let z1 = p["height"] * j as f64 / NZ as f64;
            let distance = (0..64)
                .map(|i| {
                    let a = s.point(p, i as f64 / 64., z0, 0.);
                    let b = s.point(p, i as f64 / 64., z1, 0.);
                    ((a[0] - b[0]).powi(2) + (a[1] - b[1]).powi(2) + (a[2] - b[2]).powi(2)).sqrt()
                })
                .sum::<f64>()
                / 64.;
            s.vertical.push(s.vertical[j - 1] + distance);
        }
        s.height = s.vertical[NZ];
        s
    }
    pub fn angle(&self, u: f64, z: f64) -> f64 {
        let t = (z / self.axial_height * NZ as f64).clamp(0., NZ as f64);
        let j = (t.floor() as usize).min(NZ - 1);
        let f = t - j as f64;
        let u = u.rem_euclid(1.);
        let at = |i: usize| self.arcs[j][i] * (1. - f) + self.arcs[j + 1][i] * f;
        let (mut lo, mut hi) = (0, NA);
        while hi - lo > 1 {
            let m = (lo + hi) / 2;
            if at(m) < u {
                lo = m
            } else {
                hi = m
            }
        }
        TAU * (lo as f64 + (u - at(lo)) / (at(hi) - at(lo))) / NA as f64
    }
    pub fn z(&self, v: f64) -> f64 {
        let target = v.clamp(0., 1.) * self.height;
        let hi = self.vertical.partition_point(|x| *x < target).clamp(1, NZ);
        let lo = hi - 1;
        self.axial_height
            * (lo as f64 + (target - self.vertical[lo]) / (self.vertical[hi] - self.vertical[lo]))
            / NZ as f64
    }
    pub fn point(&self, p: &Params, u: f64, z: f64, depth: f64) -> [f64; 3] {
        let a = self.angle(u, z) + p["twist"] * PI / 180. * z / p["height"];
        let r = radius(p, a, z) + depth;
        [r * a.cos(), r * a.sin(), z]
    }
    pub fn counts(&self, p: &Params) -> (f64, usize, usize) {
        let requested = if p.get("cell_pitch").copied().unwrap_or(0.) > 0. {
            p["cell_pitch"]
        } else {
            32. - 26. * p["density"] / 100.
        };
        let pitch = requested.max(2. * p["thickness"] + 2.);
        (
            pitch,
            8.max((self.circumference / pitch).round() as usize),
            3.max((self.height / (pitch * p["cell_aspect"])).round() as usize),
        )
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    #[test]
    fn ripple_adds_cells_and_equalises_surface_distance() {
        let mut p = defaults();
        for k in ["bottom_diameter", "middle_diameter", "top_diameter"] {
            p.insert(k.into(), 160.);
        }
        p.insert("height".into(), 245.);
        p.insert("lobes".into(), 4.);
        p.insert("ripple_depth".into(), 35.);
        p.insert("twist".into(), 45.);
        let s = Surface::new(&p);
        let (_, cols, _) = s.counts(&p);
        assert_eq!(cols, 70);
        assert!((s.circumference - 785.397).abs() < 0.01);
        for col in 0..cols {
            let mut length = 0.;
            for j in 0..64 {
                let a = s.point(&p, (col as f64 + j as f64 / 64.) / cols as f64, 120., 0.);
                let b = s.point(
                    &p,
                    (col as f64 + (j + 1) as f64 / 64.) / cols as f64,
                    120.,
                    0.,
                );
                length += ((a[0] - b[0]).powi(2) + (a[1] - b[1]).powi(2)).sqrt();
            }
            assert!((length - s.circumference / cols as f64).abs() < 0.01);
        }
        p.insert("ripple_depth".into(), 0.);
        assert_eq!(Surface::new(&p).counts(&p).1, 45);
    }
}

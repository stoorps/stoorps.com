fn main() -> Result<(), Box<dyn std::error::Error>> {
    let mut p = models_spike::bilresa::BilresaParameters::default();
    let args: Vec<_> = std::env::args().skip(1).collect();
    if args.len() == 2 {
        p.num_switches_left = args[0].parse()?;
        p.num_switches_right = args[1].parse()?;
    }
    let solids = models_spike::bilresa::build(&p)?;
    for (i, s) in solids.iter().enumerate() {
        println!("{i} volume {} bounds {:?}", s.volume(), s.bounding_box());
        cadrum::Solid::write_step(
            [s],
            &mut std::fs::File::create(format!("artifacts/bilresa-{i}.step"))?,
        )?;
        let mesh = cadrum::Solid::mesh(
            [s],
            cadrum::Tessellation {
                deflection_linear: 0.01,
                deflection_angular: 0.15,
                relative_linear: false,
            },
        )?;
        mesh.write_stl(&mut std::fs::File::create(format!(
            "artifacts/bilresa-{i}.stl"
        ))?)?;
    }
    Ok(())
}

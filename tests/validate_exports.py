"""Independent native OCCT STEP reader and trimesh STL validation of WASM exports."""
import json
import math
from pathlib import Path
import trimesh
from OCP.STEPControl import STEPControl_Reader
from OCP.IFSelect import IFSelect_RetDone
from OCP.BRepCheck import BRepCheck_Analyzer
from OCP.TopAbs import TopAbs_SOLID, TopAbs_IN, TopAbs_OUT
from OCP.TopExp import TopExp_Explorer
from OCP.BRepGProp import BRepGProp
from OCP.GProp import GProp_GProps
from OCP.BRepClass3d import BRepClass3d_SolidClassifier
from OCP.gp import gp_Pnt

reports = []
for path in sorted(Path('artifacts').glob('box-*.step')):
    w, length, h, d = map(float, path.stem.removeprefix('box-').split('x'))
    expected = (w * length - math.pi * d*d / 4) * h
    reader = STEPControl_Reader()
    assert reader.ReadFile(str(path)) == IFSelect_RetDone
    assert reader.TransferRoots() == 1
    shape = reader.OneShape()
    assert BRepCheck_Analyzer(shape).IsValid(), f'{path}: invalid BRep'
    explorer = TopExp_Explorer(shape, TopAbs_SOLID)
    solids = []
    while explorer.More():
        solids.append(explorer.Current())
        explorer.Next()
    assert len(solids) == 1
    solid = solids[0]
    props = GProp_GProps()
    BRepGProp.VolumeProperties_s(solid, props)
    assert abs(props.Mass() - expected) < 1e-5
    # Probe the hole at bottom, centre, and top, plus material outside the hole.
    for z in [0.001, h/2, h-0.001]:
        assert BRepClass3d_SolidClassifier(solid, gp_Pnt(w/2,length/2,z),1e-7).State() == TopAbs_OUT
        assert BRepClass3d_SolidClassifier(solid, gp_Pnt(0.1,0.1,z),1e-7).State() == TopAbs_IN
    mesh = trimesh.load(path.with_suffix('.stl'), force='mesh')
    assert mesh.is_watertight and mesh.is_winding_consistent and mesh.is_volume
    assert len(mesh.split()) == 1
    # A closed box with one tunnel has genus one: Euler characteristic zero.
    assert mesh.euler_number == 0
    assert abs(mesh.volume - expected) / expected < 0.005
    for actual, wanted in zip(mesh.bounds.flatten(), [0,0,0,w,length,h]):
        assert abs(actual-wanted) < 1e-4
    reports.append(dict(file=path.name, valid_brep=True, solid_count=1,
                        step_volume=props.Mass(), through_hole_probes=3,
                        stl_watertight=True, stl_volume=mesh.volume,
                        stl_euler_number=mesh.euler_number))
assert len(reports) == 5
Path('artifacts/independent-validation.json').write_text(json.dumps(reports, indent=2))
print(json.dumps(reports, indent=2))

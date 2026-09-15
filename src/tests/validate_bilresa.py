"""Independent CAD/mesh checks for the BILRESA WASM exports."""
import json
from pathlib import Path
from itertools import combinations
import numpy as np
import trimesh
from OCP.STEPControl import STEPControl_Reader
from OCP.IFSelect import IFSelect_RetDone
from OCP.BRepCheck import BRepCheck_Analyzer
from OCP.TopAbs import TopAbs_SOLID
from OCP.TopExp import TopExp_Explorer
from OCP.BRepGProp import BRepGProp
from OCP.GProp import GProp_GProps
from OCP.BRepAlgoAPI import BRepAlgoAPI_Common

def volume(shape):
    props = GProp_GProps()
    BRepGProp.VolumeProperties_s(shape, props)
    return props.Mass()

def read(path, count):
    reader = STEPControl_Reader()
    assert reader.ReadFile(str(path)) == IFSelect_RetDone
    reader.TransferRoots()
    shape = reader.OneShape()
    assert BRepCheck_Analyzer(shape).IsValid(), str(path)
    explorer = TopExp_Explorer(shape, TopAbs_SOLID)
    solids = []
    while explorer.More():
        solids.append(explorer.Current())
        explorer.Next()
    assert len(solids) == count, str(path)
    return solids

reports = []
for case in json.loads(Path('artifacts/bilresa/wasm-validation.json').read_text()):
    prefix = f"{case['left']}-{case['right']}"
    solids = []
    for i, expected in enumerate(case['parts']):
        base = Path(f'artifacts/bilresa/{prefix}-{i}')
        solid = read(base.with_suffix('.step'), 1)[0]
        assert abs(volume(solid)-expected['volume']) < 1e-4
        solids.append(solid)
        mesh = trimesh.load(base.with_suffix('.stl'), force='mesh')
        assert mesh.is_volume and mesh.is_watertight and mesh.is_winding_consistent
        assert len(mesh.split()) == 1
        assert abs(mesh.volume-expected['volume'])/expected['volume'] < .005
    read(Path(f'artifacts/bilresa/{prefix}-assembly.step'), 3)
    overlap = [abs(volume(BRepAlgoAPI_Common(a,b).Shape())) for a,b in combinations(solids,2)]
    assert max(overlap) < 1e-6, (prefix, overlap)
    reports.append(dict(counts=[case['left'],case['right']], valid_solids=3, closed_meshes=3, overlap_mm3=overlap))
comparison = []
for i, name in enumerate(['Part 1','Blanking Plate','Cover Top']):
    source = Path(f'models/bilresa/reference/one-each-{i}.stl')
    reference = trimesh.load(source, force='mesh')
    generated = trimesh.load(f'artifacts/bilresa/1-1-{i}.stl', force='mesh')
    distances = []
    for a,b in [(reference,generated),(generated,reference)]:
        points = trimesh.sample.sample_surface(a, 2000, seed=42)[0]
        distances.extend(trimesh.proximity.closest_point(b, points)[1])
    comparison.append(dict(part=name, reference_file=str(source), volume_difference_percent=100*(generated.volume/reference.volume-1), sampled_surface_max_mm=float(np.max(distances)), sampled_surface_rms_mm=float(np.sqrt(np.mean(np.square(distances))))))
result = dict(cases=reports, reference_comparison=comparison)
Path('artifacts/bilresa/independent-validation.json').write_text(json.dumps(result,indent=2))
print(json.dumps(result,indent=2))

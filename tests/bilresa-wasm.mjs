import {readFile,writeFile,mkdir} from 'node:fs/promises';
import init,{Bilresa,inspect_step} from '../pkg/models_spike.js';
import {assert,close} from './geometry.mjs';
await init({module_or_path:await readFile(new URL('../pkg/models_spike_bg.wasm',import.meta.url))});
await mkdir('artifacts/bilresa',{recursive:true});
const reports=[];
for(const [left,right] of [[1,1],[2,0],[0,2],[0,0],[3,1],[1,3],[8,0]]){
 const start=performance.now(),model=new Bilresa(left,right),parts=[];
 for(let i=0;i<3;i++){
  const p=model.part(i),step=p.step(),stl=p.stl();
  const parsed=JSON.parse(inspect_step(step));assert(parsed.solids.length===1,'One solid per part');close(parsed.solids[0].volume,p.volume(),1e-4,'STEP roundtrip');
  await writeFile(`artifacts/bilresa/${left}-${right}-${i}.step`,step);await writeFile(`artifacts/bilresa/${left}-${right}-${i}.stl`,stl);
  parts.push({index:i,volume:p.volume(),triangles:p.indices().length/3});p.free();
 }
 const combined=model.step();assert(JSON.parse(inspect_step(combined)).solids.length===3,'Three solids in assembly STEP');
 await writeFile(`artifacts/bilresa/${left}-${right}-assembly.step`,combined);
 reports.push({left,right,parts,milliseconds:performance.now()-start});model.free();
}
for(const args of [[-1,1],[1.5,1],[9,0],[NaN,1]]){
 let rejected=false;try{new Bilresa(...args);}catch{rejected=true;}assert(rejected,'Reject invalid counts');
}
await writeFile('artifacts/bilresa/wasm-validation.json',JSON.stringify(reports,null,2));console.log(JSON.stringify(reports,null,2));

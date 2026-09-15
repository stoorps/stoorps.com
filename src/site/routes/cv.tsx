import { createFileRoute, Link } from "@tanstack/react-router";
import { useEffect, useState } from "react";
import { roles, skillGroups, projects } from "../content/cv";
import "../cv.css";
export const Route = createFileRoute("/cv")({
  head: () => ({ meta: [{ title: "Sam Storer — Software, systems & things that move" }, { name: "robots", content: "noindex, nofollow" }] }),
  component: CV,
});
const chapters = [["intro", "Hello"], ["experience", "Experience"], ["expertise", "Expertise"], ["outside", "Outside work"], ["next", "Say hello"]];
function CV() {
  const [chapter, setChapter] = useState("intro");
  const [selectedRole, setSelectedRole] = useState(0);
  const [group, setGroup] = useState<keyof typeof skillGroups>("Languages");
  const [sort, setSort] = useState(false);
  useEffect(() => {
    const observer = new IntersectionObserver(entries => {
      for (const entry of entries) if (entry.isIntersecting) setChapter(entry.target.id);
    }, { rootMargin: "-20% 0px -55% 0px", threshold: 0 });
    document.querySelectorAll(".cv-chapter").forEach(section => observer.observe(section));
    return () => observer.disconnect();
  }, []);
  const role = roles[selectedRole];
  const skills = [...skillGroups[group]];
  if (sort) skills.sort((a,b) => b[1]-a[1]);
  return <main id="main" className="cv-page">
    <nav className="cv-nav" aria-label="CV chapters">
      <Link to="/" className="cv-back">← Back</Link>
      <div>{chapters.map(([id,label]) => <a key={id} href={`#${id}`} aria-current={chapter === id ? "location" : undefined}>{label}</a>)}</div>
      <span className="cv-nav-name">Sam Storer</span>
    </nav>
    <section id="intro" className="cv-chapter cv-intro">
      <div className="cv-intro-copy">
        <p className="cv-kicker">Sam Storer <span> / Birmingham, UK</span></p>
        <h1>Software.<br/>Systems.<br/><em>Things that move.</em></h1>
        <p className="cv-lead">Hey, I’m Sam. A senior software engineer who likes getting the whole thing working—from the interface to the hardware underneath.</p>
        <a href="#experience" className="cv-text-link">Take a look around <span>↓</span></a>
      </div>
      <div className="cv-schematic" aria-label="Working across desktop, embedded, mobile and web software">
        <div className="cv-orbit orbit-one"/><div className="cv-orbit orbit-two"/>
        <div className="cv-core"><span>stoorps</span><small>Software engineer</small></div>
        <span className="cv-node node-one">Desktop</span><span className="cv-node node-two">Embedded</span><span className="cv-node node-three">Mobile</span><span className="cv-node node-four">Web & cloud</span>
        <p>Interface → architecture → integration → delivery</p>
      </div>
      <div className="cv-intro-bottom"><span>Building since 2012</span><span>C# · Rust · A curiosity for how things work</span></div>
    </section>
    <section id="experience" className="cv-chapter">
      <div className="cv-section-heading"><p className="cv-kicker">01 / The work</p><h2>Different industries.<br/><em>Connected thinking.</em></h2><p>Retail, robotics, automation and engineering. I’m drawn to greenfield builds and modernisation: making complex systems easier to use, maintain and build on.</p></div>
      <div className="cv-career">
        <div className="cv-role-list" aria-label="Choose a role">{roles.map((item,i) => <button key={item.company} aria-pressed={selectedRole===i} aria-controls="cv-role-detail" onClick={()=>setSelectedRole(i)}><span className="cv-role-index">0{roles.length-i}</span><span><strong>{item.company}</strong><small>{item.dates}</small></span><span aria-hidden="true">↗</span></button>)}</div>
        <article id="cv-role-detail" className="cv-role-detail" key={role.company} aria-live="polite">
          <p className="cv-kicker">{role.role} · {role.dates}</p><h3>{role.theme}</h3>
          <ul>{role.points.map(point=><li key={point}>{point}</li>)}</ul>
          <div className="cv-tags">{role.tags.map(tag=><span key={tag}>{tag}</span>)}</div>
        </article>
      </div>
    </section>
    <section id="expertise" className="cv-chapter">
      <div className="cv-section-heading"><p className="cv-kicker">02 / The toolkit</p><h2>A few familiar tools.<br/><em>Always room for more.</em></h2><p>My roots are in desktop and engineering software, with more recent work across the web and cloud. These bars show my relative familiarity, as illustrated in my CV.</p></div>
      <div className="cv-skills-toolbar"><div aria-label="Expertise categories">{Object.keys(skillGroups).map(key=><button key={key} aria-pressed={group===key} onClick={()=>setGroup(key as keyof typeof skillGroups)}>{key}</button>)}</div><button className="cv-sort" aria-pressed={sort} onClick={()=>setSort(!sort)}>↓ Strongest first</button></div>
      <div className="cv-skills" key={`${group}-${sort}`}>{skills.map(([name,value],i)=><div className="cv-skill" key={name} style={{animationDelay:`${i*25}ms`}}><div><span>{name}</span><span className="cv-skill-level">{value>=85?"Deep experience":value>=65?"Comfortable": "Working knowledge"}</span></div><meter min="0" max="100" value={value} aria-label={`${name}: relative familiarity, approximately ${value} out of 100`}/></div>)}</div>
      <p className="cv-skill-note">Relative, self-assessed familiarity—not a certification or a test score. Bar lengths are approximated from the original CV.</p>
    </section>
    <section id="outside" className="cv-chapter">
      <div className="cv-section-heading"><p className="cv-kicker">03 / Still making things</p><h2>Curiosity doesn’t<br/><em>clock off.</em></h2><p>Music, Linux, physical objects and the occasional pub. Side projects give me room to follow an idea all the way through.</p></div>
      <div className="cv-projects">{projects.map((project,i)=><article key={project.name}><span className="cv-project-number">0{i+1}</span><p className="cv-kicker">{project.category}</p><h3>{project.name}</h3><p>{project.text}</p>{project.href && <a className="cv-text-link" href={project.href}>{project.href==="/"?"Explore the designs":"View on GitHub"} ↗</a>}</article>)}</div>
    </section>
    <section id="next" className="cv-chapter cv-next">
      <div><p className="cv-kicker">04 / Foundations & what’s next</p><h2>Keep learning.<br/><em>Keep building.</em></h2><p className="cv-lead">I enjoy shaping systems, mentoring developers and helping small teams deliver—even when time and resources are tight.</p>
        <a className="cv-contact" href="mailto:samstorer94@pm.me">Let’s talk <span>↗</span></a>
        <div className="cv-contact-links"><a href="https://github.com/stoorps">GitHub ↗</a><a href="https://mastodon.social/@stoorps" rel="me">Mastodon ↗</a><Link to="/">Back to the workbench →</Link></div>
      </div>
      <aside className="cv-education"><h3>The foundations</h3><dl><dt>BTEC Level 4 · Distinction</dt><dd>Professional Competence for IT, Software, Web & Telecom Professionals</dd><dt>BTEC Level 4 · Distinction</dt><dd>Music Technology</dd><dt>Cisco CCNA</dt><dd>Networking</dd></dl><details><summary>School qualifications</summary><p>GCSEs: English B; English Literature C; Music B; Mathematics C; Biology C; Chemistry B; Physics B; Resistant Materials C.</p></details></aside>
    </section>
  </main>;
}

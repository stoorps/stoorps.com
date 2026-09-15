import test from 'node:test';
import assert from 'node:assert/strict';
import { BoxGeometry } from 'three';
import { renderPreview } from '../tools/render-preview.mjs';

test('outline renderer includes silhouettes and hidden edges without invalid coordinates', () => {
  const geometry = new BoxGeometry(40, 30, 20);
  const parts = [{ positions: geometry.attributes.position.array, indices: geometry.index.array }];
  const svg = renderPreview(parts, 'A < B & "C"', 300, 200);
  assert.match(svg, /A &lt; B &amp; &quot;C&quot;/);
  assert.match(svg, /opacity="0.35" d="M/);
  assert.match(svg, /opacity=".95" d="M/);
  assert.doesNotMatch(svg, /NaN|Infinity/);
  assert.equal(svg, renderPreview(parts, 'A < B & "C"', 300, 200));
  geometry.dispose();
});

test('empty model fails rather than producing a blank preview', () => {
  assert.throws(() => renderPreview([], 'Empty'), /empty model/);
});

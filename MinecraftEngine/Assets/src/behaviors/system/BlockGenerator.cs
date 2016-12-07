using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BlockGenerator : MonoBehaviour {

	private World world;
	
	public GameObject defaultMesh;

	void Start() {
		world = World.get();
		Chunk chunk = world.attainChunk(new WorldPos(0, 0, 0));
		
		UnityEngine.Random.InitState(12345);	
		
		for (int x = chunk.getStartX(); x < chunk.getEndX(); x++) {
			for (int y = chunk.getStartY(); y < chunk.getEndY(); y++) {
				for (int z = chunk.getStartZ(); z < chunk.getEndZ(); z++) {
					WorldPos pos = new WorldPos(x, y, z);
					int r = UnityEngine.Random.Range(0, 9);
					if (r > 5) {
						Chunk.attainBlock(BlockType.AIR, pos);
					} else {
						Chunk.attainBlock(BlockType.GRANITE, pos);
					}
				}
			}
		}
	}

	public void doGenerateNextSet() {
		Debug.Log("doGenerateNextSet");
		try {
			generateChunkMesh(0, 0, 0);
		} catch (Exception e) {
			Debug.LogError(e);
		}
	}

	private void generateChunkMesh(int xChunk, int yChunk, int zChunk) {		
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		
		Chunk chunk = World.get().attainChunk(new WorldPos(xChunk, yChunk, zChunk));
		
		for (int x = chunk.getStartX(); x < chunk.getEndX(); x++) {
			for (int y = chunk.getStartY(); y < chunk.getEndY(); y++) {
				for (int z = chunk.getStartZ(); z < chunk.getEndZ(); z++) {
					renderBlock(vertices, triangles, chunk, x, y, z);
				}
			}
		}
		
		GameObject chunkGameObject = UnityEngine.Object.Instantiate(defaultMesh);
		chunkGameObject.setPosition(chunk.getStartX(), chunk.getStartY(), chunk.getStartZ());
		
		MeshFilter meshFilter = chunkGameObject.GetComponent<MeshFilter>();
		Mesh mesh = new Mesh(); // meshFilter.mesh;
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		//mesh.normals = normals.toArray();
		mesh.RecalculateNormals(); // FIXME: do normals by hand?  Should save some generation time
		meshFilter.mesh = mesh;
	}

	public Block getAdjacentBlock(BlockFace blockFace, Chunk chunk, Block block) {
		if (blockFace.isTop()) {
			return Chunk.attainBlock(new WorldPos(block.getX(), block.getY() + 1, block.getZ()));
		} else if (blockFace.isBottom()) {
			return Chunk.attainBlock(new WorldPos(block.getX(), block.getY() - 1, block.getZ()));
		} else if (blockFace.isFront()) {
			return Chunk.attainBlock(new WorldPos(block.getX(), block.getY(), block.getZ() + 1));
		} else if (blockFace.isBack()) {
			return Chunk.attainBlock(new WorldPos(block.getX(), block.getY(), block.getZ() - 1));
		} else if (blockFace.isLeft()) {
			return Chunk.attainBlock(new WorldPos(block.getX() - 1, block.getY(), block.getZ()));
		} else { // if (blockFace.isRight()) {
			return Chunk.attainBlock(new WorldPos(block.getX() + 1, block.getY(), block.getZ()));
		}
	}

	public void renderBlock(List<Vector3> vertices, List<int> triangles, Chunk chunk, int x, int y, int z) {
		Block block = Chunk.attainBlock(BlockType.AIR, new WorldPos(x, y, z));
		if (!block.isVisible()) {
			return;
		}
	
		int vertexIndex;
		
		if (renderFace(BlockFace.TOP, chunk, block)) { // Top
			vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y + 1, z));
			vertices.Add(new Vector3(x, y + 1, z + 1));
			vertices.Add(new Vector3(x + 1, y + 1, z + 1));
			vertices.Add(new Vector3(x + 1, y + 1, z));

			triangles.Add(vertexIndex + 0);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 2);

			triangles.Add(vertexIndex + 2);
			triangles.Add(vertexIndex + 3);
			triangles.Add(vertexIndex + 0);
		}
		
		if (renderFace(BlockFace.BOT, chunk, block)) { // Bottom
			vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z + 1));
			vertices.Add(new Vector3(x + 1, y, z + 1));
			vertices.Add(new Vector3(x + 1, y, z));

			triangles.Add(vertexIndex + 0);
			triangles.Add(vertexIndex + 3);
			triangles.Add(vertexIndex + 2);

			triangles.Add(vertexIndex + 2);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 0);
		}
		
		if (renderFace(BlockFace.BAK, chunk, block)) { // Back
			vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y + 1, z));
			vertices.Add(new Vector3(x + 1, y + 1, z));
			vertices.Add(new Vector3(x + 1, y, z));

			triangles.Add(vertexIndex + 0);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 2);

			triangles.Add(vertexIndex + 2);
			triangles.Add(vertexIndex + 3);
			triangles.Add(vertexIndex + 0);
		}
		
		if (renderFace(BlockFace.FRT, chunk, block)) { // Front
			vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z + 1));
			vertices.Add(new Vector3(x, y + 1, z + 1));
			vertices.Add(new Vector3(x + 1, y + 1, z + 1));
			vertices.Add(new Vector3(x + 1, y, z + 1));

			triangles.Add(vertexIndex + 0);
			triangles.Add(vertexIndex + 3);
			triangles.Add(vertexIndex + 2);

			triangles.Add(vertexIndex + 2);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 0);
		}
		
		if (renderFace(BlockFace.LFT, chunk, block)) { // Left
			vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z + 1));
			vertices.Add(new Vector3(x, y + 1, z + 1));
			vertices.Add(new Vector3(x, y + 1, z));

			triangles.Add(vertexIndex + 0);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 2);

			triangles.Add(vertexIndex + 2);
			triangles.Add(vertexIndex + 3);
			triangles.Add(vertexIndex + 0);
		}
		
		if (renderFace(BlockFace.RHT, chunk, block)) { // Right
			vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x + 1, y, z));
			vertices.Add(new Vector3(x + 1, y, z + 1));
			vertices.Add(new Vector3(x + 1, y + 1, z + 1));
			vertices.Add(new Vector3(x + 1, y + 1, z));

			triangles.Add(vertexIndex + 0);
			triangles.Add(vertexIndex + 3);
			triangles.Add(vertexIndex + 2);

			triangles.Add(vertexIndex + 2);
			triangles.Add(vertexIndex + 1);
			triangles.Add(vertexIndex + 0);
		}
	}

	public bool renderFace(BlockFace blockFace, Chunk chunk, Block block) {

		try { // don't skip faces touching non-visible blocks
			Block adjacent = getAdjacentBlock(blockFace, chunk, block);
			if (!adjacent.isVisible()) {
				return true;
			}
		} catch (Exception e) {
			Debug.LogException(e);
		}
		
		int x = block.getX();
		int y = block.getY();
		int z = block.getZ();
		

		{ // ensure outside edges until we can lookup adjacent chunks // FIXME: lookup adjacent chunk
			if (blockFace.isTop() && y != (chunk.getEndY() - 1)) {
				return false;
			}
			if (blockFace.isBottom() && y != chunk.getStartY()) {
				return false;
			}
			if (blockFace.isFront() && z != (chunk.getEndZ() - 1)) {
				return false;
			}
			if (blockFace.isBack() && z != chunk.getStartZ()) {
				return false;
			}
			if (blockFace.isLeft() && x != chunk.getStartX()) {
				return false;
			}
			
			if (blockFace.isRight() && x != (chunk.getEndX() - 1)) {
				return false;
			}
		}
		return true;
	}
}

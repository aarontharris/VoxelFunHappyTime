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
		
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-32, 1, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-32, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-31, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-17, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-16, 1, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-16, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-15, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-7, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-5, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-3, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(-1, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(0, 1, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(2, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(4, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(6, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(8, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(10, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(12, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(14, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(16, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(16, 1, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(18, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(20, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(22, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(24, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(26, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(28, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(30, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(32, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(32, 1, 0));
		
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(48, 0, 0));
		Chunk.attainBlock(BlockType.GRANITE, new WorldPos(48, 1, 0));
	}

	private int lastSet = -Chunk.CHUNK_SIZE_X * 4;

	public void doGenerateNextSet() {
		try {
			lastSet += Chunk.CHUNK_SIZE_X;
			Debug.LogFormat("doGenerateNextSet {0}", lastSet);
			generateChunkMesh(new WorldPos(lastSet, 0, 0));
		} catch (Exception e) {
			Debug.LogError(e);
		}
	}

	private void generateChunkMesh(WorldPos pos) {
		List<Vector3> vertices = new List<Vector3>();
		List<int> triangles = new List<int>();
		
		Chunk chunk = World.get().attainChunk(pos);
		Debug.LogFormat("Generating Chunk {0}, {1}, {2}", chunk.getPos().x, chunk.getPos().y, chunk.getPos().z);
		
		for (int x = chunk.getStartX(); x < chunk.getEndX(); x++) {
			for (int y = chunk.getStartY(); y < chunk.getEndY(); y++) {
				for (int z = chunk.getStartZ(); z < chunk.getEndZ(); z++) {
					renderBlock(vertices, triangles, chunk, new WorldPos(x, y, z));
				}
			}
		}
		
		GameObject chunkGameObject = UnityEngine.Object.Instantiate(defaultMesh);
		chunkGameObject.setPosition(chunk.getPos().toVector3());
		setChunkGameObjectPos(chunkGameObject, chunk.getPos());
		
		MeshFilter meshFilter = chunkGameObject.GetComponent<MeshFilter>();
		Mesh mesh = new Mesh(); // meshFilter.mesh;
		mesh.vertices = vertices.ToArray();
		mesh.triangles = triangles.ToArray();
		//mesh.normals = normals.toArray();
		mesh.RecalculateNormals(); // FIXME: do normals by hand?  Should save some generation time
		meshFilter.mesh = mesh;
	}

	private void setChunkGameObjectPos(GameObject chunkGameObject, ChunkPos pos) {
		chunkGameObject.setPosition(pos.toVector3());
		BoxCollider collider = chunkGameObject.GetComponent<BoxCollider>();
		Vector3 center = collider.center;
		center.x = Chunk.CHUNK_SIZE_X / 2;
		center.y = Chunk.CHUNK_SIZE_Y / 2;
		center.z = Chunk.CHUNK_SIZE_Z / 2;
		collider.center = center;
		Vector3 size = collider.size;
		size.x = Chunk.CHUNK_SIZE_X;
		size.y = Chunk.CHUNK_SIZE_Y;
		size.z = Chunk.CHUNK_SIZE_Z;
		collider.size = size;
	}

	public Block getAdjacentBlock(BlockFace blockFace, Chunk chunk, Block block) {
		return Chunk.attainBlock(block.getWorldPos().facing(blockFace));
	}

	public void renderBlock(List<Vector3> vertices, List<int> triangles, Chunk chunk, WorldPos pos) {
		Block block = Chunk.attainBlock(BlockType.AIR, pos);
		if (!block.isVisible()) {
			return;
		}
		
		int x = pos.x - chunk.getStartX();
		int y = pos.y - chunk.getStartY();
		int z = pos.z - chunk.getStartZ();
		
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
		
		int x = block.getWorldX();
		int y = block.getWorldY();
		int z = block.getWorldZ();
		

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

using UnityEngine;
using System.Collections;

// W-16 = C-16
// W-15 = C -1
// W -1 = C -1
// W  0 = C  0
// W 15 = C  0
// W 16 = C 16
public struct ChunkPos {
	public int x;
	public int y;
	public int z;

	public ChunkPos(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	public ChunkPos(WorldPos worldPos) {
		this.x = worldPos.x / Chunk.CHUNK_SIZE_X;
		this.y = worldPos.y / Chunk.CHUNK_SIZE_Y;
		this.z = worldPos.z / Chunk.CHUNK_SIZE_Z;
		
		if (worldPos.x < 0) {
			this.x -= 1;
			this.x *= Chunk.CHUNK_SIZE_X;
		}
		if (worldPos.y < 0) {
			this.y -= 1;
			this.y *= Chunk.CHUNK_SIZE_Y;
		}
		if (worldPos.z < 0) {
			this.z -= 1;
			this.z *= Chunk.CHUNK_SIZE_Z;
		}	
	}

}

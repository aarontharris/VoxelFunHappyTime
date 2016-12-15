using System;

// A Chunk's position in World Space
// Special Because a Chunk snaps to multiples of CHUNK_SIZE in WorldPos.
public struct ChunkPos {
	public int x;
	public int y;
	public int z;

	/// <summary>
	/// Must be guaranteed to be multiples of CHUNK_SIZE
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
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
		}
		this.x *= Chunk.CHUNK_SIZE_X;
		
		if (worldPos.y < 0) {
			this.y -= 1;
		}
		this.y *= Chunk.CHUNK_SIZE_Y;
		
		if (worldPos.z < 0) {
			this.z -= 1;
		}	
		this.z *= Chunk.CHUNK_SIZE_Z;
		
	}

	public override string ToString() {
		return string.Format("{0},{1},{2}", x, y, z);
	}
}

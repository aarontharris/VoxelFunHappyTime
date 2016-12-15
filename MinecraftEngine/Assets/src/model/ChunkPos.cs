using System;

// A Chunk's position in World Space
// Special Because a Chunk snaps to multiples of CHUNK_SIZE in WorldPos.
public struct ChunkPos {

	public static void test() {
		for (int x = -32; x < 32; x++) {
			WorldPos wpos = new WorldPos(x, 0, 0);
			ChunkPos cpos = new ChunkPos(wpos);
			BlockPos bpos = new BlockPos(wpos);
			Log.d("{0}, {1}, {2}", wpos, cpos, bpos);
		}
	}

	public int x;
	public int y;
	public int z;

	/// <summary>
	/// Must be guaranteed to be multiples of CHUNK_SIZE
	/// </summary>
	/// <param name="x">The x coordinate.</param>
	/// <param name="y">The y coordinate.</param>
	/// <param name="z">The z coordinate.</param>
	private ChunkPos(int x, int y, int z) {
		this.x = x;
		this.y = y;
		this.z = z;
	}

	// wp  to   cp
	// -1  to  -16
	// -16 to  -16
	// -63 to  -64  1
	// -64 to  -64  0
	// -65 to  -80 15
	// -66 to  -80 14
	// -67 to  -80 13
	// -68 to  -80 12
	// -69 to  -80 11
	// -70 to  -80 10
	// -71 to  -80  9
	// -72 to  -80  8
	// -73 to  -80  7
	// -74 to  -80  6
	// -75 to  -80  5
	// -76 to  -80  4
	// -77 to  -80  3
	// -78 to  -80  2
	// -79 to  -80  1
	// -80 to  -80  0
	public ChunkPos(WorldPos worldPos) {
		this.x = worldPos.x / Chunk.CHUNK_SIZE_X;
		this.y = worldPos.y / Chunk.CHUNK_SIZE_Y;
		this.z = worldPos.z / Chunk.CHUNK_SIZE_Z;
		
		if (worldPos.x < 0 && worldPos.x % Chunk.CHUNK_SIZE_X != 0) {
			this.x -= 1;
		}
		this.x *= Chunk.CHUNK_SIZE_X;
		
		if (worldPos.y < 0 && worldPos.y % Chunk.CHUNK_SIZE_Y != 0) {
			this.y -= 1;
		}
		this.y *= Chunk.CHUNK_SIZE_Y;
		
		if (worldPos.z < 0 && worldPos.z % Chunk.CHUNK_SIZE_Z != 0) {
			this.z -= 1;
		}
		this.z *= Chunk.CHUNK_SIZE_Z;
		
	}

	public override string ToString() {
		return string.Format("[ChunkPos {0},{1},{2}]", x, y, z);
	}
}

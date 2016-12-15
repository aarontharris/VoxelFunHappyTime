
/// <summary>
/// 1 unit is 1 unit
/// </summary>
public struct WorldPos {
	public int x;
	public int y;
	public int z;

	public WorldPos(int x, int y, int z) {
		this.x = x;	
		this.y = y;
		this.z = z;
	}

	public WorldPos(ChunkPos chunkPos) {
		this.x = chunkPos.x;
		this.y = chunkPos.y;
		this.z = chunkPos.z;
	}

	public WorldPos(ChunkPos chunkPos, BlockPos blockPos) {
		this.x = chunkPos.x + blockPos.x;
		this.y = chunkPos.y + blockPos.y;
		this.z = chunkPos.z + blockPos.z;
	}

	public override string ToString() {
		return string.Format("[WorldPos {0},{1},{2}]", x, y, z);
	}
}

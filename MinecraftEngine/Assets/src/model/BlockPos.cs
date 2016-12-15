using System;

// Position of a block within a Chunk Space
// 0 to CHUNK_SIZE ^ 3 for x,y,z
public struct BlockPos {

	public ChunkPos cpos;
	public byte x;
	public byte y;
	public byte z;

	public BlockPos(WorldPos pos) {
		this.cpos = new ChunkPos(pos);
		this.x = (byte)(pos.x - cpos.x);
		this.y = (byte)(pos.y - cpos.y);
		this.z = (byte)(pos.z - cpos.z);
	}

	public override string ToString() {
		return string.Format("[BlockPos {0},{1},{2},{3}]", cpos, x, y, z);
	}
}


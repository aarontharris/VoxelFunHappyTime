public struct Chunk {

	public static readonly byte CHUNK_SIZE_X = 16;
	public static readonly byte CHUNK_SIZE_Y = 16;
	public static readonly byte CHUNK_SIZE_Z = 16;

	public static Block lookupBlock(int x, int y, int z) {
		Chunk chunk = World.get().attainChunk(x, y, z);
		return chunk.attainBlock(x, y, z);
	}

	public static Chunk newInst(int x, int y, int z) {
		Chunk chunk = new Chunk();
		chunk.blocks = new Block[CHUNK_SIZE_X, CHUNK_SIZE_Y, CHUNK_SIZE_Z];
		chunk.x = x;
		chunk.y = y;
		chunk.z = z;
		return chunk;
	}

	private int x;
	private int y;
	private int z;
	
	public Block[,,] blocks;

	public int getStartX() {
		return x;
	}

	public int getEndX() {
		return x + CHUNK_SIZE_X;
	}

	public int getStartY() {
		return y;
	}

	public int getEndY() {
		return y + CHUNK_SIZE_Y;
	}

	public int getStartZ() {
		return z;
	}

	public int getEndZ() {
		return z + CHUNK_SIZE_Z;
	}

	// Null when block is not present
	public Block getBlock(int x, int y, int z) {
		if (isInside(x, y, z)) {
			Block block = blocks[x, y, z];
			return block;
		}
		return Chunk.lookupBlock(x, y, z); // potential infinite loop, but "shouldn't" happen unless XYZ is F'd
	}

	public Block attainBlock(int x, int y, int z) {
		return attainBlock(BlockType.AIR, x, y, z);
	}

	public Block attainBlock(BlockType type, int x, int y, int z) {
		Block block = getBlock(x, y, z);
		if (block.Equals(default(Block))) {
			block = Block.newInst(type, x, y, z);
			putBlock(block, x, y, z);
		}	
		return block;
	}

	public void putBlock(Block block, int x, int y, int z) {
		blocks[x, y, z] = block;
	}

	public bool isInside(int x, int y, int z) {
		if (x >= this.x && x < this.x + CHUNK_SIZE_X &&
		    y >= this.y && y < this.y + CHUNK_SIZE_Y &&
		    z >= this.z && z < this.z + CHUNK_SIZE_Z) {
			return true;
		}
		return false;
	}
	
}

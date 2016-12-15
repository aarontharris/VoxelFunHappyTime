using System;
using UnityEngine;


public struct Chunk {

	public static readonly byte CHUNK_SIZE_X = 16;
	public static readonly byte CHUNK_SIZE_Y = 16;
	public static readonly byte CHUNK_SIZE_Z = 16;

	public static Chunk newInst(ChunkPos pos) {
		Chunk chunk = new Chunk();
		chunk.blocks = new Block[CHUNK_SIZE_X, CHUNK_SIZE_Y, CHUNK_SIZE_Z];
		chunk.pos = pos;
		return chunk;
	}

	public static Block attainBlock(WorldPos pos) {
		return Chunk.attainBlock(BlockType.AIR, pos);
	}

	public static Block attainBlock(BlockType type, WorldPos pos) {
		Chunk chunk = World.get().attainChunk(pos);
		Block block = chunk.getBlock(pos);
		if (block.Equals(default(Block))) {
			block = Block.newInst(type, chunk, pos);
			chunk.putBlock(block, pos);
		}	
		return block;
	}

	// Nullable
	public static Block lookupBlock(WorldPos pos) {
		Chunk chunk = World.get().attainChunk(pos);
		Block block = chunk.getBlock(pos);
		return block;
	}

	private ChunkPos pos;

	public ChunkPos getPos() {
		return pos;
	}

	public int getStartX() {
		return pos.x;
	}

	public int getEndX() {
		return pos.x + CHUNK_SIZE_X;
	}

	public int getStartY() {
		return pos.y;
	}

	public int getEndY() {
		return pos.y + CHUNK_SIZE_Y;
	}

	public int getStartZ() {
		return pos.z;
	}

	public int getEndZ() {
		return pos.z + CHUNK_SIZE_Z;
	}

	public Block[,,] blocks;

	// Null when block is not present
	private Block getBlock(WorldPos pos) {
		BlockPos bpos = new BlockPos(pos);
		try {
			Block block = blocks[bpos.x, bpos.y, bpos.z];
			return block;
			//Block block = blocks[pos.x - getPos().x, pos.y - getPos().y, pos.z - getPos().z];
		} catch (Exception e) {
			Log.d("getBlock( {0} ) Failed.  BlockPos={1}", pos, bpos);	
			throw e;
		}
	}

	private void putBlock(Block block, WorldPos pos) {
		BlockPos bpos = new BlockPos(pos);
		//blocks[pos.x - getPos().x, pos.y - getPos().y, pos.z - getPos().z] = block;
		blocks[bpos.x, bpos.y, bpos.z] = block;
	}

	public bool isInside(WorldPos pos) {
		if (pos.x >= getStartX() && pos.x < getEndX() &&
		    pos.y >= getStartY() && pos.y < getEndY() &&
		    pos.z >= getStartZ() && pos.z < getEndZ()) {
			return true;
		}
		return false;
	}
	
}

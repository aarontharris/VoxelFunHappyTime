using System.Collections;
using System;

public struct Block {

	public static Block newInst(BlockType type, WorldPos pos) {
		Block block = new Block();
		block.type = type;
		block.wpos = pos;
		return block;
	}

	private BlockType type;
	
	private WorldPos wpos;
	private ChunkPos cpos;

	public WorldPos getWorldPosition() {
		return wpos;
	}

	public int getWorldX() {
		return wpos.x;
	}

	public int getWorldY() {
		return wpos.y;
	}

	public int getWorldZ() {
		return wpos.z;
	}

	public bool isVisible() {
		return type.isVisible();
	}

}

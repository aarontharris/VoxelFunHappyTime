using System.Collections;
using System;

public struct Block {

	public static Block newInst(BlockType type, WorldPos pos) {
		Block block = new Block();
		block.type = type;
		block.pos = pos;
		return block;
	}

	private BlockType type;
	
	private WorldPos pos;

	public WorldPos getPosition() {
		return pos;
	}

	public int getX() {
		return pos.x;
	}

	public int getY() {
		return pos.y;
	}

	public int getZ() {
		return pos.z;
	}

	public bool isVisible() {
		return type.isVisible();
	}

}

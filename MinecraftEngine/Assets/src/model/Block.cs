using System.Collections;
using System;

public struct Block {

	public static Block newInst(BlockType type, int x, int y, int z) {
		Block block = new Block();
		block.type = type;
		block.x = x;
		block.y = y;
		block.z = z;
		return block;
	}

	private BlockType type;
	
	private int x;
	private int y;
	private int z;

	public int getX() {
		return x;
	}

	public int getY() {
		return y;
	}

	public int getZ() {
		return z;
	}

	public bool isVisible() {
		return type.isVisible();
	}

}

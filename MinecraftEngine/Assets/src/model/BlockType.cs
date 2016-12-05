using UnityEngine;
using System.Collections;

public struct BlockType {

	public static readonly BlockType AIR = newInst(0, false);
	public static readonly BlockType GRANITE = newInst(1, true);
	
	private bool visible;
	private byte id;

	private static BlockType newInst(byte id, bool visible) {
		BlockType blockType = new BlockType();
		blockType.id = id;
		blockType.visible = visible;
		return blockType;
	}

	public bool isVisible() {
		return visible;
	}

	public byte getId() {
		return id;
	}

	public bool sameType(BlockType type) {
		return getId() == type.getId();
	}
}

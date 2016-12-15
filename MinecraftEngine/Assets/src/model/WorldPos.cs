using UnityEngine;
using System;


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

	public WorldPos above() {
		return new WorldPos(x, y + 1, z);
	}

	public WorldPos below() {
		return new WorldPos(x, y - 1, z);
	}

	public WorldPos front() {
		return new WorldPos(x, y, z + 1);
	}

	public WorldPos back() {
		return new WorldPos(x, y, z - 1);
	}

	public WorldPos left() {
		return new WorldPos(x - 1, y, z);
	}

	public WorldPos right() {
		return new WorldPos(x + 1, y, z);
	}

	public WorldPos facing(BlockFace face) {
		if (face.isTop()) {
			return above();
		} else if (face.isBottom()) {
			return below();
		} else if (face.isLeft()) {
			return left();
		} else if (face.isRight()) {
			return right();
		} else if (face.isFront()) {
			return front();
		} else if (face.isBack()) {
			return back();
		}
		throw new Exception("Unknown BlockFace " + face);
	}

	public Vector3 toVector3() {
		return new Vector3(x, y, z);
	}

	public override string ToString() {
		return string.Format("[WorldPos {0},{1},{2}]", x, y, z);
	}
	
}

using UnityEngine;
using System.Collections;

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
}

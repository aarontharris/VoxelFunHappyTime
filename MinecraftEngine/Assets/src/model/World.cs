using System.Collections.Generic;

public struct World {
	public static readonly byte CHUNK_RADIUS = 5;

	private static World self;

	public static World get() {
		if (World.self.Equals(default(World))) {
			World.self = World.newInst();
		}
		return World.self;
	}

	private static World newInst() {
		World world = new World();
		world.chunksMap = new Dictionary<int, Dictionary<int,Dictionary<int, Chunk>>>();
		return world;
	}

	private Dictionary<int, Dictionary<int,Dictionary<int, Chunk>>> chunksMap;

	public Chunk attainChunk(int x, int y, int z) {
		int xChunk = x / Chunk.CHUNK_SIZE_X;
		int yChunk = y / Chunk.CHUNK_SIZE_Y;
		int zChunk = z / Chunk.CHUNK_SIZE_Z;
		if (x < 0) {
			xChunk -= 1;
		}
		if (y < 0) {
			yChunk -= 1;
		}
		if (z < 0) {
			zChunk -= 1;
		}
		
		Dictionary<int, Dictionary<int,Dictionary<int, Chunk>>> xyzChunks = chunksMap;
		Dictionary<int,Dictionary<int, Chunk>> yzChunks;
		Dictionary<int, Chunk> zChunks;
		Chunk chunk = default(Chunk);
		
		if (!xyzChunks.ContainsKey(xChunk)) {
			yzChunks = new Dictionary<int, Dictionary<int, Chunk>>();
			xyzChunks.Add(xChunk, yzChunks);
		} else {
			yzChunks = xyzChunks[xChunk];
		}
		
		if (!yzChunks.ContainsKey(yChunk)) {
			zChunks = new Dictionary<int, Chunk>();
			yzChunks.Add(yChunk, zChunks);
		} else {
			zChunks = yzChunks[yChunk];
		}
		
		if (!zChunks.ContainsKey(zChunk)) {
			chunk = Chunk.newInst(xChunk, yChunk, zChunk);
			zChunks.Add(zChunk, chunk);
		} else {
			chunk = zChunks[zChunk];
		}
		
		return chunk;
	}
}

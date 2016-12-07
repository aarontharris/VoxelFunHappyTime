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

	public Chunk attainChunk(WorldPos worldPos) {
		ChunkPos pos = new ChunkPos(worldPos);
		
		Dictionary<int, Dictionary<int,Dictionary<int, Chunk>>> xyzChunks = chunksMap;
		Dictionary<int,Dictionary<int, Chunk>> yzChunks;
		Dictionary<int, Chunk> zChunks;
		Chunk chunk = default(Chunk);
		
		if (!xyzChunks.ContainsKey(pos.x)) {
			yzChunks = new Dictionary<int, Dictionary<int, Chunk>>();
			xyzChunks.Add(pos.x, yzChunks);
		} else {
			yzChunks = xyzChunks[pos.x];
		}
		
		if (!yzChunks.ContainsKey(pos.y)) {
			zChunks = new Dictionary<int, Chunk>();
			yzChunks.Add(pos.y, zChunks);
		} else {
			zChunks = yzChunks[pos.y];
		}
		
		if (!zChunks.ContainsKey(pos.z)) {
			chunk = Chunk.newInst(pos);
			zChunks.Add(pos.z, chunk);
		} else {
			chunk = zChunks[pos.z];
		}
		
		return chunk;
	}
}

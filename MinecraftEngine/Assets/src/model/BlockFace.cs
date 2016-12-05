
public struct BlockFace {

	private static BlockFace newInst(byte face) {
		BlockFace blockFace = new BlockFace();
		blockFace.face = face;
		return blockFace;
	}

	public static readonly BlockFace TOP = BlockFace.newInst(1);
	public static readonly BlockFace BOT = BlockFace.newInst(2);
	
	public static readonly BlockFace FRT = BlockFace.newInst(3);
	public static readonly BlockFace BAK = BlockFace.newInst(4);
	
	public static readonly BlockFace LFT = BlockFace.newInst(5);
	public static readonly BlockFace RHT = BlockFace.newInst(6);
	
	private byte face;

	public byte getFaceValue() {
		return face;
	}

	public bool isTop() {
		return TOP.face == face;
	}

	public bool isBottom() {
		return BOT.face == face;
	}

	public bool isFront() {
		return FRT.face == face;
	}

	public bool isBack() {
		return BAK.face == face;
	}

	public bool isLeft() {
		return LFT.face == face;
	}

	public bool isRight() {
		return RHT.face == face;
	}

}

using UnityEngine;
using System.Collections;

public class CanvasInputHandler : MonoBehaviour {
	private BlockGenerator mBoxGenerator;

	void Start () {
		mBoxGenerator = Object.FindObjectOfType<BlockGenerator>();
	}
	
	public void onClickGenerateBlocks() {
		Debug.Log("onClickGenerateBlocks");
		mBoxGenerator.doGenerateNextSet();
	}
}

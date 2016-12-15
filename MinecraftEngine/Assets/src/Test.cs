using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Test : MonoBehaviour {

	void Start() {
		//ChunkPos.test();
	}

	void test1() {
		Stopwatch stopwatch = new Stopwatch();
		stopwatch.Start();
		
		
		stopwatch.Stop();
		UnityEngine.Debug.LogFormat("Test1: Duration {0}", stopwatch.ElapsedMilliseconds);
	}

}

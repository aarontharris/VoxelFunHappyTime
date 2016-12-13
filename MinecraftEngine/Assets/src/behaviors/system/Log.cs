using UnityEngine;

public static class Log {

	public static void d(string format, params System.Object[] args) {
		Debug.LogFormat(format, args);
	}

}


using UnityEngine;
using System.Collections;

public static class Extensions {

	public static void setPosition(this GameObject gameObject, float x, float y, float z) {
		setPosition(gameObject.transform, x, y, z);
	}

	public static void setPosition(this Transform transform, float x, float y, float z) {
		Vector3 position = transform.position;
		position.x = x;
		position.y = y;
		position.z = z;
		transform.position = position;
	}
	
}

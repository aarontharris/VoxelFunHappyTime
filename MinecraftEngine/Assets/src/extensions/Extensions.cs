using UnityEngine;
using System.Collections;

public static class Extensions {

	public static void setPosition(this GameObject gameObject, Vector3 pos) {
		setPosition(gameObject.transform, pos.x, pos.y, pos.z);
	}

	public static void setPosition(this GameObject gameObject, float x, float y, float z) {
		setPosition(gameObject.transform, x, y, z);
	}

	public static void setPosition(this Transform transform, Vector3 pos) {
		setPosition(transform, pos.x, pos.y, pos.z);
	}

	public static void setPosition(this Transform transform, float x, float y, float z) {
		Vector3 position = transform.position;
		position.x = x;
		position.y = y;
		position.z = z;
		transform.position = position;
	}

	public static void setXYZ(this Vector3 vector, float x, float y, float z) {
		Vector3 rval = vector;
		rval.x = x;
		rval.y = y;
		rval.z = z;
	}
}

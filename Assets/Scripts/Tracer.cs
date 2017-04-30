using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracer : MonoBehaviour
{
	Vector3 lastPos;

	void Update()
	{
		Debug.DrawLine(lastPos, transform.position, Color.red, Mathf.Infinity);
		lastPos = transform.position;
	}
}

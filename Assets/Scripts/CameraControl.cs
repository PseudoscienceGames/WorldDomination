using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
	public float moveSpeed;
	public float zoomSpeed;
	public TerrainGenerator tg;

	void Start()
	{
		tg = GameObject.Find("Terrain").GetComponent<TerrainGenerator>();
		transform.position = new Vector3(transform.position.x, tg.visibleLevel * tg.voxelSize.y, transform.position.z);
	}

	void Update()
	{
		if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
		{
			transform.position += moveSpeed * Input.GetAxis("Horizontal") * transform.right;
			transform.position += moveSpeed * Input.GetAxis("Vertical") * transform.forward;
		}
		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			transform.position = new Vector3(transform.position.x, tg.visibleLevel * tg.voxelSize.y, transform.position.z);
			//Camera.main.orthographicSize -= Input.GetAxisRaw("Mouse ScrollWheel") * zoomSpeed;
		}
	}
}
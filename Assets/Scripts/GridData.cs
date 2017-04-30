using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridData : MonoBehaviour
{
	public int level;
	public Dictionary<Vector2, int> grid = new Dictionary<Vector2, int>();
	public Vector3 voxelSize;
	FloorMesh floor;
	WallMesh walls;
	public bool updateMeshes = false;

	void Update()
	{
		if(updateMeshes)
		{
			updateMeshes = false;
			floor.GenMeshData();
			walls.GenMeshData();
		}
	}

	void Start()
	{
		floor = transform.GetComponentInChildren<FloorMesh>();
		floor.gd = this;
		walls = transform.GetComponentInChildren<WallMesh>();
		walls.gd = this;
	}

	public void AddBlock(Vector2 gridLoc, int type)
	{
		grid.Add(gridLoc, type);
		updateMeshes = true;

	}
	public void RemoveBlock(Vector2 gridLoc)
	{
		if (grid.ContainsKey(gridLoc))
		{
			grid[gridLoc] = 0;
		}
		updateMeshes = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMesh : MeshData
{
	public GridData gd;

	public void GenMeshData()
	{
		foreach (Vector2 levelLoc in gd.grid.Keys)
		{
			Vector3 gridLoc;
			gridLoc.x = levelLoc.x;
			gridLoc.y = gd.level;
			gridLoc.z = levelLoc.y;
			if (gd.grid[levelLoc] == 0)
			{
				AddWalls(gridLoc);
			}
		}
		GenMesh();
	}
	void AddWalls(Vector3 gridLoc)
	{
		Vector2 north = new Vector2(gridLoc.x, gridLoc.z + 1);
		Vector2 south = new Vector2(gridLoc.x, gridLoc.z - 1);
		Vector2 west = new Vector2(gridLoc.x - 1, gridLoc.z);
		Vector2 east = new Vector2(gridLoc.x + 1, gridLoc.z);
		gridLoc = Vector3.Scale(gridLoc, gd.voxelSize);
		if (gd.grid.ContainsKey(north) && gd.grid[north] == 1)
			AddSideN(gridLoc);
		if (gd.grid.ContainsKey(south) && gd.grid[south] == 1)
			AddSideS(gridLoc);
		if (gd.grid.ContainsKey(west) && gd.grid[west] == 1)
			AddSideW(gridLoc);
		if (gd.grid.ContainsKey(east) && gd.grid[east] == 1)
			AddSideE(gridLoc);
	}
	void AddSideN(Vector3 gridLoc)
	{
		verts.Add(gridLoc + new Vector3(0, 0, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(0, gd.voxelSize.y * 0.9f, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, gd.voxelSize.y * 0.9f, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, 0, gd.voxelSize.z));
		AddTrisAndUVs();
	}
	void AddSideE(Vector3 gridLoc)
	{
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, 0, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, gd.voxelSize.y * 0.9f, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, gd.voxelSize.y * 0.9f, 0));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, 0, 0));
		AddTrisAndUVs();
	}
	void AddSideS(Vector3 gridLoc)
	{
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, 0, 0));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, gd.voxelSize.y * 0.9f, 0));
		verts.Add(gridLoc + new Vector3(0, gd.voxelSize.y * 0.9f, 0));
		verts.Add(gridLoc);
		AddTrisAndUVs();
	}
	void AddSideW(Vector3 gridLoc)
	{
		verts.Add(gridLoc);
		verts.Add(gridLoc + new Vector3(0, gd.voxelSize.y * 0.9f, 0));
		verts.Add(gridLoc + new Vector3(0, gd.voxelSize.y * 0.9f, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(0, 0, gd.voxelSize.z));
		AddTrisAndUVs();
	}
	void AddTrisAndUVs()
	{
		tris.Add(vertNum);
		tris.Add(vertNum + 1);
		tris.Add(vertNum + 2);
		tris.Add(vertNum + 2);
		tris.Add(vertNum + 3);
		tris.Add(vertNum);
		uvs.Add(new Vector3(0, 0));
		uvs.Add(new Vector3(0, 1));
		uvs.Add(new Vector3(1, 1));
		uvs.Add(new Vector3(1, 0));
		vertNum += 4;
	}
}

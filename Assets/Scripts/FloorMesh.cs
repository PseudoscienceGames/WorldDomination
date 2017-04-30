using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FloorMesh : MeshData
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
			if (gd.grid[levelLoc] == 1)
				gridLoc.y += 0.9f;
			gridLoc = Vector3.Scale(gridLoc, gd.voxelSize);
			AddTop(gridLoc);
		}
		GenMesh();
		GetComponent<NavMeshSurface>().BuildNavMesh();
	}

	void AddTop(Vector3 gridLoc)
	{
		verts.Add(gridLoc);
		verts.Add(gridLoc + new Vector3(0, 0, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, 0, gd.voxelSize.z));
		verts.Add(gridLoc + new Vector3(gd.voxelSize.x, 0, 0));

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

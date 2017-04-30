using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
	public float level;
	public Dictionary<Vector2, int> grid = new Dictionary<Vector2, int>();
	public List<Vector3> verts;
	public List<int> tris;
	public List<Vector2> uvs;
	public int vertNum = 0;
	public Mesh mesh;
	public MeshCollider col;
	Vector3 voxelSize;
	float z;
	public TerrainGenerator tg;

	void Start()
	{
		voxelSize = tg.voxelSize;
		mesh = GetComponent<MeshFilter>().mesh;
		col = GetComponent<MeshCollider>();
		GenMesh();
	}

	public void AddBlock(Vector2 gridLoc, int type)
	{
		grid.Add(gridLoc, type);
	}
	public void RemoveBlock(Vector2 gridLoc)
	{
		if (grid.ContainsKey(gridLoc))
		{
			grid.Remove(gridLoc);
			GenMesh();
		}
	}

	public void GenMesh()
	{
		mesh.Clear();
		vertNum = 0;
		verts.Clear();
		tris.Clear();
		uvs.Clear();
		foreach(KeyValuePair<Vector2, int> kvp in grid)
			AddVoxel(kvp.Key.x, kvp.Key.y);

		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		//col.sharedMesh = null;
		//col.sharedMesh = mesh;
	}

	void AddVoxel(float x, float y)
	{
		float x2 = x * voxelSize.x;
		float y2 = y * voxelSize.z;
		z = level * voxelSize.y;
		AddTop(x2, y2);
		//AddBottom(x2, y2);
		//if (!grid.ContainsKey(new Vector2(x, y + 1)))
		//	AddSideN(x2, y2);
		//if (!grid.ContainsKey(new Vector2(x + 1, y)))
		//	AddSideE(x2, y2);
		//if (!grid.ContainsKey(new Vector2(x, y - 1)))
		//	AddSideS(x2, y2);
		//if (!grid.ContainsKey(new Vector2(x - 1, y)))
		//	AddSideW(x2, y2);
	}
    void AddTop(float x, float y)
	{
		verts.Add(new Vector3(x, z + voxelSize.y, y));
		verts.Add(new Vector3(x, z + voxelSize.y, y + voxelSize.z));
		verts.Add(new Vector3(x + voxelSize.x, z + voxelSize.y, y + voxelSize.z));
		verts.Add(new Vector3(x + voxelSize.z, z + voxelSize.y, y));
		AddTrisAndUVs();
	}
	void AddBottom(float x, float y)
	{
		verts.Add(new Vector3(x + voxelSize.x, z, y));
		verts.Add(new Vector3(x + voxelSize.x, z, y + voxelSize.z));
		verts.Add(new Vector3(x, z, y + voxelSize.z));
		verts.Add(new Vector3(x, z, y));
		AddTrisAndUVs();
    }
    void AddSideN(float x, float y)
	{
		verts.Add(new Vector3(x + voxelSize.x, z, y + voxelSize.z));
		verts.Add(new Vector3(x + voxelSize.x, z + voxelSize.y, y + voxelSize.z));
		verts.Add(new Vector3(x, z + voxelSize.y, y + voxelSize.z));
		verts.Add(new Vector3(x, z, y + voxelSize.z));
		AddTrisAndUVs();
	}
	void AddSideE(float x, float y)
	{
		verts.Add(new Vector3(x + voxelSize.x, z, y));
		verts.Add(new Vector3(x + voxelSize.x, z + voxelSize.y, y));
		verts.Add(new Vector3(x + voxelSize.x, z + voxelSize.y, y + voxelSize.z));
		verts.Add(new Vector3(x + voxelSize.x, z, y + voxelSize.z));
		AddTrisAndUVs();
	}
	void AddSideS(float x, float y)
	{
		verts.Add(new Vector3(x, z, y));
		verts.Add(new Vector3(x, z + voxelSize.y, y));
		verts.Add(new Vector3(x + voxelSize.x, z + voxelSize.y, y));
		verts.Add(new Vector3(x + voxelSize.x, z, y));
		AddTrisAndUVs();
	}
	void AddSideW(float x, float y)
	{
		verts.Add(new Vector3(x, z, y + voxelSize.z));
		verts.Add(new Vector3(x, z + voxelSize.y, y + voxelSize.z));
		verts.Add(new Vector3(x, z + voxelSize.y, y));
		verts.Add(new Vector3(x, z, y));
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

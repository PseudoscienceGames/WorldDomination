using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marker : MonoBehaviour
{
	public MarkTool mt;
	public Vector3 markerStart;
	public Vector3 markerEnd;
	Vector3 bottomBackLeft;
	Vector3 topFrontRight;
	public float offset;
	public List<Vector3> verts = new List<Vector3>();
	public List<int> tris = new List<int>();
	public List<Vector2> uvs = new List<Vector2>();
	int vertNum = 0;
	float voxelSize = 2;
	Mesh mesh;

	public void StartMarker(Vector3 gridLoc)
	{
		mesh = GetComponent<MeshFilter>().mesh;
		markerStart = gridLoc;
		UpdateMarker(gridLoc);
	}
	public void EndMarker(Vector3 gridLoc)
	{
		UpdateMarker(gridLoc);
	}
	public void UpdateMarker(Vector3 gridLoc)
	{
		mesh.Clear();
		vertNum = 0;
		verts.Clear();
		tris.Clear();
		uvs.Clear();
		markerEnd = gridLoc;
		if (markerStart.x > markerEnd.x)
		{
			bottomBackLeft.x = markerEnd.x;
			topFrontRight.x = markerStart.x;
		}
		else
		{
			bottomBackLeft.x = markerStart.x;
			topFrontRight.x = markerEnd.x;
		}
		if (markerStart.y > markerEnd.y)
		{
			bottomBackLeft.y = markerEnd.y;
			topFrontRight.y = markerStart.y;
		}
		else
		{
			bottomBackLeft.y = markerStart.y;
			topFrontRight.y = markerEnd.y;
		}
		if (markerStart.z > markerEnd.z)
		{
			bottomBackLeft.z = markerEnd.z;
			topFrontRight.z = markerStart.z;
		}
		else
		{
			bottomBackLeft.z = markerStart.z;
			topFrontRight.z = markerEnd.z;
		}
		Vector3 bbl = bottomBackLeft;
		Vector3 tfr = topFrontRight;
		bbl *= voxelSize;
		tfr *= voxelSize;
		bbl -= new Vector3(offset, offset, offset);
		tfr += new Vector3(offset, offset, offset);
		tfr += new Vector3(voxelSize, voxelSize, voxelSize);

		verts.Add(new Vector3(bbl.x, tfr.y, bbl.z));
		verts.Add(new Vector3(bbl.x, tfr.y, tfr.z));
		verts.Add(new Vector3(tfr.x, tfr.y, tfr.z));
		verts.Add(new Vector3(tfr.x, tfr.y, bbl.z));
		AddTrisAndUVs();
		verts.Add(new Vector3(bbl.x, bbl.y, tfr.z));
		verts.Add(new Vector3(bbl.x, bbl.y, bbl.z));
		verts.Add(new Vector3(tfr.x, bbl.y, bbl.z));
		verts.Add(new Vector3(tfr.x, bbl.y, tfr.z));
		AddTrisAndUVs();
		verts.Add(new Vector3(tfr.x, bbl.y, bbl.z));
		verts.Add(new Vector3(tfr.x, tfr.y, bbl.z));
		verts.Add(new Vector3(tfr.x, tfr.y, tfr.z));
		verts.Add(new Vector3(tfr.x, bbl.y, tfr.z));
		AddTrisAndUVs();
		verts.Add(new Vector3(tfr.x, bbl.y, tfr.z));
		verts.Add(new Vector3(tfr.x, tfr.y, tfr.z));
		verts.Add(new Vector3(bbl.x, tfr.y, tfr.z));
		verts.Add(new Vector3(bbl.x, bbl.y, tfr.z));
		AddTrisAndUVs();
		verts.Add(new Vector3(bbl.x, bbl.y, tfr.z));
		verts.Add(new Vector3(bbl.x, tfr.y, tfr.z));
		verts.Add(new Vector3(bbl.x, tfr.y, bbl.z));
		verts.Add(new Vector3(bbl.x, bbl.y, bbl.z));
		AddTrisAndUVs();
		verts.Add(new Vector3(bbl.x, bbl.y, bbl.z));
		verts.Add(new Vector3(bbl.x, tfr.y, bbl.z));
		verts.Add(new Vector3(tfr.x, tfr.y, bbl.z));
		verts.Add(new Vector3(tfr.x, bbl.y, bbl.z));
		AddTrisAndUVs();

		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
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

	public void Demolish()
	{
		for(int x = (int)bottomBackLeft.x; x <= (int) topFrontRight.x; x++)
		{
			for (int y = (int)bottomBackLeft.y; y <= (int)topFrontRight.y; y++)
			{
				for (int z = (int)bottomBackLeft.z; z <= (int)topFrontRight.z; z++)
				{
					mt.tg.RemoveBlock(new Vector3(x, y, z));
				}
			}
		}
		Destroy(gameObject);
	}
}

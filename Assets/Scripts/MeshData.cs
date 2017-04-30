using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshData : MonoBehaviour
{
	public List<Vector3> verts = new List<Vector3>();
	public List<int> tris = new List<int>();
	public List<Vector2> uvs = new List<Vector2>();
	public int vertNum = 0;
	public Mesh mesh;
	public MeshCollider col;

	void Start()
	{
		mesh = GetComponent<MeshFilter>().mesh;
		col = GetComponent<MeshCollider>();
	}

	public void GenMesh()
	{
		Start();
		mesh.Clear();
		Debug.Log(verts.Count + " " + tris.Count);
		mesh.vertices = verts.ToArray();
		mesh.triangles = tris.ToArray();
		mesh.uv = uvs.ToArray();
		mesh.RecalculateNormals();
		mesh.RecalculateTangents();
		col.sharedMesh = null;
		col.sharedMesh = mesh;
		vertNum = 0;
		verts.Clear();
		tris.Clear();
		uvs.Clear();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkTool: MonoBehaviour
{
	public Vector3 gridLoc;
	public GameObject marker;
	public Marker currentMarker;
	public TerrainGenerator tg;

	void Start()
	{
		tg = GameObject.Find("Terrain").GetComponent<TerrainGenerator>();
	}

	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, 50))
		{
			Vector3 lastGridLoc = gridLoc;
			gridLoc = new Vector3(Mathf.Floor((hit.point.x - hit.normal.x)/ 2), tg.visibleLevel, Mathf.Floor((hit.point.z - hit.normal.z)/ 2));
			if (currentMarker != null && lastGridLoc != gridLoc)
				currentMarker.UpdateMarker(gridLoc);
		}
		if(Input.GetMouseButtonDown(1))
		{
			StartSelection();
		}
		if(Input.GetMouseButtonUp(1))
		{
			EndSelection();
		}

	}

	void StartSelection()
	{
		GameObject m = Instantiate(marker) as GameObject;
		currentMarker = m.GetComponent<Marker>();
		currentMarker.StartMarker(gridLoc);
		currentMarker.mt = this;
	}
	void EndSelection()
	{
		currentMarker.EndMarker(gridLoc);
		currentMarker = null;
	}
	public void Demolish()
	{
		foreach(GameObject marker in GameObject.FindGameObjectsWithTag("Marker"))
		{
			marker.GetComponent<Marker>().Demolish();
		}
	}
}

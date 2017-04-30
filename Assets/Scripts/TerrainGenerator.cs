using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
	public GameObject level;
	public List<GridData> chunks;
	public int visibleLevel = 1;
	public Vector3 voxelSize;
	public Vector3 worldSize;

	void Start()
	{
		GenerateTerrain();
    }
	void Update()
	{
		if (Input.GetAxis("Mouse ScrollWheel") != 0)
		{
			if(Input.GetAxis("Mouse ScrollWheel") > 0 && visibleLevel < worldSize.y - 1)
			{
				visibleLevel++;
				chunks[visibleLevel].gameObject.SetActive(true);
			}

			if (Input.GetAxis("Mouse ScrollWheel") < 0 && visibleLevel > 0)
			{
				chunks[visibleLevel].gameObject.SetActive(false);
				visibleLevel--;
			}
		}
	}

	void GenerateTerrain()
	{
		for (int i = 0; i < worldSize.y; i++)
		{
			GameObject currentLevel = Instantiate(level) as GameObject;
			GridData chunk = currentLevel.GetComponent<GridData>();
			chunk.level = i;
			chunk.voxelSize = voxelSize;
			currentLevel.transform.parent = transform;
			chunks.Add(chunk);
			GenerateChunk(chunk);
		}
		visibleLevel = (int)worldSize.y - 1;
	}

	void GenerateChunk(GridData chunk)
	{
		for(int x = -Mathf.RoundToInt(worldSize.x / 2f); x < Mathf.RoundToInt(worldSize.x / 2f); x++)
		{
			for (int y = -Mathf.RoundToInt(worldSize.z / 2f); y < Mathf.RoundToInt(worldSize.z / 2f); y++)
			{
				chunk.AddBlock(new Vector2(x, y), 1);

			}
		}
	}
	public void RemoveBlock(Vector3 gridLoc)
	{
		chunks[(int)gridLoc.y].RemoveBlock(new Vector2(gridLoc.x, gridLoc.z));
	}
}

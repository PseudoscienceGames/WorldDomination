using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
	public float spawntime;
	public float boxCount;
	public GameObject box;

	void Start()
	{
		for(int i = 0; i < boxCount; i++)
		{
			SpawnBox();
		}
	}

	public void SpawnBox()
	{
		int x = Random.Range(-25, 25);
		int y = Random.Range(1, 100);
		int z = Random.Range(-25, 25);
		Instantiate(box, new Vector3(x, y, z), Quaternion.identity);
	}
}

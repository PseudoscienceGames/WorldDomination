using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
	public SensorTypes myType;
	public float maxAngle;
	public float maxDistance;

	public Transform Sense(MapObject desire)
	{
		if (myType == SensorTypes.Eyes)
		{
			for(float i = -maxAngle * 0.5f; i <= maxAngle * 0.5f; i++)
			{
				RaycastHit hit;
				if(Physics.Raycast(transform.position, Quaternion.Euler(0, i, 0) * transform.forward, out hit))
				{
					if(hit.transform.GetComponent<MapObject>() != null && hit.transform.GetComponent<MapObject>().myType == desire.myType)
					{
						Debug.Log(hit.transform.position);
						return hit.transform;
					}
				}
			}
		}
		return null;
	}

	public enum SensorTypes { Eyes, other};
}

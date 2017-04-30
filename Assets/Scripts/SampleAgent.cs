using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SampleAgent : MonoBehaviour
{
	public MapObject desire;
	public Transform target;
	NavMeshAgent agent;
	Sensor sensor;

	void Start ()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void Update ()
	{
		agent.SetDestination(target.position);
		if (target == null)
			DeterminePath();
	}

	void DeterminePath()
	{
		sensor = GetComponent<Sensor>();
		if(target == null)
		{
			if(sensor != null)
			{
				target = sensor.Sense(desire);
                agent.SetDestination(target.position);
			}
		}
	}
}

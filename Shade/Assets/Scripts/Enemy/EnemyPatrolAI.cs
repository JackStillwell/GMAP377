using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour {

	public Transform[] WayPoints;
	private NavMeshAgent _navAgent;
	private int _nextPoint; 

	void Start () {
		_navAgent = gameObject.GetComponent<NavMeshAgent> ();
		_navAgent.autoBraking = false; 
		NextPoint ();
	}

	void Update () {
		if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.5f)
			NextPoint();

	}
	public void NextPoint() {
		if (WayPoints.Length == 0)
			return;
		_navAgent.destination = WayPoints[_nextPoint].position;
		_nextPoint = (_nextPoint + 1) % WayPoints.Length;
	}
}

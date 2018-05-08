using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour {

	[SerializeField] private Transform[] _wayPoints;
	private NavMeshAgent _navAgent;
	private int _nextPoint = 0; 

	void Start () {
		_navAgent = gameObject.GetComponent<NavMeshAgent> ();
		_navAgent.autoBraking = false; 
		NextPoint ();
	}

	void Update () {
		if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.5f) {
			
			NextPoint ();
		}

	}
	public void NextPoint() {
		if (_wayPoints.Length == 0)
			return;
		_navAgent.destination = _wayPoints[_nextPoint].position;
		_nextPoint = (_nextPoint + 1) % _wayPoints.Length;
	}
}

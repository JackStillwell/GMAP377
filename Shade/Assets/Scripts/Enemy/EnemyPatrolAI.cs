using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour 
{
	[SerializeField] private Transform[] _wayPoints;
	[SerializeField] private float minWaitTime = 1f;
	[SerializeField] private float maxWaitTime = 1f;
	private float waitTime;
	private NavMeshAgent _navAgent;	 
	private int _nextPoint = 0; 	 	
	private float patrolTimer = 0f;
	private float originalSpeed = 0f;
	private Rigidbody rb;
	private bool movingFlag = false;

	void Start () 
	{
		_navAgent = gameObject.GetComponent<NavMeshAgent>();
		_navAgent.autoBraking = true; 
		_navAgent.updateRotation = true;
		waitTime = Random.Range (minWaitTime, maxWaitTime);
		rb = gameObject.GetComponent<Rigidbody> ();
		Debug.Log (waitTime);
		_navAgent.Stop ();
		NextPoint();

	}

	void Update () 
	{


		if (!_navAgent.pathPending && _navAgent.remainingDistance < 4f) 
		{

			patrolTimer += Time.deltaTime;

			_navAgent.Stop ();
			if (patrolTimer > waitTime) {


				NextPoint ();
				patrolTimer = 0f;
				waitTime = Random.Range (minWaitTime, maxWaitTime);
				//Debug.Log (waitTime);
			}

		}
	}



	public void NextPoint() 
	{		
		_navAgent.Resume ();

		if (_wayPoints.Length == 0)
			return;
		_navAgent.destination = _wayPoints[_nextPoint].position;
		_nextPoint = (_nextPoint + 1) % _wayPoints.Length;
	}

}

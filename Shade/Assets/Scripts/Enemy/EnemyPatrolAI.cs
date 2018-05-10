using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour 
{

	[SerializeField] private Transform[] _wayPoints;
	[SerializeField] private float maxWaitTime = 0.2f;
	private float waitTime;
	private NavMeshAgent _navAgent;
	private int _nextPoint = 0; 
	private float patrolTimer = 0f;
	private float originalSpeed = 0f;
	private Rigidbody rb;

	void Start () 
	{
		_navAgent = gameObject.GetComponent<NavMeshAgent>();
		_navAgent.autoBraking = false; 
		_navAgent.updateRotation = true;
		waitTime = Random.Range (0f, maxWaitTime);
		rb = gameObject.GetComponent<Rigidbody> ();
		Debug.Log (waitTime);
		NextPoint();
	
	}

	void Update () 
	{
		

		if (!_navAgent.pathPending && _navAgent.remainingDistance < 0.1f) 
		{
			patrolTimer += Time.deltaTime;
			Debug.Log (patrolTimer);
			rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
			Debug.Log ("Freeze");
			//originalSpeed = _navAgent.speed;
			//_navAgent.speed = 0.01f;

			if (patrolTimer > waitTime) {
				rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX;
				NextPoint ();
				patrolTimer = 0f;
				waitTime = Random.Range (0f, maxWaitTime);
			}

		}
	}



	public void NextPoint() 
	{		Debug.Log ("ff");

		if (_wayPoints.Length == 0)
			return;

	
		_navAgent.destination = _wayPoints[_nextPoint].position;
		_nextPoint = (_nextPoint + 1) % _wayPoints.Length;


	}


}

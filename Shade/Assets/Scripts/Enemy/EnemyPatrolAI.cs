using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour {

	public Transform[] WayPoints;
	private NavMeshAgent NavAgent;
	private int nextPoint; 
	private bool PlayerInSight;

	void Start () {
		NavAgent = gameObject.GetComponent<NavMeshAgent> ();
		NavAgent.autoBraking = false; 
		NextPoint ();
		PlayerInSight = GetComponent<EnemySight> ().PlayerInSight;
	}
	


	void Update () {
		if (!NavAgent.pathPending && NavAgent.remainingDistance < 0.5f)
			NextPoint();

	}

	public void NextPoint() {
		// Returns if no points have been set up
		if (WayPoints.Length == 0)
			return;
		NavAgent.destination = WayPoints[nextPoint].position;
		nextPoint = (nextPoint + 1) % WayPoints.Length;
	}
}

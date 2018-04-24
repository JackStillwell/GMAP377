using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
	// How wide of an angle the object can see
	public float fieldOfViewAngle = 120f;
	//public string targetTag;
	public bool PlayerInSight;

	public GameObject Player;
	private SphereCollider col;


	private NavMeshAgent Nav;
	//private NavMeshAgent nav;

	void Awake()
	{
		Nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();


	}


	void Update()
	{
		if (PlayerInSight) {
		
			Debug.Log ("Player is in sight");
		} else {
			Debug.Log ("Player not found");
		}
	}



	void OnTriggerStay(Collider other)
	{
		if(other.gameObject == Player)
		{
			PlayerInSight = false;

			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			if (angle < fieldOfViewAngle * 0.5f) 
			{
				RaycastHit hit;
				if (Physics.Raycast (transform.position + transform.up, direction.normalized, out hit)) {
					if (hit.collider.gameObject == Player) {
						PlayerInSight = true;
					
					}	
				}
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject == Player)	
			PlayerInSight = false;
	}


	float CalculatePathLength(Vector3 targetPosition)

	{
		NavMeshPath path = new NavMeshPath ();
		if (Nav.enabled)
			Nav.CalculatePath (targetPosition, path);

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		allWayPoints [0] = transform.position;
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		for (int i = 0; i < path.corners.Length; i++) {
			allWayPoints [i + 1] = path.corners [i];

		
		}
		float pathLength = 0f;
		for (int i = 0; i < allWayPoints.Length - 1; i++) {
		
			pathLength += Vector3.Distance (allWayPoints [i], allWayPoints [i + 1]);

		}
		return pathLength;
	}
}
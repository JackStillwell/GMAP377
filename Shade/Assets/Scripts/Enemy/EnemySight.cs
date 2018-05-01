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

	private GameObject Player;
	private SphereCollider col;
	public Color playerView = new Color();
	private EnemyAI enemyAI;
	private NavMeshAgent Nav;
	private EnemyPatrolAI patrolAI;
	//private NavMeshAgent nav;

	void Start()
	{
		patrolAI = GetComponent<EnemyPatrolAI> ();
		Nav = GetComponent<NavMeshAgent> ();
		col = GetComponent<SphereCollider> ();
	    enemyAI = gameObject.GetComponent<EnemyAI> ();

		Player = GameObject.FindGameObjectWithTag("Player");
	}
	void Update()
	{
	}



	void OnTriggerStay(Collider other)
	{
		
		// if the player enters trigger
		if(other.gameObject == Player)
		{
			PlayerInSight = false;
			//get the direction of the player
			Vector3 direction = other.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			// check if the player is in sight
			if (angle < fieldOfViewAngle * 0.5f) 
			{
				Debug.Log(fieldOfViewAngle);
				RaycastHit hit;
				// player detection engine 
				// raycast to player's direction
				if (Physics.Raycast (transform.position, direction.normalized, out hit, col.radius)) 
				{
					// if enemy directly "sees" player
					if (hit.collider.gameObject == Player) 
					{
						playerView = Player.transform.GetComponent<Renderer>().material.color;
						PlayerInSight = true;
					} 
					//if enemy does not directly "sees" player
					else {

						//get the color of both objects and then math needs to happen
						//Color check engine 
						Color playerColor = Player.GetComponent<Renderer>().material.color;
						// GameObject hitobj = hitInfo.transform.gameObject;
						Color objColor = hit.transform.gameObject.GetComponent<MeshRenderer>().material.color;
						Color newColor = Color.white;
						newColor.a = playerColor.a;
						newColor.r = ((playerColor.r + (objColor.r * objColor.a)) / 2);
						newColor.g = ((playerColor.g + (objColor.g * objColor.a)) / 2);
						newColor.b = ((playerColor.b + (objColor.b * objColor.a)) / 2);
						playerView = newColor;
					}
				}
			}
		}
	}
	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == Player) {
			PlayerInSight = false;
			patrolAI.NextPoint ();
		}
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
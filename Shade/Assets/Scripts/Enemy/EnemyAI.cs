using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	[SerializeField]
	private GameObject Player;
	[SerializeField]
	private GameObject Camera;
	private Color PlayerColor;
	private Color EnemyColor;
	private bool PlayerInSight;
	public NavMeshAgent navAgent;



	// Use this for initialization
	void Start () 
	{
		EnemyColor = GetComponent<Renderer>().material.GetColor("_Color");

		//ColorRaycaster colorRaycaster = GetComponent<ColorRaycaster>();
		EnemySight enemySight = GetComponent<EnemySight> ();
		navAgent = GetComponent<NavMeshAgent> ();

		PlayerColor = enemySight.playerView;
		//StartCoroutine ("waitThreeSeconds");
	}

	// Update is called once per frame
	void Update ()
	{
		EnemyColor = GetComponent<Renderer>().material.GetColor("_Color");
		PlayerColor = GetComponent<EnemySight>().playerView;
		PlayerInSight = GetComponent<EnemySight> ().PlayerInSight;
		if (IsEqualTo (PlayerColor, EnemyColor)) 
		{

		} 
		else 
		{
			if (PlayerInSight) 
			{
			navAgent.updatePosition = true;
			navAgent.updateRotation = true;
			navAgent.SetDestination (Player.transform.position);
			}
		}

	}

	public bool IsEqualTo(Color me, Color other)
	{
		bool isRedSimilar = false, isGreenSimilar = false, isBlueSimilar = false;
		if (Mathf.Abs(other.r - me.r) < .1)
		{
			//Debug.Log(other);
			//Debug.Log(me);
			//Debug.Log(Mathf.Abs(other.r - me.r));
			isRedSimilar = true;
		}
		if (Mathf.Abs(other.b - me.b) < .1)
			isBlueSimilar = true;
		if (Mathf.Abs(other.g - me.g) < .1)
			isGreenSimilar = true;

		return isRedSimilar && isBlueSimilar && isGreenSimilar;
	}





		



}

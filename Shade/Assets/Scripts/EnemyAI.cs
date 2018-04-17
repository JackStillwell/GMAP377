using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	[SerializeField]
	private GameObject Player;



	// Use this for initialization
	void Start () {
		GetComponent<Renderer>().material.color = Player.GetComponent<Renderer>().material.GetColor("_Color");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

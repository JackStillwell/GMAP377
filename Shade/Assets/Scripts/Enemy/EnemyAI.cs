﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	[SerializeField]
	private GameObject Player;
	[SerializeField]
	private GameObject Camera;
	private Color PlayerColor;
	private Color EnemyColor;



	// Use this for initialization
	void Start () {

		EnemyColor = GetComponent<Renderer>().material.GetColor("_Color");


		ColorRaycaster colorRaycaster = GetComponent<ColorRaycaster> ();


		PlayerColor = colorRaycaster.playerView;
		//StartCoroutine ("waitThreeSeconds");



	}

	// Update is called once per frame
	void Update () {

		EnemyColor = GetComponent<Renderer>().material.GetColor("_Color");
		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");


		//Debug.Log (PlayerColor);
		//Debug.Log (EnemyColor);



		if (IsEqualTo (PlayerColor, EnemyColor)) {
			Debug.Log ("Same Color right here");
		} else {
			Debug.Log ("Color is not the same");
		}

	}

	public  bool IsEqualTo(Color me, Color other)
	{
		return me.r == other.r&& me.g == other.g && me.b == other.b && me.a == other.a;
	}
//	// change Player's color
//	IEnumerator waitThreeSeconds(){
//		Player.GetComponent<Renderer> ().material.color = Color.yellow;
//		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
//		Debug.Log (PlayerColor);
//			yield return new WaitForSeconds (3);
//		Player.GetComponent<Renderer> ().material.color = Color.blue;
//		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
//		Debug.Log (PlayerColor);
//			yield return new WaitForSeconds (3);
//		Player.GetComponent<Renderer> ().material.color = Color.green;
//		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
//		Debug.Log (PlayerColor);
//			yield return new WaitForSeconds (3);
//		Player.GetComponent<Renderer> ().material.color = EnemyColor;
//		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
//		Debug.Log (PlayerColor);
//
//	
//
//
//
//	}
//





		



}

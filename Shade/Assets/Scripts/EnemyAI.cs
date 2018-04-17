using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
	[SerializeField]
	private GameObject Player;
	private Color PlayerColor;



	// Use this for initialization
	void Start () {
		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
		StartCoroutine ("waitThreeSeconds");
	}

	// Update is called once per frame
	void Update () {



	}

	IEnumerator waitThreeSeconds(){

		Player.GetComponent<Renderer> ().material.color = Color.blue;
		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
		Debug.Log (PlayerColor);
			yield return new WaitForSeconds (3);
		Player.GetComponent<Renderer> ().material.color = Color.red;
		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
		Debug.Log (PlayerColor);
			yield return new WaitForSeconds (3);
		Player.GetComponent<Renderer> ().material.color = Color.green;
		PlayerColor = Player.GetComponent<Renderer>().material.GetColor("_Color");
		Debug.Log (PlayerColor);
	}



}

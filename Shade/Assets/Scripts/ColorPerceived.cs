using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPerceived : MonoBehaviour 
{
	[SerializeField] private GameObject _enemy;
	private Color _perceivedColor;
	// Use this for initialization
	void Start() 
	{
		_perceivedColor = _enemy.GetComponent<ColorRaycaster>().GetPercievedColor();
	}
	
	// Update is called once per frame
	void Update () 
	{
		_perceivedColor = _enemy.GetComponent<ColorRaycaster>().GetPercievedColor();
		transform.GetComponentInParent<Image>().color = _perceivedColor;
	}
}

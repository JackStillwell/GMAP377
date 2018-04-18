﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPerceived : MonoBehaviour 
{
	public GameObject enemy;
	private Color perceivedColor;
	// Use this for initialization
	void Start() 
	{
		perceivedColor = enemy.GetComponent<ColorRaycaster>().playerView;
	}
	
	// Update is called once per frame
	void Update () 
	{
		perceivedColor = enemy.GetComponent<ColorRaycaster>().playerView;
		transform.GetComponentInParent<Image>().color = perceivedColor;
	}
}

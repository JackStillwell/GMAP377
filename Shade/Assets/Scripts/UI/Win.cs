﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour 
{

	public int winScreen;
	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
			UnityEngine.SceneManagement.SceneManager.LoadScene(winScreen);
	}
	 
}

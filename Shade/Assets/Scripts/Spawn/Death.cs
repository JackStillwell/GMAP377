using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
	private Spawn spawnManager;
	
	[SerializeField] private GameObject spawnManagerObject;

	private void Start()
	{
		spawnManager = spawnManagerObject.GetComponent<Spawn>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player(Clone)")
		{
			Destroy(other.gameObject);

			spawnManager.TriggerSpawn();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyAI : MonoBehaviour
{
	private Spawn _spawnManager;
		
	private void Start()
	{
		_spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player(Clone)")
		{
			_spawnManager.TriggerRespawn(other.gameObject);
		}
	}
}

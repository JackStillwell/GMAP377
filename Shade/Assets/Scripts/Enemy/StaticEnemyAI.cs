using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class StaticEnemyAI : MonoBehaviour
{
	private Spawn _spawnManager;
	private EnemySight _sight;
		
	private void Start()
	{
		_spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
		_sight = GetComponent<EnemySight>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player") && 
		    _sight.IsPlayerVisible() &&
		    _sight.GetPercievedColor() == gameObject.GetComponent<Renderer>().material.color)
		{
			_spawnManager.TriggerRespawn(other.gameObject);
		}
	}
}

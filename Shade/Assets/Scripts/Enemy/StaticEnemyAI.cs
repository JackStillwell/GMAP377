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

	/* private void OnTriggerEnter(Collider other)
	{
		
		Debug.Log("PlayerVisible: " + _sight.IsPlayerVisible());
		
		if (other.CompareTag("Player"))
		{
			_spawnManager.TriggerRespawn(other.gameObject);
		}
	} */

	private void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player") && _sight.IsPlayerVisible() && _sight.GetPercievedColor() != GetEnemyColor())
		{
			_spawnManager.TriggerRespawn(other.gameObject);
		}
	}

	private Color GetEnemyColor()
	{
		return GetComponent<Renderer>().material.color;
	}
}

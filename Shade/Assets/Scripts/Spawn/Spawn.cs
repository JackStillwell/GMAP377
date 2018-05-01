using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class Spawn : MonoBehaviour
{
	private List<GameObject> _spawnPoints;
	private int _currentSpawn;
	
	[SerializeField] private GameObject _playerPrefab;
	
	// Use this for initialization
	void Start ()
	{
		gameObject.tag = "SpawnManager";
		
		_currentSpawn = 0;
		
		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawnpoint");

		_spawnPoints = new List<GameObject>(new GameObject[spawns.Length]);
		
		foreach (var go in spawns)
		{
			SpawnPoint sp = go.GetComponent<SpawnPoint>();
			_spawnPoints[sp.GetSpawnNumber()] = sp.gameObject;
		}
		
		TriggerSpawn();
	}

	public void SetCurrentSpawnNumber(int number)
	{
		_currentSpawn = number;
	}

	private void TriggerSpawn()
	{
		Instantiate(_playerPrefab, 
			_spawnPoints[_currentSpawn].transform.position, 
			_spawnPoints[_currentSpawn].transform.rotation);
	}
	
	public void TriggerRespawn(GameObject player)
	{
		player.transform.position = _spawnPoints[_currentSpawn].transform.position;
		player.transform.rotation = _spawnPoints[_currentSpawn].transform.rotation;
	}
}
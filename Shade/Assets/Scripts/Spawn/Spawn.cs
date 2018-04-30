﻿using System;
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
		_spawnPoints = new List<GameObject>();
		_currentSpawn = 0;
		
		gameObject.tag = "SpawnManager";

		GameObject[] spawns = GameObject.FindGameObjectsWithTag("Spawnpoint");
		
		foreach(GameObject go in spawns)
		{
			SpawnPoint sp = go.GetComponent<SpawnPoint>();
			_spawnPoints.Insert(sp.GetSpawnNumber(), go);
		}
		
		Instantiate(_playerPrefab, 
			_spawnPoints[_currentSpawn].transform.position, 
			_spawnPoints[_currentSpawn].transform.rotation);
	}

	public void SetCurrentSpawnNumber(int number)
	{
		_currentSpawn = number;
		Debug.Log("The spawn was set to " + _currentSpawn);
	}

	public void TriggerSpawn()
	{
		Instantiate(_playerPrefab, _spawnPoints[_currentSpawn].transform.position, Quaternion.Euler(Vector3.zero));
	}
}
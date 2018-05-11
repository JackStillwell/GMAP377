using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _initialSpawn;
    
    private int _spawnNumber;
    private Spawn _spawnManager;

    // Use this for initialization
    void Start ()
    {
	    gameObject.tag = "Spawnpoint";

        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
    }

    public bool IsInitialSpawn()
    {
        return _initialSpawn;
    }

    public void SetSpawnNumber(int index)
    {
        _spawnNumber = index;
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.CompareTag("Player"))
        {
            _spawnManager.SetCurrentSpawnNumber(_spawnNumber); 
        }
    }
}
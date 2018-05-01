using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int _spawnNumber;
    private Spawn _spawnManager;

    // Use this for initialization
    void Start ()
    {
	    gameObject.tag = "Spawnpoint";

        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
    }

    public int GetSpawnNumber()
    {
        return _spawnNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
		if (other.tag=="Player")
        {
            _spawnManager.SetCurrentSpawnNumber(_spawnNumber); 
        }
    }
}
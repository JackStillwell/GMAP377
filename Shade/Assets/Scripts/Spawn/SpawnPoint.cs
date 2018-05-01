using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private int _spawnNumber;

    // Use this for initialization
    void Start ()
    {
	    gameObject.tag = "Spawnpoint";
    }

    public int GetSpawnNumber()
    {
        return _spawnNumber;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player(Clone)")
        {
            GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>().SetCurrentSpawnNumber(_spawnNumber); 
        }
    }
}
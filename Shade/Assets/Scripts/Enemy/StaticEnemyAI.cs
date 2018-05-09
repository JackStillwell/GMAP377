﻿using System.Collections;
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

        if (other.CompareTag("Player"))
        {
            Debug.Log(IsEqualTo(_sight.GetPercievedColor(), GetEnemyColor()));
            Debug.Log("Player Color: " + _sight.GetPercievedColor());
            Debug.Log("Enemy Color: " + GetEnemyColor());
        }
        if (other.CompareTag("Player") && _sight.IsPlayerVisible() && !IsEqualTo(_sight.GetPercievedColor(), GetEnemyColor()))
        {

            _spawnManager.TriggerRespawn(other.gameObject);
        }
    }

    private Color GetEnemyColor()
    {
        return transform.parent.GetComponentInChildren<Renderer>().material.color;
    }

    private static bool IsEqualTo(Color me, Color other)
    {
        bool isRedSimilar = false, isGreenSimilar = false, isBlueSimilar = false;
        if (Mathf.Abs(other.r - me.r) < .1)
            isRedSimilar = true;
        if (Mathf.Abs(other.b - me.b) < .1)
            isBlueSimilar = true;
        if (Mathf.Abs(other.g - me.g) < .1)
            isGreenSimilar = true;

        return isRedSimilar && isBlueSimilar && isGreenSimilar;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private int _currentSpawn;
    [SerializeField] private ColorName _initialPlayerColor;

    private CameraFade fader;

    [SerializeField] private GameObject _playerPrefab;
    private List<GameObject> _spawnPoints;

    // Use this for initialization
    private void Start()
    {
        gameObject.tag = "SpawnManager";

        var spawns = GameObject.FindGameObjectsWithTag("Spawnpoint");

        _spawnPoints = new List<GameObject>(new GameObject[spawns.Length]);

        for (var i = 0; i < spawns.Length; i++)
        {
            _spawnPoints[i] = spawns[i];
            spawns[i].GetComponent<SpawnPoint>().SetSpawnNumber(i);

            if (spawns[i].GetComponent<SpawnPoint>().IsInitialSpawn())
                _currentSpawn = i;
        }

        TriggerSpawn();
    }

    public void SetCurrentSpawnNumber(int number)
    {
        _currentSpawn = number;
    }

    private void TriggerSpawn()
    {
        _playerPrefab.GetComponentInChildren<ColorArray>().SetBaseColor(_initialPlayerColor);
        Instantiate(_playerPrefab,
            _spawnPoints[_currentSpawn].transform.position,
            _spawnPoints[_currentSpawn].transform.rotation);
    }

    public void TriggerRespawn(GameObject player)
    {
        StartCoroutine(sceneReloadCoroutine());

        // player.transform.position = _spawnPoints[_currentSpawn].transform.position;
        //player.transform.rotation = _spawnPoints[_currentSpawn].transform.rotation;
    }

    public IEnumerator sceneReloadCoroutine()
    {

        fader = GameObject.FindObjectOfType<CameraFade>();
        fader.fadeOut(.6f);
        yield return new WaitForSeconds(1);
        Application.LoadLevel(Application.loadedLevel);
    }
}
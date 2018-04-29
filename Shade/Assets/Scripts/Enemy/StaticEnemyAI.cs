using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticEnemyAI : MonoBehaviour
{
	private Collider _sight;
	private bool _playerIsVisible;
	
	// Use this for initialization
	void Start ()
	{
		_sight = GetComponent<Collider>();
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.name == "Player")
			_playerIsVisible = true;
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.name == "Player")
			_playerIsVisible = false;
	}

	public bool IsPlayerVisible()
	{
		return _playerIsVisible;
	}
}

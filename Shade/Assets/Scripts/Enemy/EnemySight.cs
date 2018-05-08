using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
	// Is true only if Player is in FoV and wrong color
	private bool _playerVisible;

	private GameObject _player;
	private MeshCollider _col;

	private Color _percievedPlayerColor;
	private NavMeshAgent _nav;
	private EnemyPatrolAI _patrolAi;

	private RaycastHit[] _hitArray;

	void Start()
	{
		_patrolAi = GetComponent<EnemyPatrolAI> ();
		_nav = GetComponent<NavMeshAgent> ();
		_col = GetComponent<MeshCollider> ();
		_player = GameObject.FindGameObjectWithTag("Player");

		_percievedPlayerColor = GetPlayerColor(_player);
		_player = GameObject.FindGameObjectWithTag("Player");
		_playerVisible = false;
	}
	
	private void OnTriggerEnter(Collider other)
    {
	    if (other.CompareTag("Player"))
	    {
		    _percievedPlayerColor = GetPlayerColor(_player);
		    VisibleCheck();
	    }
    }

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			_percievedPlayerColor = GetPlayerColor(_player);
		}
		/*
		Debug.Log (_playerVisible);
		//Debug.Log (other.gameObject);
		//get the direction of the object
	    Vector3 direction = other.transform.position - transform.position;
	    
	    // get the angle between forward and the object
	    float angle = Vector3.Angle(direction, transform.forward);
		// note: Playervua
		if (angle < _fieldOfViewAngle * 0.5f && !_allObjectsInSight.Contains(other.gameObject) && ChangesColor(other.tag))
			_allObjectsInSight.Add(other.gameObject);
		
		
	    // remove objects in collider but not in sight
	    if (!(angle < _fieldOfViewAngle * 0.5f) && _allObjectsInSight.Contains(other.gameObject)) 
		    _allObjectsInSight.Remove(other.gameObject);

		// after all that, see if player is in the list and check visibility
		if (_allObjectsInSight.Contains (_player)) {
			Debug.Log("visible check 2");
			VisibleCheck();
		} */
	}

	void OnTriggerExit(Collider other)
	{
		/*if (other.gameObject == _player)
		{
			_allObjectsInSight.Remove(other.gameObject);
			_playerVisible = false;
			_patrolAi.NextPoint ();
		}*/

		if (other.tag == "Player")
	    {
		    _playerVisible = false;
	    }
	}

	private void VisibleCheck()
	{
		var direction = _player.transform.position - transform.position;
		_hitArray = Physics.RaycastAll(transform.position, direction.normalized, direction.magnitude);

		foreach (var hit in _hitArray)
		{	
			if (hit.collider.gameObject == _player)
			{
				ColorPercievedUpdate();
				_playerVisible = true;
				break;
			}
		}
	}

	private void ColorPercievedUpdate()
    {
        if (_allObjectsInSight.Count > 1)
        {
			Debug.Log ("Color with glass");
            //now see if player is behind or in front of object
			RaycastHit[] _hitArray1;
            _hitArray1 = Physics.RaycastAll(gameObject.transform.position, _player.transform.position - gameObject.transform.position);

	        if (_hitArray1.Length > 1)
	        {
				foreach (var hitInfo in _hitArray1)
		        {
			        if (hitInfo.transform.gameObject != _player)
			        {
				        // GameObject hitobj = hitInfo.transform.gameObject;
//				        Color objColor = hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color;
//
//				        Color newColor = Color.white;
//
//				        newColor.a = _percievedPlayerColor.a;
//
//				        newColor.r = ((_percievedPlayerColor.r + (objColor.r * objColor.a)) / 2);
//				        newColor.g = ((_percievedPlayerColor.g + (objColor.g * objColor.a)) / 2);
//				        newColor.b = ((_percievedPlayerColor.b + (objColor.b * objColor.a)) / 2);
//
//				        _percievedPlayerColor = newColor;
						_percievedPlayerColor = _player.GetComponentInChildren<Renderer>().material.color;
						Debug.Log (_percievedPlayerColor);
			        }

			        else
			        {
				        break;
			        }
		        }
	        }

	        else
	        {
		        _percievedPlayerColor = _player.GetComponentInChildren<Renderer>().material.color;
	        }
        }

        else
		{
            _percievedPlayerColor = _player.GetComponentInChildren<Renderer>().material.color;
			Debug.Log (_percievedPlayerColor);
        }
    }

    public Color GetPercievedColor()
    {
        return _percievedPlayerColor;
    }

	public bool IsPlayerVisible()
	{
		return _playerVisible;
	}
	
	// Not currently used
	float CalculatePathLength(Vector3 targetPosition)
	{
		NavMeshPath path = new NavMeshPath ();
		if (_nav.enabled)
			_nav.CalculatePath (targetPosition, path);

		Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

		allWayPoints [0] = transform.position;
		allWayPoints [allWayPoints.Length - 1] = targetPosition;

		for (int i = 0; i < path.corners.Length; i++) {
			allWayPoints [i + 1] = path.corners [i];

		
		}
		float pathLength = 0f;
		for (int i = 0; i < allWayPoints.Length - 1; i++) {
		
			pathLength += Vector3.Distance (allWayPoints [i], allWayPoints [i + 1]);

		}
		return pathLength;
	}

	bool ChangesColor(string tag)
	{
		return tag.Equals("Red") ||
		       tag.Equals("Orange") ||
		       tag.Equals("Yellow") ||
		       tag.Equals("Green") ||
		       tag.Equals("Cyan") ||
		       tag.Equals("Violet") ||
		       tag.Equals("Pink") ||
		       tag.Equals("White") ||
		       tag.Equals("Player");
	}

	private Color GetPlayerColor(GameObject player)
	{
		foreach (var renderer in player.GetComponentsInChildren<Renderer>())
		{
			foreach (var mat in renderer.materials)
			{
				if (mat.name == "Player (Instance)")
				{
					return mat.color;
				}
			}
		}

		Debug.Log("GET COLOR FAILURE");
		return Color.white;
	}
}
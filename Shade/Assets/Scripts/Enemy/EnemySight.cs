using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
	// How wide of an angle the object can see
	[SerializeField] private float _fieldOfViewAngle = 120f;

	[SerializeField] private float _seeThroughThreshold = .9f;

	// Is true only if Player is in FoV and wrong color
	private bool _playerVisible;

	private GameObject _player;
	private SphereCollider _col;
	private Color _percievedPlayerColor;
	private NavMeshAgent _nav;
	private EnemyPatrolAI _patrolAi;

	private List<GameObject> _allObjectsInSight;
	private RaycastHit[] _hitArray;

	void Start()
	{
		_patrolAi = GetComponent<EnemyPatrolAI> ();
		_nav = GetComponent<NavMeshAgent> ();
		_col = GetComponent<SphereCollider> ();
		_player = GameObject.FindGameObjectWithTag("Player");

		_percievedPlayerColor = _player.GetComponent<Renderer>().material.color;
		_allObjectsInSight = new List<GameObject>();
		_playerVisible = false;
	}
	
	private void OnTriggerEnter(Collider other)
    {
	    //get the direction of the object
	    Vector3 direction = other.transform.position - transform.position;
	    
	    // get the angle between forward and the object
	    float angle = Vector3.Angle(direction, transform.forward);
	    
	    // check if the object is in sight
	    if (angle < _fieldOfViewAngle * 0.5f) 
		    _allObjectsInSight.Add(other.gameObject);

	    if(_allObjectsInSight.Contains(_player))
		    VisibleCheck();
    }

	private void OnTriggerStay(Collider other)
	{
		//get the direction of the object
	    Vector3 direction = other.transform.position - transform.position;
	    
	    // get the angle between forward and the object
	    float angle = Vector3.Angle(direction, transform.forward);

		if (angle < _fieldOfViewAngle * 0.5f && !_allObjectsInSight.Contains(other.gameObject))
			_allObjectsInSight.Add(other.gameObject);
		
	    // remove objects in collider but not in sight
	    if (!(angle < _fieldOfViewAngle * 0.5f) && _allObjectsInSight.Contains(other.gameObject)) 
		    _allObjectsInSight.Remove(other.gameObject);

		// after all that, see if player is in the list and check visibility
	    if(_allObjectsInSight.Contains(_player))
		    VisibleCheck();
	}

	void OnTriggerExit(Collider other)
	{
		if (other.gameObject == _player)
		{
			_allObjectsInSight.Remove(other.gameObject);
			_playerVisible = false;
			_patrolAi.NextPoint ();
		}
	}

	private void VisibleCheck()
	{
		var direction = _player.transform.position - transform.position;
		_hitArray = Physics.RaycastAll(transform.position, direction.normalized, _col.radius);

		foreach (var hit in _hitArray)
		{
			if (hit.collider.gameObject == _player)
			{
				ColorPercievedUpdate();
				_playerVisible = true;
				break;
			}

			try
			{
				Renderer objectRenderer = hit.collider.gameObject.GetComponent<Renderer>();
				if (objectRenderer.material.color.a > _seeThroughThreshold)
					break;
			}

			catch (MissingComponentException x)
			{
				break;
			}	
		}
	}

	private void ColorPercievedUpdate()
    {
        if (_allObjectsInSight.Count > 1)
        {
            //now see if player is behind or in front of object
            _hitArray = Physics.RaycastAll(gameObject.transform.position, _player.transform.position - gameObject.transform.position);

            foreach (var hitInfo in _hitArray)
            {
                if (hitInfo.transform.gameObject != _player)
                {
                    // GameObject hitobj = hitInfo.transform.gameObject;
                    Color objColor = hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color;

                    Color newColor = Color.white;

                    newColor.a = _percievedPlayerColor.a;

                    newColor.r = ((_percievedPlayerColor.r + (objColor.r * objColor.a)) / 2);
                    newColor.g = ((_percievedPlayerColor.g + (objColor.g * objColor.a)) / 2);
                    newColor.b = ((_percievedPlayerColor.b + (objColor.b * objColor.a)) / 2);

                    _percievedPlayerColor = newColor;
                }

                else
                {
                    break;
                }
            }
        }

        else
        {
            _percievedPlayerColor = _player.GetComponent<Renderer>().material.color;
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
}
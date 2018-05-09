using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    // Is true only if Player is in FoV and wrong color
    private bool _playerVisible;

    private GameObject _player;
    
    private Color _percievedPlayerColor;
    private NavMeshAgent _nav; // may be used in future
    private EnemyPatrolAI _patrolAi;

    private RaycastHit[] _hitArray;

    void Start()
    {
        _patrolAi = GetComponentInParent<EnemyPatrolAI> ();
        _nav = GetComponentInParent<NavMeshAgent> (); // may be used in future
        _player = GameObject.FindGameObjectWithTag("Player");

        _percievedPlayerColor = GetPlayerColor(_player);
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerVisible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            VisibleCheck();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
			VisibleCheck();
        }	
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerVisible = false;
			if (!transform.parent.CompareTag("Enemy_Static"))	
				_patrolAi.NextPoint();        
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
				Debug.Log("Player is Visible");
                ColorPercievedUpdate();
                Debug.Log("The Player's Color Is: " + _percievedPlayerColor);
                _playerVisible = true;
                break;
            }

            if (ChangesColor(hit.collider.tag)) ;  // Do Nothing and Continue
            else
            {
                break;
            }
        }
    }

    private void ColorPercievedUpdate()
    {
        /* Debugging Loop 
        foreach (var hit in _hitArray)
        {
             Debug.Log("Enemy sees: " + hit.collider.name);
        }
        */

        if (_hitArray.Length > 1)
        {
            int index = 0;
            
            if (_percievedPlayerColor == Color.white)
            {
                Color objColor = GetColorValue((ColorName) Enum.Parse(typeof(ColorName), _hitArray[0].collider.tag));
                _percievedPlayerColor = objColor;

                index++;
            }
            
            while(index < _hitArray.Length)
            {
                var hitInfo = _hitArray[index];
                
                if (hitInfo.transform.gameObject != _player)
                {
                    Color objColor = GetColorValue((ColorName) Enum.Parse(typeof(ColorName), hitInfo.collider.tag));

                    Color newColor = new Color();

                    newColor.a = _percievedPlayerColor.a;

                    newColor.r = (_percievedPlayerColor.r + objColor.r) / 2;
                    newColor.g = (_percievedPlayerColor.g + objColor.g) / 2;
                    newColor.b = (_percievedPlayerColor.b + objColor.b) / 2;

                    _percievedPlayerColor = newColor;
                }

                else
                {
                    break;
                }

                index++;
            }
        }

        else
        {
            Debug.Log("Enemy sees only the Player");
            _percievedPlayerColor = GetPlayerColor(_player);
        }
        
        Debug.Log("The Player's Percieved Color Is: " + _percievedPlayerColor);
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
    float CalculatePathLength(Vector3 targetPosition) // may be used in future
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
    
    private Color GetColorValue(ColorName c)
    {
        Color colorValue;
        
        switch (c)
        {
            case ColorName.Red:
                colorValue = Color.red;
                break;
            case ColorName.Orange:
                colorValue = new Color(1, .5f, 0, 1);
                break;
            case ColorName.Yellow:
                colorValue = Color.yellow;
                break;
            case ColorName.Green:
                colorValue = Color.green;
                break;
            case ColorName.Cyan:
                colorValue = Color.cyan;
                break;
            case ColorName.Violet:
                colorValue = new Color(.5f, 0, 1, 1);
                break;
            case ColorName.Pink:
                colorValue = new Color(1, .4f, .87f, 1);
                break;
            case ColorName.White:
                colorValue = new Color(1, 1, 1, 1);
                break;
            case ColorName.Null:
                colorValue = new Color(0, 0, 0, 0);
                break;
            default:
                colorValue = Color.white;
                break;
        }
        return colorValue;
    }
}
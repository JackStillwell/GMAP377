using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    // Is true only if Player is in FoV and wrong color
    private bool _playerVisible;

    private GameObject _player;

    private Color _percievedPlayerColor;
    private Color _enemyColor;
    private NavMeshAgent _nav; // may be used in future
    private EnemyPatrolAI _patrolAi;
    private EnemyAI _enemyAi;

    private RaycastHit[] _hitArray;

    void Start()
    {
        _patrolAi = GetComponentInParent<EnemyPatrolAI>();
        _nav = GetComponentInParent<NavMeshAgent>(); // may be used in future
        _enemyAi = GetComponentInParent<EnemyAI>();

        _player = GameObject.FindGameObjectWithTag("Player");
        _percievedPlayerColor = GetPlayerColor(_player);
        _enemyColor = _enemyAi.EnemyColor();
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
            if (_playerVisible && 
                IsEqualTo(_percievedPlayerColor, _enemyColor) && 
                !transform.parent.CompareTag("Enemy_Static"))
            _patrolAi.NextPoint();

            _playerVisible = false;
        }
    }

    private void VisibleCheck()
    {
        var direction = _player.transform.position - transform.position;
        _hitArray = Physics.RaycastAll(transform.position, direction.normalized, direction.magnitude);

        // ColorPercievedUpdate();

        foreach (var hit in _hitArray)
        {
            // Debug.Log(hit.collider.gameObject);
            if (hit.collider.gameObject == _player)
            {
                //Debug.Log("Player Visible");
                _playerVisible = true;
                ColorPercievedUpdate();
                break;
            }

            if (ChangesColor(hit.collider.tag)) ;
            // Debug.Log("Color Changing Between");  // Do Nothing and Continue

            else if (hit.collider.name == "glasses") ;
            // Debug.Log("Glasses in the Way"); // Do Nothing and Continue

            else
            {
                // Debug.Log("View Obstructed");
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

        var colorUpdateArray = new List<Color>();

        foreach (var hit in _hitArray)
        {
            if (hit.collider.CompareTag("Player"))
                break;
            if (ChangesColor(hit.collider.tag))
                colorUpdateArray.Add(GetColorValue((ColorName) Enum.Parse(typeof(ColorName), hit.collider.tag)));
        }

        if (colorUpdateArray.Count > 0)
        {
            int index = 0;

            if (_percievedPlayerColor == Color.white)
            {
                _percievedPlayerColor = colorUpdateArray[index];

                index++;
            }

            while (index < colorUpdateArray.Count)
            {
                var objColor = colorUpdateArray[index];

                Color newColor = new Color();

                newColor.a = _percievedPlayerColor.a;

                newColor.r = (_percievedPlayerColor.r + objColor.r) / 2;
                newColor.g = (_percievedPlayerColor.g + objColor.g) / 2;
                newColor.b = (_percievedPlayerColor.b + objColor.b) / 2;

                _percievedPlayerColor = newColor;

                index++;
            }
        }

        else
        {
            // Debug.Log("Enemy sees only the Player");
            _percievedPlayerColor = GetPlayerColor(_player);
        }

        // Debug.Log("The Player's Percieved Color Is: " + _percievedPlayerColor);
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
        NavMeshPath path = new NavMeshPath();
        if (_nav.enabled)
            _nav.CalculatePath(targetPosition, path);

        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];


        }

        float pathLength = 0f;
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {

            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);

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
        return _player.GetComponentInChildren<ColorArray>().GetCurrentColor();
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

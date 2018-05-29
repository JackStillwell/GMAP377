using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySight : MonoBehaviour
{
    private EnemyAI _enemyAi;
    private StaticEnemyAI _staticEnemyAI;

    private Color _enemyColor;

    private RaycastHit[] _hitArray;
    private NavMeshAgent _nav; // may be used in future
    private EnemyPatrolAI _patrolAi;

    private Color _percievedPlayerColor;

    private GameObject _player;

    // Is true only if Player is in FoV and wrong color
    private bool _playerVisible;

    private void Start()
    {
        if (this.CompareTag("Enemy_Static"))
        {
            _staticEnemyAI = GetComponentInParent<StaticEnemyAI>();
            _enemyColor = _staticEnemyAI.GetEnemyColor();
        }
        else 
        {
            _enemyAi = GetComponentInParent<EnemyAI>();
            _enemyColor = _enemyAi.EnemyColor();
            _patrolAi = GetComponentInParent<EnemyPatrolAI>();
            _nav = GetComponentInParent<NavMeshAgent>(); // may be used in future
        }
        /*/
        else 
        {
            _enemyColor = Color.white;
        }
*/
        _player = GameObject.FindGameObjectWithTag("Player");
        _percievedPlayerColor = GetPlayerColor(_player);

        _playerVisible = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_enemyAi)
                _nav.isStopped = false;
            VisibleCheck();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            VisibleCheck();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_playerVisible && !this.CompareTag("Enemy_Static") && IsEqualTo(_percievedPlayerColor, _enemyColor))
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

            if (ColorEnum.ChangesColor(hit.collider.tag)) ;
            // Debug.Log("Color Changing Between");  // Do Nothing and Continue

            else if (hit.collider.name == "glasses") ;
            // Debug.Log("Glasses in the Way"); // Do Nothing and Continue

            else
                break;
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
            if (ColorEnum.ChangesColor(hit.collider.tag))
                colorUpdateArray.Add(ColorEnum.GetColorValue((ColorName)Enum.Parse(typeof(ColorName), hit.collider.tag)));
        }

        if (colorUpdateArray.Count > 0)
        {
            var index = 0;

            if (_percievedPlayerColor == Color.white)
            {
                _percievedPlayerColor = colorUpdateArray[index];

                index++;
            }

            while (index < colorUpdateArray.Count)
            {
                var objColor = colorUpdateArray[index];

                var newColor = new Color();

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
    /* 
    private float CalculatePathLength(Vector3 targetPosition) // may be used in future
    {
        var path = new NavMeshPath();
        if (_nav.enabled)
            _nav.CalculatePath(targetPosition, path);

        var allWayPoints = new Vector3[path.corners.Length + 2];

        allWayPoints[0] = transform.position;
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        for (var i = 0; i < path.corners.Length; i++) allWayPoints[i + 1] = path.corners[i];

        var pathLength = 0f;
        for (var i = 0; i < allWayPoints.Length - 1; i++)
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);

        return pathLength;
    }

    */

    private Color GetPlayerColor(GameObject player)
    {
        return _player.GetComponent<ColorArray>().GetCurrentColor();
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
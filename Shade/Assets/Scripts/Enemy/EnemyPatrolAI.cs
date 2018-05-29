using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolAI : MonoBehaviour
{
    private NavMeshAgent _navAgent;
    private int _nextPoint;
    [SerializeField] private Transform[] _wayPoints;
    [SerializeField] private float maxWaitTime = 1f;
    [SerializeField] private float minWaitTime = 1f;
    private bool movingFlag = false;
    private float originalSpeed = 0f;
    private float patrolTimer;
    private Rigidbody rb;
    private float waitTime;

    private void Start()
    {
        _navAgent = gameObject.GetComponent<NavMeshAgent>();
        _navAgent.autoBraking = true;
        _navAgent.updateRotation = true;
        waitTime = Random.Range(minWaitTime, maxWaitTime);
        rb = gameObject.GetComponent<Rigidbody>();
        //Debug.Log(waitTime);
        _navAgent.isStopped = true;
        NextPoint();
    }

    private void Update()
    {
        if (!_navAgent.pathPending && _navAgent.remainingDistance < 4f)
        {
            patrolTimer += Time.deltaTime;

            gameObject.GetComponentInChildren<Animator>().SetBool("isMoving", false);
            _navAgent.isStopped = true;
            if (patrolTimer > waitTime)
            {
                NextPoint();
                patrolTimer = 0f;
                waitTime = Random.Range(minWaitTime, maxWaitTime);
                //Debug.Log (waitTime);
            }
        }
    }


    public void NextPoint()
    {
        _navAgent.isStopped = false;
        gameObject.GetComponentInChildren<Animator>().SetBool("isMoving", true);

        if (_wayPoints.Length == 0)
            return;
        _navAgent.destination = _wayPoints[_nextPoint].position;
        _nextPoint = (_nextPoint + 1) % _wayPoints.Length;
    }
}
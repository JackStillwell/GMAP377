using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour 
{
    private GameObject _player;
    
    [SerializeField] private float _catchDistance = 0.5f;
    private Color _playerColor;
    
    private Color _enemyColor;
    private bool _playerVisible;
    private NavMeshAgent _navAgent;

    private Spawn _spawnManager;

    // Use this for initialization
    void Start() 
    {
        _enemyColor = GetComponentInChildren<Renderer>().material.GetColor("_Color");

        _player = GameObject.FindGameObjectWithTag("Player");

        EnemySight enemySight = GetComponent<EnemySight>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
        
        _playerColor = enemySight.GetPercievedColor();
    }

    // Update is called once per frame
    void Update ()
    {

        _enemyColor = GetComponentInChildren<Renderer>().material.GetColor("_Color");

        _playerColor = GetComponent<EnemySight>().GetPercievedColor();
        _playerVisible = GetComponent<EnemySight> ().IsPlayerVisible();

        if (_playerVisible && !IsEqualTo(_playerColor, _enemyColor))
        {
            Debug.Log("Player: " + _playerColor);
            Debug.Log("Enemy: " + _enemyColor);
            
            _navAgent.updatePosition = true;
            _navAgent.updateRotation = true;
            _navAgent.SetDestination(_player.transform.position);
            
            if (!_navAgent.pathPending && _navAgent.remainingDistance < _catchDistance)
            {
                _spawnManager.TriggerRespawn(_player);
            }
        }
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

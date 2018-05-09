using System.Runtime.InteropServices;
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
    private EnemySight _enemySight;

    private Spawn _spawnManager;

    // Use this for initialization
    void Start()
    {
        _enemyColor = GetEnemyColor(); 
        _enemySight = GetComponentInChildren<EnemySight>();
        _navAgent = GetComponent<NavMeshAgent>();
        
        _player = GameObject.FindGameObjectWithTag("Player");
        
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
        
        _playerColor = _enemySight.GetPercievedColor();
    }

    // Update is called once per frame
    void Update ()
    {
        _playerColor = _enemySight.GetPercievedColor();
        _playerVisible = _enemySight.IsPlayerVisible();
      
        if (_playerVisible && !IsEqualTo(_playerColor, _enemyColor))
        {
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

    private Color GetEnemyColor()
    {
        foreach (var rend in GetComponentsInChildren<Renderer>())
        {
            foreach (var mat in rend.materials)
            {
                if (mat.name == "Enemy (Instance)")
                    return mat.color;
            }
        }
        
        Debug.Log("GET ENEMY COLOR FAILURE");
        return Color.white;
    }
}

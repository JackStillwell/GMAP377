using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _catchDistance = 0.5f;

    private Color _enemyColor;
    private EnemySight _enemySight;
    private NavMeshAgent _navAgent;
    private GameObject _player;
    private Color _playerColor;
    private bool _playerVisible;

    private Spawn _spawnManager;

    private SoundManager sm;


    // Use this for initialization
    private void Start()
    {
        _enemyColor = GetEnemyColor();
        _enemySight = GetComponentInChildren<EnemySight>();
        _navAgent = GetComponent<NavMeshAgent>();

        _player = GameObject.FindGameObjectWithTag("Player");

        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();

        _playerColor = _enemySight.GetPercievedColor();

        sm = GameObject.FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        _playerVisible = _enemySight.IsPlayerVisible();
        _playerColor = _enemySight.GetPercievedColor();

        //Debug.Log("The Enemy Sees Player Color: " + _playerColor);
        //Debug.Log(_enemyColor);

        if (_playerVisible && !IsEqualTo(_playerColor, _enemyColor))
        {
            Debug.Log("I See You! You look: " + _playerColor);
            sm.playSiren();
            _navAgent.updatePosition = true;
            _navAgent.updateRotation = true;
            _navAgent.SetDestination(_player.transform.position);
            gameObject.GetComponentInChildren<Animator>().SetBool("isMoving", true);
            _navAgent.isStopped = false;
            if (!_navAgent.pathPending && _navAgent.remainingDistance < _catchDistance)
            {
                CameraShake.Shake();
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

    public Color EnemyColor()
    {
        return _enemyColor;
    }

    private Color GetEnemyColor()
    {
        foreach (var rend in GetComponentsInChildren<Renderer>())
        foreach (var mat in rend.materials)
            if (mat.name.Contains("Enemy"))
                return mat.color;

        Debug.Log("GET ENEMY COLOR FAILURE");
        return Color.white;
    }
}
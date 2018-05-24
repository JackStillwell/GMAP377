using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private bool _initialSpawn;
    private Spawn _spawnManager;

    private int _spawnNumber;

    // Use this for initialization
    private void Start()
    {
        gameObject.tag = "Spawnpoint";

        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
    }

    public bool IsInitialSpawn()
    {
        return _initialSpawn;
    }

    public void SetSpawnNumber(int index)
    {
        _spawnNumber = index;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) _spawnManager.SetCurrentSpawnNumber(_spawnNumber);
    }
}
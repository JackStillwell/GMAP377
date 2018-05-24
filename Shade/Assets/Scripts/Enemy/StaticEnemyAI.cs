using UnityEngine;

public class StaticEnemyAI : MonoBehaviour
{
    private EnemySight _sight;
    private Spawn _spawnManager;


    private void Start()
    {
        _spawnManager = GameObject.FindGameObjectWithTag("SpawnManager").GetComponent<Spawn>();
        _sight = GetComponent<EnemySight>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player Color: " + _sight.GetPercievedColor());
            Debug.Log("Enemy Color: " + GetEnemyColor());
            Debug.Log("Is Player Visible: " + _sight.IsPlayerVisible());
        }

        if (other.CompareTag("Player") && _sight.IsPlayerVisible() &&
            !IsEqualTo(_sight.GetPercievedColor(), GetEnemyColor()))
        {
            CameraShake.Shake();
            _spawnManager.TriggerRespawn(other.gameObject);
        }
    }

    private Color GetEnemyColor()
    {
        foreach (var mat in transform.GetComponentInChildren<Renderer>().materials)
            if (mat.name.Contains("Enemy") && !mat.name.Contains("FOV"))
                return mat.color;

        Debug.Log("GET ENEMY COLOR FAILURE");
        return Color.white;
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
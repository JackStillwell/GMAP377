using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour
{
    public int winScreen;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(winScreen);
    }
}
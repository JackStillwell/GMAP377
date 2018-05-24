using UnityEngine;
using UnityEngine.SceneManagement;

public class PressEnterToStart : MonoBehaviour
{
    public int nextSceneIndex;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return)) SceneManager.LoadScene(nextSceneIndex);
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressEnterToStart : MonoBehaviour
{
    public int nextSceneIndex;
    public SoundManager sm;

    // Use this for initialization
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            sm.playRoll();
            SceneManager.LoadScene(nextSceneIndex);
        }

    }
}
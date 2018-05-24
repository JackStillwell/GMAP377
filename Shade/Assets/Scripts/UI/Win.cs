using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Win : MonoBehaviour
{
    public int winScreen;

    private CameraFade fader;

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
        {
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        fader = GameObject.FindObjectOfType<CameraFade>();
        fader.fadeOut(.6f);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(winScreen);
    }
}
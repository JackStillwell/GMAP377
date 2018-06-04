using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Win : MonoBehaviour
{
    public int winScreen;

    private CameraFade fader;
    private SoundManager sm;

    // Use this for initialization
    private void Start()
    {

        sm = GameObject.FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sm.playTriumph();
            StartCoroutine(LoadScene());
        }
    }

    public IEnumerator LoadScene()
    {
        fader = GameObject.FindObjectOfType<CameraFade>();
        fader.fadeOut(.6f);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(winScreen);
    }
}
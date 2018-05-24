using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Win : MonoBehaviour 
{

    public CameraFade fader;

	public int winScreen;
	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

    IEnumerator moveScene(Collider other)
    {

        fader = GameObject.FindObjectOfType<CameraFade>();
        fader.fadeOut(fader.fadeTime);
        yield return new WaitForSeconds(fader.fadeTime);
        if (other.CompareTag("Player"))
            UnityEngine.SceneManagement.SceneManager.LoadScene(winScreen);


    }

	void OnTriggerEnter(Collider other)
	{
        StartCoroutine(moveScene(other));
	}
	 
}

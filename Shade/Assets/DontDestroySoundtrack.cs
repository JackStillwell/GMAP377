using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySoundtrack : MonoBehaviour {

    private AudioSource music;

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
        music = GetComponent<AudioSource>();
	}
    private void Update()
    {
        if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex > 2 && UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex < 8)
        {
            if(!music.isPlaying)
            {

                music.Play();

            }



        }
        else
        {

            if(music.isPlaying)
            {

                music.Stop();

            }


        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource rollSound;
    public AudioSource squelchSound;

    public AudioSource splashSound;

    public AudioSource sirenSound;

    public AudioSource triumphSound;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void playRoll()
    {

        rollSound.Play();

    }

    public void playSquelch()
    {

        squelchSound.Play();

    }

    public void playTriumph()
    {

        triumphSound.Play();


    }

    public void playSplash()
    {

        splashSound.Play();

    }

    public void playSiren()
    {

        sirenSound.Play();

    }
	

}

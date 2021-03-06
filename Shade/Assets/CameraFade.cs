﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CameraFade : MonoBehaviour {

	private Image fadeTo;
	public float fadeTime;

	// Use this for initialization
	void Start () {
		
        fadeTo = GetComponent<Image>();
        fadeTo.color = Color.black;
        fadeIn();
	}
	
    public void fadeIn()
    {
        fadeTo.DOColor(Color.clear, fadeTime);

    }

    public void fadeOut(float duration)
    {

        fadeTo.DOColor(Color.black, duration);

    }

}

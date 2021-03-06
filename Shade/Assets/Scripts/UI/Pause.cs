﻿using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause_screen;
    private SoundManager sm;

    // Use this for initialization
    private void Start()
    {
        pause_screen.SetActive(false);
        sm = GameObject.FindObjectOfType<SoundManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    public void Toggle()
    {
        if (Time.timeScale == 0)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            pause_screen.SetActive(false);
        }
        else if (Time.timeScale == 1)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;
            sm.playRoll();
            Time.timeScale = 0;
            pause_screen.SetActive(true);
        }
    }
}
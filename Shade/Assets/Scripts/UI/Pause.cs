using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pause_screen;
    // Use this for initialization
    void Start()
    {
        pause_screen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();

    }

    void Toggle()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            pause_screen.SetActive(false);
        }
        else if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            pause_screen.SetActive(true);
        }
    }
}

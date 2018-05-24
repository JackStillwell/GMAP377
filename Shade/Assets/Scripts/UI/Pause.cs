using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pause_screen;

    // Use this for initialization
    private void Start()
    {
        pause_screen.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Toggle();
    }

    private void Toggle()
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
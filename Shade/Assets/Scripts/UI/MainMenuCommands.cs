using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MainMenuCommands : MonoBehaviour {

    public int startScene;

    public int chapterSelectScene;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void NewGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(startScene);
    }

    public void Continue()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(startScene);
    }

    public void OptionsMenu()
    {

    }

    public void ChapterSelect()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(chapterSelectScene);

    }

    public void Exit()
    {
        Application.Quit();

    }
}

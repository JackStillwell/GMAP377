using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCommands : MonoBehaviour
{
    public int chapterSelectScene;

    public int startScene;

    public void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void NewGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void Continue()
    {
        SceneManager.LoadScene(startScene);
    }

    public void OptionsMenu()
    {
    }

    public void ChapterSelect()
    {
        SceneManager.LoadScene(chapterSelectScene);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCommands : MonoBehaviour
{
    public int chapterSelectScene;

    public int startScene;
    public int menuScene;

    public int credits;

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

    public void UnPause(GameObject self)
    {
        self.SetActive(false);
    }

    public void backToMenu()
    {
        SceneManager.LoadScene(menuScene);
    }

    public void OptionsMenu()
    {
        SceneManager.LoadScene(credits);
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
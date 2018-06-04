using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterLoad : MonoBehaviour
{

    private void Start()
    {
        Cursor.visible = true;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(1);
    }

    public void chapterLoad(int i)
    {
		SceneManager.LoadScene(i);
    }

}
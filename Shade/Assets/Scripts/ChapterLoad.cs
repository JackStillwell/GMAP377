using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoad : MonoBehaviour {

    private void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(1);

        }
    }

    public void chapterLoad(int i)
    {


        UnityEngine.SceneManagement.SceneManager.LoadScene(i);

    }
}

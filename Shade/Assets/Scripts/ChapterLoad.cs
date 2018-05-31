using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoad : MonoBehaviour {

	public void chapterLoad(int i)
    {


        UnityEngine.SceneManagement.SceneManager.LoadScene(i);

    }
}

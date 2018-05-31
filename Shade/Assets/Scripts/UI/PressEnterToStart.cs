using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressEnterToStart : MonoBehaviour {

    public int nextSceneIndex;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetKey(KeyCode.Return))
        {

            UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);

        }
		
	}
}

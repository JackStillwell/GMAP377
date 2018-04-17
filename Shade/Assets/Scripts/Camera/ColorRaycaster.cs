using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRaycaster : MonoBehaviour {


    public GameObject playerObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    bool colorCheck()
    {
        Ray r = new Ray(gameObject.transform.position, (playerObject.transform.position - gameObject.transform.position));

        return false;

    }
}

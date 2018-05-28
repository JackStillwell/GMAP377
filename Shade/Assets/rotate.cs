using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour {

    public float speed;
    private Transform t;

	// Use this for initialization
	void Start () {
        t = gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
        t.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
	}
}

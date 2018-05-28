using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EntryCameraMove : MonoBehaviour {

    public float animTime = 4;

	// Use this for initialization
	void Start () {
        gameObject.transform.DOMove(new Vector3(38.2f, 13.1f, 11.4f), animTime);
        gameObject.transform.DORotate(new Vector3(18.42f, -109.09f, -3.549f), animTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

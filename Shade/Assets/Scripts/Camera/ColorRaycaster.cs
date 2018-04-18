using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRaycaster : MonoBehaviour {


    public PlayerColorController target;
    private RaycastHit hitInfo;

    public Color playerView;

	// Use this for initialization
	void Start () {

        //playerMat = playerObject.GetComponent<Material>();
    }
	
	// Update is called once per frame
	void Update () {
        colorCheck();
	}

    bool colorCheck()
    {
        Ray rayDirection = new Ray(gameObject.transform.position, (target.transform.position - gameObject.transform.position));

        Physics.Raycast(rayDirection, out hitInfo, (target.transform.position - gameObject.transform.position).magnitude);

        if(hitInfo.transform.gameObject != target)
        {

            //get the color of both objects and then math needs to happen
            Color playerColor = target.realColor;
            GameObject hitobj = hitInfo.transform.gameObject;
            Color objColor = hitobj.GetComponent<MeshRenderer>().material.color;

            Color newColor = Color.white;

            newColor.a = playerColor.a;

            newColor.r = ((playerColor.r + (objColor.r * objColor.a)) / 2);
            newColor.g = ((playerColor.g + (objColor.g * objColor.a)) / 2);
            newColor.b = ((playerColor.b + (objColor.b * objColor.a)) / 2);

            playerView = newColor;

            Debug.Log(playerView.ToString());

            return true;
        }
        else
        {
            Debug.Log(playerView.ToString());
            return false;
        }
        

    }
}

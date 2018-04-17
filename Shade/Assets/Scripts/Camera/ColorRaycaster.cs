using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRaycaster : MonoBehaviour {


    public GameObject playerObject;
    public PlayerColorController playerObjColorController;
    private RaycastHit hitInfo;
    public Material playerMat;

	// Use this for initialization
	void Start () {

        //playerMat = playerObject.GetComponent<Material>();
        playerObjColorController = playerObjColorController.GetComponent<PlayerColorController>();
    }
	
	// Update is called once per frame
	void Update () {
        colorCheck();
	}

    bool colorCheck()
    {
        Ray rayDirection = new Ray(gameObject.transform.position, (playerObject.transform.position - gameObject.transform.position));

        Physics.Raycast(rayDirection, out hitInfo, (playerObject.transform.position - gameObject.transform.position).magnitude);

        if(hitInfo.transform.gameObject != playerObject)
        {

            //get the color of both objects and then math needs to happen
            Color playerColor = playerMat.color;
            GameObject hitobj = hitInfo.transform.gameObject;
            Color objColor = hitobj.GetComponent<MeshRenderer>().material.color;

            Color newColor = Color.white;

            newColor.a = playerColor.a;

            newColor.r = ((playerColor.r + (objColor.r * objColor.a)) / 2);
            newColor.g = ((playerColor.g + (objColor.g * objColor.a)) / 2);
            newColor.b = ((playerColor.b + (objColor.b * objColor.a)) / 2);

            playerObjColorController.changePlayerColor(newColor);

            return true;
        }
        else
        {
            return false;
        }
        

    }
}

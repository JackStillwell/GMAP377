using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRaycaster : MonoBehaviour 
{
    public PlayerColorController target;
    private RaycastHit hitInfo;

    public Color playerView;

    private List<GameObject> currentlyColliding;

	// Use this for initialization
	void Start () 
    {
        //playerMat = playerObject.GetComponent<Material>();
        currentlyColliding = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () 
    {
	}

    /// <summary>
    /// Adds the new object to the currentlyColliding list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("beep");
        currentlyColliding.Add(other.gameObject);
        colorCheck();
    }

    /// <summary>
    /// Removes exiting object from the currentlyColliding list.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        currentlyColliding.Remove(other.gameObject);
        colorCheck();
    }

    public bool colorCheck()
    {
        if (currentlyColliding.Count >= 1)
        {
            bool foundTarget = false;

            foreach(GameObject g in currentlyColliding)
            {                
                if(g.name == target.name)
                {
                    foundTarget = true;
                    break;
                }       
            }

            if (foundTarget)
            {
                Debug.Log("beep");
                //now see if player is behind or in front of object
                Physics.Raycast(gameObject.transform.position, (target.transform.position - gameObject.transform.position), out hitInfo);
                if (hitInfo.transform.gameObject.name != target.name)
                {
                    
                    //get the color of both objects and then math needs to happen
                    Color playerColor = target.realColor;
                    // GameObject hitobj = hitInfo.transform.gameObject;
                    Color objColor = hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color;

                    Color newColor = Color.white;

                    newColor.a = playerColor.a;

                    newColor.r = ((playerColor.r + (objColor.r * objColor.a)) / 2);
                    newColor.g = ((playerColor.g + (objColor.g * objColor.a)) / 2);
                    newColor.b = ((playerColor.b + (objColor.b * objColor.a)) / 2);

                    Debug.Log("YES");

                    playerView = newColor;

                    return true;
                }
                else return false;
            }
            else return false;
            
        }
        else
        {
            return false;
        }


    }
}

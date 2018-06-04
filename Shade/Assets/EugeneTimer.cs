using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EugeneTimer : MonoBehaviour {


    //get the alpha channel here
  //  public ColorArray eugene;

    public EugeneFill filledObj;
    public GameObject toInstantiate;

    

    // Use this for initialization
    void Start () {
        
	}

    public EugeneFill addColor(Color c, float totalTime)
    {
        //create an image at 100% AT THE BACK of the color image
        
        // create a filled image
        GameObject toAdd = Instantiate(toInstantiate, gameObject.transform) as GameObject;
        filledObj = toAdd.GetComponent<EugeneFill>();
        filledObj.fill.color = c;
        filledObj.timeTotal = totalTime;
        
        return filledObj;
    }

 
	
	// Update is called once per frame
	void Update () {
        
        

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EugeneTimer : MonoBehaviour {


    //get the alpha channel here
  //  public ColorArray eugene;

    public EugeneFill filledObj;
    public GameObject toInstantiate;

    public List<EugeneFill> allFills;

    // Use this for initialization
    void Start () {
        allFills = new List<EugeneFill>();
	}

    public EugeneFill addColor(Color c, float totalTime)
    {
        //create an image at 100% AT THE BACK of the color image
        
        // create a filled image
        GameObject toAdd = Instantiate(toInstantiate, gameObject.transform) as GameObject;
        filledObj = toAdd.GetComponent<EugeneFill>();
        filledObj.fill.color = c;
        filledObj.interiorColor = c;
        filledObj.timeTotal = totalTime;
        filledObj.timeRemaining = totalTime;
        allFills.Add(filledObj);
        filledObj.parent = this;
        Debug.Log(allFills.Count);

        recolor();


        return filledObj;
    }

    void recolor()
    {
        //create array in order
        EugeneFill[] ef = new EugeneFill[allFills.Count];

        List<EugeneFill> backup = new List<EugeneFill>(allFills);
        int fills = allFills.Count;

        int counter = 0;

        while(counter < (fills))
        {
            EugeneFill currentLeast = null;

            foreach(EugeneFill fillRemaining in allFills)
            {
                if(currentLeast == null)
                {

                    currentLeast = fillRemaining;

                }
                else if (fillRemaining.timeRemaining < currentLeast.timeRemaining)
                {

                    currentLeast = fillRemaining;

                }


            }

            ef[counter] = currentLeast;
            allFills.Remove(currentLeast);
            counter++;
            
        }
        allFills = new List<EugeneFill>(backup);
        backup.Clear();

        for(int i = 0; i < ef.Length; i++)
        {

            if (ef[i] != null)
            {
                int colorCount = 0;

                float rTotal = 0;
                float gTotal = 0;
                float bTotal = 0;

                for (int j = i; j < ef.Length; j++)
                {
                    if (ef[j] != null)
                    {
                        rTotal += ef[j].interiorColor.r;
                        gTotal += ef[j].interiorColor.g;
                        bTotal += ef[j].interiorColor.b;
                        colorCount += 1;
                    }

                }

                rTotal = rTotal / colorCount;
                gTotal = gTotal / colorCount;
                bTotal = bTotal / colorCount;
                Color newFill = new Color(rTotal, gTotal, bTotal, 1);
                ef[i].fill.color = newFill;
            }

        }








    }
 
	
	// Update is called once per frame
	void Update () {
        
        

    }
}

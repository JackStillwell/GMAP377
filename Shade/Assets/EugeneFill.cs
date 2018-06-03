using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EugeneFill : MonoBehaviour {

    public Image fill;

    public float timeTotal;
    private float timeRemaining;
    public bool decreasing;

	// Use this for initialization
	void Start () {
        fill = GetComponent<Image>();
        fill.type = Image.Type.Filled;
        timeRemaining = timeTotal;
        fill.fillAmount = 1f;
        decreasing = false;
        transform.SetAsFirstSibling();
	}

    private void Update()
    {
        if (decreasing)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0)
            {
                GameObject.Destroy(gameObject);

            }
            else
            {
                float percent = timeRemaining / timeTotal;
                
                fill.fillAmount = percent;
            }



        }
        else
        {

            timeRemaining = timeTotal;

        }

    }

}

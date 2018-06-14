using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EugeneFill : MonoBehaviour {

    public Image fill;
    public EugeneTimer parent;
    public float timeTotal;
    public float timeRemaining;
    public bool decreasing;

    public Color interiorColor;

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
                parent.allFills.Remove(this);
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

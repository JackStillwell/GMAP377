using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ColoredLight : MonoBehaviour
{
	Color originalColor;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        originalColor = other.gameObject.GetComponent<Renderer>().material.color;
        Color lightColor = transform.GetComponent<Light>().color;
        Color newColor = Color.white;

        newColor.r = ((originalColor.r + (lightColor.r * lightColor.a)) / 2);
        newColor.g = ((originalColor.g + (lightColor.g * lightColor.a)) / 2);
        newColor.b = ((originalColor.b + (lightColor.b * lightColor.a)) / 2);

        other.gameObject.GetComponent<Renderer>().material.color = newColor;
         */


        //So this should work fine, but I can't figure out how to get the enum to be a global value so that I can reference it.
        ColorName colorName = (ColorName) Enum.Parse(typeof(ColorName), this.transform.tag);
         
        other.gameObject.GetComponent<ColorArray>().addColorToLayer("Spotlight", colorName);
    }

    private void OnTriggerExit(Collider other)
    {
		other.gameObject.GetComponent<ColorArray>().removeColorFromLayer("Spotlight");
    }
}

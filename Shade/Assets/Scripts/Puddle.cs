using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puddle : MonoBehaviour
{
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
        if (other.tag == "Player")
        {
            Debug.Log("Beep");
            ColorName colorName = (ColorName)Enum.Parse(typeof(ColorName), this.transform.tag);

            other.gameObject.GetComponent<ColorArray>().addColorToLayer("Puddle", colorName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Boop");
            DelayColorRemove(other);
        }
    }

    IEnumerator DelayColorRemove(Collider other)
    {
        Debug.Log("WAKE ME UP");
        yield return new WaitForSeconds(2);
        other.gameObject.GetComponent<ColorArray>().removeColorFromLayer("Puddle");
    }

}

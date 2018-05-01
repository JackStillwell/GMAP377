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

    private void onTriggerEnter(Collider other)
    {
        ColorName colorName = (ColorName)Enum.Parse(typeof(ColorName), this.transform.tag);

        other.gameObject.GetComponent<ColorArray>().addColorToLayer("Puddle", colorName);
        
    }

    private void OnTriggerExit(Collider other)
    {
        DelayColorRemove(other);
    }

    IEnumerator DelayColorRemove(Collider other)
    {
        yield return new WaitForSeconds(30);
		other.gameObject.GetComponent<ColorArray>().removeColorFromLayer("Puddle");
    }

}

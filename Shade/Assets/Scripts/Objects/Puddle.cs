using System;
using System.Collections;
using UnityEngine;

public class Puddle : MonoBehaviour
{

    private int _colorArrayIndex;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ColorName colorName = (ColorName)Enum.Parse(typeof(ColorName), transform.tag);

            _colorArrayIndex = other.gameObject.GetComponent<ColorArray>().AddColor(colorName);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(DelayColorRemove(other));
        }
    }

    IEnumerator DelayColorRemove(Collider other)
    {
        yield return new WaitForSecondsRealtime(2);
        Debug.Log("MUAKE IT RAOUNN BEIEBBBYYY");
        other.gameObject.GetComponent<ColorArray>().RemoveColor(_colorArrayIndex);
    }
}
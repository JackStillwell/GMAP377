using System;
using System.Collections;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    
    private ColorName _colorName;
    [SerializeField] private int effectDuration;

    private void Start()
    {
        _colorName = (ColorName)Enum.Parse(typeof(ColorName), transform.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<ColorArray>().AddColor(_colorName);
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
        yield return new WaitForSecondsRealtime(effectDuration);
        Debug.Log("MUAKE IT RAOUNN BEIEBBBYYY");
        other.gameObject.GetComponent<ColorArray>().RemoveColor(_colorName);
    }
}
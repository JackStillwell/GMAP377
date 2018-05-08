using System;
using System.Collections;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    
    private ColorName _colorName;
    [SerializeField] private int effectDuration;

    private ColorArray playerArray;

    private void Start()
    {
        _colorName = (ColorName)Enum.Parse(typeof(ColorName), transform.tag);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ColorArray>().AddColor(_colorName);
            playerArray = other.gameObject.GetComponentInChildren<ColorArray>();
        }
        else if (this.CompareTag("Untagged"))
        {
            Debug.LogError("Tag the puddle, dumbass!");
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
        
        playerArray.RemoveColor(_colorName);
        playerArray = null;

    }
}
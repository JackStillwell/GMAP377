using System;
using UnityEngine;

public class ColoredLight : MonoBehaviour
{
    private ColorName _colorName;

    private void Start()
    {
        _colorName = (ColorName) Enum.Parse(typeof(ColorName), transform.tag);
    }

    private void OnTriggerEnter(Collider other)
    {
        //So this should work fine, but I can't figure out how to get the enum to be a global value so that I can reference it.
        if (other.CompareTag("Player")) other.gameObject.GetComponentInChildren<ColorArray>().AddColor(_colorName);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) other.gameObject.GetComponentInChildren<ColorArray>().RemoveColor(_colorName);
    }
}
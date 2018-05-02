﻿using System;
using UnityEngine;

public class ColoredLight : MonoBehaviour
{
    private int _colorArrayIndex;

    private void OnTriggerEnter(Collider other)
    {
        //So this should work fine, but I can't figure out how to get the enum to be a global value so that I can reference it.
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
            other.gameObject.GetComponent<ColorArray>().RemoveColor(_colorArrayIndex);
        }
    }
}

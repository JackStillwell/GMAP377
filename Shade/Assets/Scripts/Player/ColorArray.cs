﻿using System.Collections.Generic;
using UnityEngine;

public enum ColorName { Red, Orange, Yellow, Green, Cyan, Violet, Pink, Null, White };

public class ColorArray : MonoBehaviour
{
    private List<ColorName> _colorModifiers;

    [SerializeField] private Color _baseColor = Color.white;

    private void Start()
    {
        _colorModifiers = new List<ColorName>();
        ApplyColor(_baseColor);
    }

    private void ChangePlayerColor()
	{
        ApplyColor(CombineColors());
	}

    private void ApplyColor(Color inColor)
    {
        bool setColor = false;
	    foreach (var rend in GetComponentsInParent<Renderer>())
	    {
	        foreach (var mat in rend.materials)
	        {
	            if (mat.name == "Player (Instance)")
	            {
	                mat.color = inColor;
	                setColor = true;
	            }
	        }
	    }

        if (!setColor)
        {
            Debug.Log("APPLY COLOR FAILURE");
        }
	}

    private Color CombineColors()
    {
        Color combinedColor = new Color();

        // add all colors in the array
        if (_colorModifiers.Count > 0)
        {
            if (_baseColor != Color.white)
            {
                combinedColor = _baseColor;
            }
            
            foreach (var color in _colorModifiers)
            {
                Color value = GetColorValue(color);

                combinedColor.r = (combinedColor.r + value.r) / 2;
                combinedColor.g = (combinedColor.g + value.g) / 2;
                combinedColor.b = (combinedColor.b + value.b) / 2;
            }

            return combinedColor;
        }

        return _baseColor;
    }

    private Color GetColorValue(ColorName c)
    {
        Color colorValue;
        
        switch (c)
        {
            case ColorName.Red:
                colorValue = Color.red;
                break;
            case ColorName.Orange:
                colorValue = new Color(1, .5f, 0, 1);
                break;
            case ColorName.Yellow:
                colorValue = Color.yellow;
                break;
            case ColorName.Green:
                colorValue = Color.green;
                break;
            case ColorName.Cyan:
                colorValue = Color.cyan;
                break;
            case ColorName.Violet:
                colorValue = new Color(.5f, 0, 1, 1);
                break;
            case ColorName.Pink:
                colorValue = new Color(1, .4f, .87f, 1);
                break;
            case ColorName.White:
                colorValue = new Color(1, 1, 1, 1);
                break;
            case ColorName.Null:
                colorValue = new Color(0, 0, 0, 0);
                break;
            default:
                colorValue = Color.white;
                break;
        }
        return colorValue;
    }

    public void AddColor(ColorName inColor)
    {
        _colorModifiers.Add(inColor);
        ChangePlayerColor();
    }
    
    public void RemoveColor(ColorName inColor)
    {
        _colorModifiers.Remove(inColor);
        ChangePlayerColor();
    }
}

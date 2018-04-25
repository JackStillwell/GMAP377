using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColorName { Red, Orange, Yellow, Green, Cyan, Violet, Pink, Null, White };

public class ColorArray : MonoBehaviour
{
   
    private ColorName glassLayer = ColorName.Null;
    private ColorName puddleLayer = ColorName.Null;
    private ColorName spotlightLayer = ColorName.Null;
    //public Array<ColorName> colorModifiers = new List<ColorName>(){glassLayer, puddleLayer, spotlightLayer};
    private ColorName[] colorModifiers = new ColorName[3];
    // Use this for initialization
    void Start()
    {
        colorModifiers[0] = ColorName.White;
        colorModifiers[1] = ColorName.White;
        colorModifiers[2] = ColorName.White;
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void changePlayerColor()
	{
		if (colorModifiers[1] != ColorName.Null)
		{
			applyColor(1);
			if (colorModifiers[2] != ColorName.Null)
				applyColor(2);
		}
		else if (colorModifiers[2] != ColorName.Null)
			applyColor(2);

	}

	public void applyColor(int layer)
	{
		this.transform.GetComponent<Renderer>().material.color = GetColorValue(colorModifiers[layer]);
	}
    public Color combineColors()
    {
        int nonNullColors = 0;
        for (int i = 0; i < colorModifiers.Length; i++)
        {
            if (colorModifiers[i] != ColorName.Null)
                nonNullColors++;
        }

        Color glassColor = GetColorValue(colorModifiers[0]);
        Color puddleColor = GetColorValue(colorModifiers[1]);
        Color spotlightColor = GetColorValue(colorModifiers[2]);

        Color combinedColor = new Color();

        combinedColor.r = ((glassColor.r * glassColor.a) + (puddleColor.r * puddleColor.a) + (spotlightColor.r * spotlightColor.a));
        combinedColor.g = ((glassColor.g * glassColor.a) + (puddleColor.g * puddleColor.a) + (spotlightColor.g * spotlightColor.a));
        combinedColor.b = ((glassColor.b * glassColor.a) + (puddleColor.b * puddleColor.a) + (spotlightColor.b * spotlightColor.a));

        return combinedColor;
    }

    private Color GetColorValue(ColorName c)
    {
        Color colorValue = new Color(0, 0, 0, 0);
        Debug.Log("Glass: " + colorModifiers[0]);
        Debug.Log("Puddle: " + colorModifiers[1]);
        Debug.Log("Light: " + colorModifiers[2]);
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

    public void addColorToLayer(string layerName, ColorName layerColor)
    {
        switch (layerName)
        {
            case "Glass":
                colorModifiers[0] = layerColor;
                break;
            case "Puddle":
                colorModifiers[1] = layerColor;
				changePlayerColor();
                break;
            case "Spotlight":
                colorModifiers[2] = layerColor;
				changePlayerColor();
                break;
            default:
                Debug.Log("no plz");
                break;
        }
    }
    public void removeColorFromLayer(string layerName)
    {
        switch (layerName)
        {
            case "Glass":
                colorModifiers[0] = ColorName.Null;
                break;
            case "Puddle":
                colorModifiers[1] = ColorName.Null;
				changePlayerColor();
                break;
            case "Spotlight":
                colorModifiers[2] = ColorName.Null;
				changePlayerColor();
                break;
            default:
                Debug.Log("no");
                break;
        }
    }
}

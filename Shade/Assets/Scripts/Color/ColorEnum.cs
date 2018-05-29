using UnityEngine;

public enum ColorName
{
    Red,
    Orange,
    Yellow,
    Green,
    Cyan,
    Blue,
    Violet,
    Pink,
    Null,
    White
}

public class ColorEnum
{
    public static Color GetColorValue(ColorName c)
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
            case ColorName.Blue:
                colorValue = new Color(0,0,1,0);
                break;
            default:
                colorValue = Color.white;
                break;
        }

        return colorValue;
    }
    
    public static bool ChangesColor(string tag)
    {
        return tag.Equals("Red") ||
               tag.Equals("Orange") ||
               tag.Equals("Yellow") ||
               tag.Equals("Green") ||
               tag.Equals("Cyan") ||
               tag.Equals("Blue") ||
               tag.Equals("Violet") ||
               tag.Equals("Pink") ||
               tag.Equals("White") ||
               tag.Equals("Player");
    }
}

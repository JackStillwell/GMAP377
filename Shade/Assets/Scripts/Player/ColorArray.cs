using System.Collections.Generic;
using UnityEngine;

public class ColorArray : MonoBehaviour
{
    [SerializeField] private float _alphaValue = .9f;

    [SerializeField] private Color _baseColor = Color.white;
    private List<ColorName> _colorModifiers;
    private Color _currentColor;

    private void Start()
    {
        _colorModifiers = new List<ColorName>();
        ApplyColor(_baseColor);
        _currentColor = _baseColor;
    }

    private void ChangePlayerColor()
    {
        ApplyColor(CombineColors());
    }

    private void ApplyColor(Color inColor)
    {
        var setColor = false;
        foreach (var rend in GetComponentsInParent<Renderer>())
        foreach (var mat in rend.materials)
            if (mat.name == "Player (Instance)")
            {
                mat.color = inColor;
                setColor = true;
            }

        if (!setColor)
        {
            Debug.Log("APPLY COLOR FAILURE");
            return;
        }

        _currentColor = inColor;
        // Debug.Log("Player Color is: " + inColor);
    }

    private Color CombineColors()
    {
        var combinedColor = new Color();

        // add all colors in the array
        if (_colorModifiers.Count > 1)
        {
            if (_baseColor != Color.white)
                combinedColor = _baseColor;

            else
                combinedColor = ColorEnum.GetColorValue(_colorModifiers[0]);

            for (var i = 1; i < _colorModifiers.Count; i++)
            {
                var value = ColorEnum.GetColorValue(_colorModifiers[i]);
                combinedColor = MixColors(combinedColor, value);
            }

            return combinedColor;
        }

        if (_colorModifiers.Count == 1)
        {
            if (_baseColor == Color.white) return ColorEnum.GetColorValue(_colorModifiers[0]);

            return MixColors(_baseColor, ColorEnum.GetColorValue(_colorModifiers[0]));
        }

        return _baseColor;
    }

    private Color MixColors(Color one, Color two)
    {
        var combinedColor = new Color();

        combinedColor.r = (one.r + two.r) / 2;
        combinedColor.g = (one.g + two.g) / 2;
        combinedColor.b = (one.b + two.b) / 2;
        combinedColor.a = _alphaValue;

        return combinedColor;
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

    public Color GetCurrentColor()
    {
        return _currentColor == new Color() ? _baseColor : _currentColor;
    }

    public void SetBaseColor(ColorName inColor)
    {
        _baseColor = ColorEnum.GetColorValue(inColor);
    }
}
using System;
using System.Collections;
using UnityEngine;

public class Puddle : MonoBehaviour
{
    private ColorName _colorName;
    [SerializeField] private int _effectDuration;

    private EugeneTimer timer;
    private EugeneFill correspondingFill;
    private SoundManager sm;

    private void Start()
    {
        try
        {
            _colorName = (ColorName) Enum.Parse(typeof(ColorName), transform.tag);
        }
        catch
        {
            Debug.LogError("Tag the puddle, dumbass!");
        }


        timer = GameObject.FindObjectOfType<EugeneTimer>();
        sm = GameObject.FindObjectOfType<SoundManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ColorArray>().AddColor(_colorName);
            correspondingFill = timer.addColor(ColorEnum.GetColorValue(_colorName), _effectDuration);
            sm.playSplash();
        }
        else if (CompareTag("Untagged")) Debug.LogError("Tag the puddle, dumbass!");
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<ColorArray>()
                .RemoveColorAfterSeconds(_colorName, _effectDuration);
            correspondingFill.decreasing = true;
        }

    }
}
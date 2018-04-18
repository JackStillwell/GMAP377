using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour {

    public Material playerMat;

    public Color realColor;

    public void Start()
    {
        playerMat = gameObject.GetComponent<MeshRenderer>().material;
        realColor = playerMat.color;
    }

    /// <summary>
    /// Changes the player object's albedo tint to the given Color
    /// </summary>
    /// <param name="newColor">The new Color to tint to</param>
	public void changePlayerColor(Color newColor)
    {
        realColor = newColor;
        playerMat.color = newColor;


    }
}

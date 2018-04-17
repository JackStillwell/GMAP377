using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorController : MonoBehaviour {

    public Material playerMat;

    public void Start()
    {
        playerMat = gameObject.GetComponent<Material>();
    }

    /// <summary>
    /// Changes the player object's albedo tint to the given Color
    /// </summary>
    /// <param name="newColor">The new Color to tint to</param>
	public void changePlayerColor(Color newColor)
    {
        playerMat.color = newColor;


    }
}

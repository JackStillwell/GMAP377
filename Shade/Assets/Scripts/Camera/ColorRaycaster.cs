using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRaycaster : MonoBehaviour 
{
    private GameObject _player;
    private RaycastHit[] _hitInfoArray;

    private Color _percievedPlayerColor;

    private List<GameObject> _currentlyColliding;

	// Use this for initialization
	void Start() 
    {
        _currentlyColliding = new List<GameObject>();
        
        _player = GameObject.Find("Player(Clone)");
        
        _percievedPlayerColor = _player.GetComponent<Renderer>().material.color;
    }

    /// <summary>
    /// Adds the new object to the currentlyColliding list
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        _currentlyColliding.Add(other.gameObject);

        if(_currentlyColliding.Contains(_player))
            ColorCheck();
    }

    /// <summary>
    /// Removes exiting object from the currentlyColliding list.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerExit(Collider other)
    {
        _currentlyColliding.Remove(other.gameObject);
        
        if(_currentlyColliding.Contains(_player))
            ColorCheck();
    }

    private void ColorCheck()
    {
        if (_currentlyColliding.Count > 1)
        {
            //now see if player is behind or in front of object
            _hitInfoArray = Physics.RaycastAll(gameObject.transform.position, _player.transform.position - gameObject.transform.position);

            foreach (var hitInfo in _hitInfoArray)
            {
                if (hitInfo.transform.gameObject != _player)
                {
                    // GameObject hitobj = hitInfo.transform.gameObject;
                    Color objColor = hitInfo.transform.gameObject.GetComponent<MeshRenderer>().material.color;

                    Color newColor = Color.white;

                    newColor.a = _percievedPlayerColor.a;

                    newColor.r = ((_percievedPlayerColor.r + (objColor.r * objColor.a)) / 2);
                    newColor.g = ((_percievedPlayerColor.g + (objColor.g * objColor.a)) / 2);
                    newColor.b = ((_percievedPlayerColor.b + (objColor.b * objColor.a)) / 2);

                    _percievedPlayerColor = newColor;
                }

                else
                {
                    break;
                }
            }
        }

        else
        {
            _percievedPlayerColor = _player.GetComponent<Renderer>().material.color;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject FollowObj;

    [SerializeField]
    private float _camDistance = 10f;
    private float _currentX;

    private Rigidbody _followObjRigidbody;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _followObjRigidbody = FollowObj.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        //take in axis info from our mouse
        // I honestly don't know why it's swapped but this is correct
        _currentX -= Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        // Clamp rotation to 90 degrees
        _currentX = Mathf.Clamp(_currentX, 0f, 90f);
        
        //set our z distance away from the PC
        Vector3 dir = new Vector3(0, 0, -_camDistance);
        
        //set the rotation of the camera
        Vector3 followObjEuler = _followObjRigidbody.rotation.eulerAngles;
        
        // takes the y and z value from followObj but sets x value based on Mouse input
        Quaternion rotation = Quaternion.Euler(_currentX, followObjEuler.y, followObjEuler.z);

        //lerp camera positon to new position
        //multiplying a position by a quaternion rotates the positon
        transform.position = Vector3.Lerp(
            transform.position, 
            FollowObj.transform.position + rotation * dir, 
            8f * Time.deltaTime);
        transform.LookAt(FollowObj.transform.position);
    }
}
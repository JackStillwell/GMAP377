using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject followObj;

    [SerializeField]
    private float camDistance = 10f;
    private float currentY;
    private float currentX=180f;

    private Rigidbody followObjRigidbody;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        followObjRigidbody = followObj.GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        //take in axis info from our mouse
        currentX += Input.GetAxis("Mouse X");
        currentY -= Input.GetAxis("Mouse Y");
    }

    private void FixedUpdate()
    {
        //set our z distance away from the PC
        Vector3 dir = new Vector3(0, 0, camDistance);
        //set the rotation of the camera
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        //lerp camera positon to new position                 -define new position as pc position, offset by the rotation, 
        //Move Backwards 

        //multiplying a position by a quaternion rotates the positon
        transform.position = Vector3.Lerp(transform.position, followObj.transform.position + rotation * dir, 8f * Time.deltaTime);
        transform.LookAt(followObj.transform.position);
    }
}
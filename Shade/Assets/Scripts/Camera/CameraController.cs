using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject FollowObj;

    [SerializeField] private float _maxCamDistance = 10f;
    private float _camDistance = 5f;
    [SerializeField] private float _minCamDistance = 3f;

    [SerializeField] private float _scrollSpeed = 1f;
    [SerializeField] private float _tiltSpeed = 1f;
    
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
        _currentX -= Input.GetAxis("Mouse Y") * _tiltSpeed;
        _camDistance -= Input.GetAxis("Mouse ScrollWheel") * _scrollSpeed;
    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if(!other.transform.parent.CompareTag("Player"))
//        _camDistance -= 100f;
//    }
//
//    private void OnTriggerStay(Collider other)
//    {
//        if(!other.transform.parent.CompareTag("Player"))
//        _camDistance -= 100f;
//    }

    private void FixedUpdate()
    {
        // Clamp rotation to 90 degrees
        _currentX = Mathf.Clamp(_currentX, -15f, 75f);
        _camDistance = Mathf.Clamp(_camDistance, _minCamDistance, _maxCamDistance);
        
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
	    
		HitWall(FollowObj.transform.position, transform.position);
	    
        transform.LookAt(FollowObj.transform.position);

    }
	
	private void HitWall(Vector3 playerVector, Vector3 camVector)
	{
		// Debug.DrawLine (playerVector, camVector, Color.cyan);
		RaycastHit wallHit = new RaycastHit();
		if(Physics.Linecast(playerVector, camVector, out wallHit))
		{
			// Debug.DrawLine (wallHit.point, Vector3.left, Color.red);
			transform.position = new Vector3 (wallHit.point.x, transform.position.y, wallHit.point.z);
		}
	}
}
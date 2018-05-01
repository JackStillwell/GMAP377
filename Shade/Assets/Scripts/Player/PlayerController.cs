using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;
    [SerializeField] private float _jumpForce = 200f;
    [SerializeField] private float _groundDistance = 0.5f;

    private float _x;
    private float _y;
    private float _z;

    private Rigidbody _pcRigidbody;

    void Start()
    {
        _pcRigidbody = GetComponent<Rigidbody>();
        // Freezes X and Z rotation so the player cannot fall over and can only rotate side to side.
        _pcRigidbody.constraints = (RigidbodyConstraints) 80;
        _pcRigidbody.useGravity = true;
        _pcRigidbody.isKinematic = false;
    }

    void Update()
    {
        _y = Input.GetAxis("Mouse X");

        _z = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        _x = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _pcRigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }

    private void FixedUpdate()
    {
        Vector3 moveVector = _pcRigidbody.position + _pcRigidbody.rotation * new Vector3(_x, 0, _z);
        Quaternion moveQuaternion = Quaternion.Euler(new Vector3(0, _y, 0));
        
        _pcRigidbody.MoveRotation(transform.rotation * moveQuaternion);
        _pcRigidbody.MovePosition(moveVector);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(_pcRigidbody.transform.position, Vector3.down, out hit, _groundDistance);
    }
}
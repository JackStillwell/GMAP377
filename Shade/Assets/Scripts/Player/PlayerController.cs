﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _groundDistance = 0.5f;
    [SerializeField] private float _jumpForce = 200f;
    [SerializeField] private float _moveSpeed = 3.0f;

    private Rigidbody _pcRigidbody;
    [SerializeField] private float _rotateSensitivity = 1f;

    private float _x;
    private float _y;
    private float _z;

    private Animator animator;

    private void Start()
    {
        _pcRigidbody = GetComponent<Rigidbody>();
        // Freezes X and Z rotation so the player cannot fall over and can only rotate side to side.
        _pcRigidbody.constraints = (RigidbodyConstraints)80;
        _pcRigidbody.useGravity = true;
        _pcRigidbody.isKinematic = false;
        animator = transform.GetComponentInChildren<Animator>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        _y = Input.GetAxis("Mouse X") * _rotateSensitivity;

        _z = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        _x = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded()) _pcRigidbody.AddForce(Vector3.up * _jumpForce);
        if (_z != 0 || _x != 0)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);
        var moveVector = _pcRigidbody.position + _pcRigidbody.rotation * new Vector3(_x, 0, _z);
        var moveQuaternion = Quaternion.Euler(new Vector3(0, _y, 0));

        _pcRigidbody.MoveRotation(transform.rotation * moveQuaternion);
        _pcRigidbody.MovePosition(moveVector);
    }

    private bool IsGrounded()
    {
        RaycastHit hit;
        return Physics.Raycast(_pcRigidbody.transform.position, Vector3.down, out hit, _groundDistance);
    }

    private void OnCollisionEnter(Collision other)
    {
        _pcRigidbody.angularVelocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision other)
    {
        _pcRigidbody.angularVelocity = Vector3.zero;
    }
}
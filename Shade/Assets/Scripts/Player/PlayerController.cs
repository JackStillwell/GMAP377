using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 3.0f;

    private Rigidbody _pcRigidbody;

    void Start()
    {
        _pcRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        var y = Input.GetAxis("Mouse X");
        
        var z = Input.GetAxis("Vertical") * Time.deltaTime * _moveSpeed;
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * _moveSpeed;
        
        Vector3 moveVector = _pcRigidbody.position + new Vector3(x, 0, z);
        Quaternion moveQuaternion = Quaternion.Euler(new Vector3(0, y, 0));
        
        _pcRigidbody.MoveRotation(transform.rotation * moveQuaternion);
        _pcRigidbody.MovePosition(moveVector);
    }
}
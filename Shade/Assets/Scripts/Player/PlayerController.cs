using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3.0f;

    private Rigidbody pcRigidbody;

    void Start()
    {
        pcRigidbody = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        var y = Input.GetAxis("Mouse X");
        var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        
        transform.Rotate(0, y, 0);
        transform.Translate(x, 0, z);
    }
}

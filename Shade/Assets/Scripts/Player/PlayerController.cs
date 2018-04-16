using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 150.0f;
    [SerializeField] private float moveSpeed = 3.0f;
    
    void Update()
    {
        var y = Input.GetAxis("Horizontal") * Time.deltaTime * rotateSpeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        transform.Rotate(0, y, 0);
        transform.Translate(0, 0, -z);
    }
}

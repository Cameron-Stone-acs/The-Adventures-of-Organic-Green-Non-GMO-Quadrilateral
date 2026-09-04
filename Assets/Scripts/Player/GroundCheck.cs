using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;

    void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Enter Door") && !other.gameObject.CompareTag("Exit Door") &&
            !other.gameObject.CompareTag("Checkpoint") && !other.gameObject.CompareTag("Kill"))
        {
            isGrounded = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enter Door") && !other.gameObject.CompareTag("Exit Door") && 
            !other.gameObject.CompareTag("Checkpoint") && !other.gameObject.CompareTag("Kill"))
        {
            isGrounded = false;
        }
    }
}

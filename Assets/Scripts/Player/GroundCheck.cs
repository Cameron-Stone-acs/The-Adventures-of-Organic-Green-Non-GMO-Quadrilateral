using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool isGrounded;

    void OnTriggerEnter(Collider other)
    {
        if(!other.gameObject.CompareTag("Enter Door") && !other.gameObject.CompareTag("Exit Door"))
        {
            isGrounded = true;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Enter Door") && !other.gameObject.CompareTag("Exit Door"))
        {
            isGrounded = false;
        }
    }
}

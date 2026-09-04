using System.Collections;
using UnityEngine;

public class FlipPlatform : MonoBehaviour
{
    public float speed = 90f;
    public float waitTime = 2f;
    public float flipAngle = 180f;

    private Quaternion startRotation;
    private Quaternion flippedRotation;
    private bool flipped = false;
    private float timer = 0f;

    void Start() 
    {
        startRotation = transform.rotation;
        flippedRotation = startRotation * Quaternion.Euler(0, 0, flipAngle);
    }

    void Update() 
    {
        Quaternion target;

        if (flipped) target = startRotation;
        else target = flippedRotation;
        
        // Flip the object
        transform.rotation = Quaternion.RotateTowards(transform.rotation, target, speed * Time.deltaTime);

        // When the flip is finished, start the wait timer
        if (Quaternion.Angle(transform.rotation, target) < 0.1f)
        {
            timer += Time.deltaTime;

            if (timer >= waitTime) {
                flipped = !flipped;
                timer = 0f;
            }
        }
    }
}

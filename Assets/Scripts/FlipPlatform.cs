using System.Collections;
using UnityEngine;

public class FlipPlatform : MonoBehaviour
{
    public float flipTime = 2.5f;
    public float timeToFlip = 0.5f;
    private float time;
    private float timeFlip;
    private bool isFlipping = false;
    private int flipSide = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time = flipTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isFlipping) StartCoroutine(Flip(flipTime));
    }

    IEnumerator Flip(float duration)
    {
        isFlipping = true;
        yield return new WaitForSeconds(duration);
        
        if (flipSide == 0)
        {
            for (int i = 0; i < 180; i++)
            {
                yield return new WaitForSeconds(0.001f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 180), 1);
            }
            flipSide = 1;
        }
        else
        {
            for (int i = 0; i < 180; i++)
            {
                yield return new WaitForSeconds(0.001f);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 1);    
            }
            flipSide = 0;
        }
        isFlipping = false;
    }
}

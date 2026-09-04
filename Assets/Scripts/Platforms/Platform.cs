using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Platform : MonoBehaviour
{
    public InputActionAsset actions;
    private InputAction moveAction;
    private Rigidbody rb;

    public LayerMask playerMask;
    public LayerMask platformMask;

    int playerLayer;
    int platformLayer;

    bool isIgnoring = false;

    void Awake()
    {
        playerLayer = GetFirstLayer(playerMask);
        platformLayer = GetFirstLayer(platformMask);

        moveAction = actions.FindAction("Move");
    }

    void Update()
    {
        rb = GetComponent<Rigidbody>();

        if (moveAction.ReadValue<Vector2>().y < 0 && !isIgnoring)
        {
            StartCoroutine(TemporarilyIgnoreCollision(0.5f));
        }
        if (rb.linearVelocity.y > 0 && !isIgnoring)
        {
            StartCoroutine(TemporarilyIgnoreCollision(0.2f));
        }
    }

    IEnumerator TemporarilyIgnoreCollision(float duration)
    {
        isIgnoring = true;

        // Disable collision between Player and Platform layers
        Physics.IgnoreLayerCollision(playerLayer, platformLayer, true);

        yield return new WaitForSeconds(duration);

        // Re-enable collision
        Physics.IgnoreLayerCollision(playerLayer, platformLayer, false);

        isIgnoring = false;
    }

    // Get the first layer index from a LayerMask
    int GetFirstLayer(LayerMask mask)
    {
        int layer = mask.value;

        for (int i = 0; i < 32; i++) { if ((layer & (1 << i)) != 0) return i; }

        return 0;
    }
}

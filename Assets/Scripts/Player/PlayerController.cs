using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Input Actions and Asset (player input keys)
    public InputActionAsset actions;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction interactAction;

    // Movement variables
    public float runSpeed = 5f;
    public float JumpForce = 5f;
    public float extraJumps = 1;
    public float currentExtraJumps;
    private bool isGrounded;
    private Vector2 moveDirection;
    private Vector3 playerPosition;

    // Door variables
    public bool enterDoor = false;
    public bool exitDoor = false;

    // Other Objects and Scripts
    public GroundCheck groundCheckScript;
    private Rigidbody rb;


    void Start()
    {
        // Assigning component references
        rb = GetComponent<Rigidbody>();

        // Assigning the player actions from the main asset (player input keys)
        moveAction = actions.FindAction("Move");
        jumpAction = actions.FindAction("Jump");
        interactAction = actions.FindAction("Interact");

        // Enabling all actions (player input keys)
        moveAction.Enable();
        jumpAction.Enable();
        interactAction.Enable();
    }

    void Update()
    {
        // Assigning variables that need to be constant
        playerPosition = this.gameObject.transform.position;
        moveDirection = moveAction.ReadValue<Vector2>(); // Ge the X and Y values from the move action in the input system
        isGrounded = groundCheckScript.isGrounded;

        // Jumping
        if (jumpAction.WasPressedThisFrame() && currentExtraJumps > 0)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            currentExtraJumps--;
        }
        if (isGrounded) currentExtraJumps = extraJumps;

        // Interactions
        if (interactAction.WasPressedThisFrame())
        {
            if (enterDoor) this.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z - 20);
            if (exitDoor) this.gameObject.transform.position = new Vector3(playerPosition.x, playerPosition.y, playerPosition.z + 20);
        }
    }

    // Movement
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * runSpeed, rb.linearVelocity.y, 0f);
    }

    // Collision Enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enter Door")) enterDoor = true;
        if (other.gameObject.CompareTag("Exit Door")) exitDoor = true;
    }

    // Collision Exit
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enter Door")) enterDoor = false;
        if (other.gameObject.CompareTag("Exit Door")) exitDoor = false;
    }
}
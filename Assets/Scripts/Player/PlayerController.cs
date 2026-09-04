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
    private float currentExtraJumps;
    private bool isGrounded;
    private Vector2 moveDirection;
    private Vector3 playerPosition;

    // Door variables
    private bool enterDoor = false;
    private bool exitDoor = false;

    // Respawn Variables
    private Transform currentRespawn;
    private Transform collidedRespawn;
    private float currentRespawnNumber;
    private float collidedRespawnNumber;

    // Other Objects and Scripts
    public GroundCheck groundCheckScript;
    private Rigidbody rb;

    // Runs when script is loaded
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

    // Runs every Frame
    void Update()
    {
        // Assigning variables that need to be constant
        playerPosition = this.transform.position;
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

    // Runs every fixed frame
    void FixedUpdate()
    {
        rb.linearVelocity = new Vector3(moveDirection.x * runSpeed, rb.linearVelocity.y, 0f); // Smooth Movement
    }

    // Assigns new checkpoint 
    private void CheckPoint(Collider other)
    {
        collidedRespawn = other.gameObject.transform;
        collidedRespawnNumber = other.gameObject.GetComponent<CheckPoint>().checkpointNumber;

        if (currentRespawnNumber < collidedRespawnNumber)
        {
            currentRespawn = collidedRespawn;
            currentRespawnNumber = collidedRespawnNumber;
        }
    }

    // Collision Enter
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enter Door")) enterDoor = true;
        if (other.gameObject.CompareTag("Exit Door")) exitDoor = true;
        if (other.gameObject.CompareTag("Kill")) this.transform.position = currentRespawn.position;

        if (other.gameObject.CompareTag("Checkpoint")) CheckPoint(other);
    }

    // Collision Exit
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enter Door")) enterDoor = false;
        if (other.gameObject.CompareTag("Exit Door")) exitDoor = false;
    }
}
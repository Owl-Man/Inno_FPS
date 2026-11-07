using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class FPSInput : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference crouchAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;
    [SerializeField] private float crouchMultiplier = 0.5f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float gravity = -20f;

    [Header("Crouch")]
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchingHeight = 1f;
    [SerializeField] private float heightLerpSpeed = 12f;

    [Header("Ground Check")]
    [SerializeField] private float groundedRayLength = 0.15f;
    [SerializeField] private LayerMask groundMask = ~0;
    private bool isGrounded;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isCrouching;
    private float targetHeight;

    private void Start()
    {
        controller = GetComponent<CharacterController>();

        isCrouching = false;
        targetHeight = standingHeight;
        controller.height = standingHeight;
        controller.center = Vector3.zero;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
        jumpAction.action.Enable();
        crouchAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
        jumpAction.action.Disable();
        crouchAction.action.Disable();
    }

    private void Update()
    {
        HandleCrouch();
        HandleMovement();
        HandleJumpAndGravity();
    }

    private void HandleCrouch()
    {
        bool wantCrouch = crouchAction.action.IsPressed();

        if (wantCrouch != isCrouching)
        {
            isCrouching = wantCrouch;
            targetHeight = isCrouching ? crouchingHeight : standingHeight;
        }

        float previousHeight = controller.height;

        float newHeight = Mathf.Lerp(controller.height, targetHeight, heightLerpSpeed * Time.deltaTime);
        controller.height = newHeight;
        controller.center = Vector3.zero;   

        float heightDelta = newHeight - previousHeight;
        if (Mathf.Abs(heightDelta) > 0)
        {
            transform.position += Vector3.up * (heightDelta * 0.5f);
        }
    }

    private void HandleMovement()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        bool sprint = sprintAction.action.IsPressed() && !isCrouching;

        float speed = moveSpeed;
        if (isCrouching) speed *= crouchMultiplier;
        else if (sprint) speed *= sprintMultiplier;

        Vector3 horizontal = new Vector3(input.x, 0f, input.y);
        horizontal = Vector3.ClampMagnitude(horizontal, 1f);
        horizontal = transform.TransformDirection(horizontal);

        controller.Move(horizontal * speed * Time.deltaTime);
    }

    private void HandleJumpAndGravity()
    {
        isGrounded = controller.isGrounded || 
            Physics.Raycast(transform.position, Vector3.down, 
                    controller.height * 0.5f + groundedRayLength, 
                    groundMask);

        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f; 
        }

        if (jumpAction.action.WasPressedThisFrame() && isGrounded && !isCrouching)
        {
            velocity.y = jumpForce;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void Launch(Vector3 force)
    {
        velocity = force;
    }

    public bool IsCrouching => isCrouching;
}

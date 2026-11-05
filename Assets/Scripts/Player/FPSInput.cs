using UnityEngine;
using UnityEngine.InputSystem;
public class FPSInput : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintMultiplier = 2f;

    private CharacterController controller;
    private float gravity = -9.81f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        sprintAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        sprintAction.action.Disable();
    }

    private void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();

        bool sprint = sprintAction.action.IsPressed();
        float speed = moveSpeed * (sprint ? sprintMultiplier : 1f);

        Vector3 horizontal = new Vector3(input.x, 0f, input.y);
        horizontal = Vector3.ClampMagnitude(horizontal, 1f);
        horizontal = transform.TransformDirection(horizontal);

        Vector3 vertical = new Vector3(0f, gravity, 0f);

        controller.Move((horizontal * speed + vertical) * Time.deltaTime);
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLookX : MonoBehaviour
{
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float lookSensitivity = 0.3f;

    private void OnEnable()
    {
        lookAction.action.Enable();
    }
    private void OnDisable()
    {
        lookAction.action.Disable();
    }

    private void Update()
    {
        var input = lookAction.action.ReadValue<Vector2>();
        transform.Rotate(new Vector3(0, input.x * lookSensitivity, 0));
    }
}

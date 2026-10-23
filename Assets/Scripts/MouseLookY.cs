using UnityEngine;
using UnityEngine.InputSystem;
public class MouseLookY : MonoBehaviour
{
    [SerializeField] private InputActionReference lookAction;
    [SerializeField] private float lookSensitivity = .3f;
    [SerializeField] private float minRotation = -50f;
    [SerializeField] private float maxRotation = 50f;
    private float rotationX = 0f;

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
        Vector2 input = lookAction.action.ReadValue<Vector2>();
        rotationX += -input.y * lookSensitivity;
        rotationX = Mathf.Clamp(rotationX, minRotation, maxRotation);
        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    } 
}

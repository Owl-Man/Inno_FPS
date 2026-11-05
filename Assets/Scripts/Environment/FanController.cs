using UnityEngine;

public class FanController : MonoBehaviour
{
    public float maxSpeed = 800f;       
    public float acceleration = 300f;  

    private float currentSpeed;
    private bool isOn;

    private void Update()
    {
        float targetSpeed = isOn ? maxSpeed : 0f;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        transform.Rotate(currentSpeed * Time.deltaTime, 0f, 0);
    }

    public void Toggle()
    {
        isOn = !isOn;
    }

    public void SetOn(bool on)
    {
        isOn = on;
    }
}

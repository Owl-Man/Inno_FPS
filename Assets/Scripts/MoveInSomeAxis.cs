using UnityEngine;

public class MoveInSomeAxis : MonoBehaviour
{
    public float orbitRadius = 2f;     
    public float orbitSpeed = 1f;
    public float height = 0.5f;   
    public float heightSpeed = 2f; 

    public float spinSpeed = 90f;     
    public float axisWobble = 1f;      

    public float pulseAmount = 0.2f; 
    public float pulseSpeed = 3f;   

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        float t = Time.time;

        float x = Mathf.Cos(t * orbitSpeed) * orbitRadius;
        float z = Mathf.Sin(t * orbitSpeed) * orbitRadius;
        float y = Mathf.Sin(t * heightSpeed) * height;
        transform.position = startPos + new Vector3(x, y, z);

        Vector3 axis = new Vector3(
            Mathf.Sin(t * axisWobble),
            1f + Mathf.Cos(t * axisWobble * 0.7f) * 0.5f,
            Mathf.Sin(t * axisWobble + 1f)
        ).normalized;
        transform.Rotate(axis, spinSpeed * Time.deltaTime, Space.World);

        float scale = 1f + Mathf.Sin(t * pulseSpeed) * pulseAmount;
        transform.localScale = Vector3.one * scale;
    }
}

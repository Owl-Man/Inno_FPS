using UnityEngine;

public class Spin : MonoBehaviour
{
    public float spinSpeed = 20.0f;

    private void Update()
    {
       float verticalRot = Time.deltaTime * spinSpeed; 
       transform.Rotate(verticalRot, 0, 0);
    }
}

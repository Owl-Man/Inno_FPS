using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float launchForce = 22f;
    [SerializeField] private Vector3 launchDirection = Vector3.up;

    private void OnTriggerEnter(Collider other)
    {
        FPSInput player = other.GetComponent<FPSInput>();
        if (player == null) return;

        player.Launch(launchDirection.normalized * launchForce);
    }
}

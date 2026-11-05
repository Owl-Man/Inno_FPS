using UnityEngine;

public class DoorZone : MonoBehaviour
{
    public DoorTrigger door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && door != null)
        {
            door.SetOpen(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && door != null)
        {
            door.SetOpen(false);
        }
    }
}

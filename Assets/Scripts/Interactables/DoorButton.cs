using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public float openAngle = 90f;
    public float openSpeed = 2f;
    public float interactDistance = 3f;
    public float lookAngle = 40f;
    public KeyCode interactKey = KeyCode.E;

    private bool isOpen;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Transform player;
    private Camera playerCamera;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
            playerCamera = playerObj.GetComponentInChildren<Camera>();
        }
    }

    private void Update()
    {
        Quaternion target = isOpen ? openRotation : closedRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, target, openSpeed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > interactDistance) return;

        Vector3 dirToDoor = (transform.position - playerCamera.transform.position).normalized;
        float angle = Vector3.Angle(playerCamera.transform.forward, dirToDoor);
        if (angle > lookAngle) return;

        if (Input.GetKeyDown(interactKey))
        {
            isOpen = !isOpen;
        }
    }
}

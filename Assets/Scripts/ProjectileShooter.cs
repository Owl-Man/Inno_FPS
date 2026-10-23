using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float launchSpeed = 18f;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 position = cam.transform.position + cam.transform.forward * 0.5f;

            Quaternion rotation = cam.transform.rotation;

            GameObject projectile = Instantiate(projectilePrefab, position, rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();

            rb.useGravity = true;
            rb.linearVelocity = rotation * Vector3.forward * launchSpeed;
        }
    }
}

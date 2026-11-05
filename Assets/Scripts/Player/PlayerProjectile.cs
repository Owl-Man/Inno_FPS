using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerProjectile : MonoBehaviour
{
    public float lifetime = 5f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        ReactiveTarget target = collision.gameObject.GetComponentInParent<ReactiveTarget>();

        if (target != null)
        {
            print("Projectile hit enemy");
            target.ReactToHit();
        }

        Destroy(gameObject);
    }
}

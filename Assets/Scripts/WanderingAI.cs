using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    public float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public float turnSpeed = 120.0f;

    private bool isAlive;
    [SerializeField] GameObject fireballPrefab;
    private GameObject fireball;

    private float targetYaw;
    private bool isTurning;

    private void Start()
    {
        isAlive = true;
        targetYaw = transform.eulerAngles.y;
    }

    private void Update()
    {
        if (!isAlive) return;

        if (isTurning)
        {
            Quaternion targetRotation = Quaternion.Euler(0f, targetYaw, 0f);
            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                turnSpeed * Time.deltaTime
            );

            if (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetYaw)) < 1f)
            {
                transform.rotation = targetRotation;
                isTurning = false;
            }
        }
        else
        {
            transform.Translate(0, 0, speed * Time.deltaTime);

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                PlayerCharacter player = hit.transform.GetComponentInParent<PlayerCharacter>();

                if (player != null)
                {
                    if (fireball == null)
                    {
                        fireball = Instantiate(fireballPrefab) as GameObject;
                        fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(70f, 110f);
                    if (Random.value < 0.5f) angle = -angle;

                    targetYaw = transform.eulerAngles.y + angle;
                    isTurning = true;
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }
}

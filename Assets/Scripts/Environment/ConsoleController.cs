using UnityEngine;

public class ConsoleController : MonoBehaviour
{
    public FanController fan;
    public float interactDistance = 2.5f;
    public KeyCode interactKey = KeyCode.E;

    private Transform player;

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            player = playerObj.transform;
    }

    void Update()
    {
        if (player == null || fan == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > interactDistance) return;

        if (Input.GetKeyDown(interactKey))
        {
            fan.Toggle();
        }
    }
}

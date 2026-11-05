using System.Collections;
using UnityEngine;

public class PressObstacle : MonoBehaviour
{
    private Vector3 startPosition;
    public Vector3 targetPosition;
    public float speed = 5f;
    public float delayAtTarget = 1f;

    private void Start()
    {
        startPosition = transform.localPosition;
        
        StartCoroutine(PressRoutine());
    }

    private IEnumerator PressRoutine()
    {
        while (true)
        {
            while (Vector3.Distance(transform.localPosition, targetPosition) > 0.01f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPosition, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(delayAtTarget);

            while (Vector3.Distance(transform.localPosition, startPosition) > 0.01f)
            {
                transform.localPosition = Vector3.MoveTowards(transform.localPosition, startPosition, speed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds(delayAtTarget);
        }
    }
}

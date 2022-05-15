using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    [Range(1, 10)]
    public float smoothFactor;

    private void FixedUpdate()
    {
        follow();
    }

    void follow() {
        Vector3 targetPosition = target.transform.position + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);
        transform.position = targetPosition;
    }
}

using UnityEngine;

public class TempleRun_CameraFollow : MonoBehaviour
{
    public Transform target; // The target to follow
    public float smoothSpeed = 0.125f; // smoothness of the camera movement
    public float offset = 2f; // offset between the player and the camera

    void LateUpdate()
    {
        if (target != null)
        {
            float targetX = target.position.x + offset;
            float smoothedX = Mathf.Lerp(transform.position.x, targetX, smoothSpeed);
            Vector3 newPosition = new Vector3(smoothedX, transform.position.y, transform.position.z);

            transform.position = newPosition;
        }
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("settings")]
    public Transform target;
    public float smoothSpeed = 0.12f; 
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    private float cameraZ = -10f;
    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        desiredPosition.z = cameraZ; 

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Transform target;     // The player transform to follow
    [SerializeField] private float smoothTime = 0.25f; // Time taken for camera to reach target
    [SerializeField] private Vector3 offset = new Vector3(0f, 0f, -10f); // Default 2D camera Z-offset
    public float floorLevel = -1;
    private Vector3 currentVelocity = Vector3.zero;

    // LateUpdate runs after player movement calculations, preventing camera stuttering
    private void LateUpdate()
    {
        if (target == null) return;

        // Calculate our target coordinate position
        Vector3 targetPosition = target.position + offset;

        // Smoothly glide the camera position from its current point to the target point
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        if (transform.position.y < floorLevel) transform.position = new Vector3(transform.position.x, floorLevel, transform.position.z);
        
    }
}

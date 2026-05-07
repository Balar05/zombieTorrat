using UnityEngine;

public class IsometricCameraFollow : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(-8f, 12f, -8f);
    public float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target); 
    }
}

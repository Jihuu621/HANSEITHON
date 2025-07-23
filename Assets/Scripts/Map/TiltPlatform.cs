using UnityEngine;

public class TiltPlatform : MonoBehaviour
{
    public float maxTiltAngle = 15f; 
    public float tiltSpeed = 100f;  
    public float recoverySpeed = 2f; 

    private float currentTargetAngle = 0f;
    private bool playerOnPlatform = false;

    void Update()
    {
        float currentZ = transform.eulerAngles.z;
        if (currentZ > 180f) currentZ -= 360f;

        float newZ = Mathf.Lerp(currentZ, currentTargetAngle, Time.deltaTime * (playerOnPlatform ? tiltSpeed : recoverySpeed));
        transform.rotation = Quaternion.Euler(0f, 0f, newZ);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;

            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 center = transform.position;

            if (contactPoint.x < center.x)
                currentTargetAngle = maxTiltAngle;   
            else
                currentTargetAngle = -maxTiltAngle; 
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = true;

            Vector2 contactPoint = collision.GetContact(0).point;
            Vector2 center = transform.position;

            if (contactPoint.x < center.x)
                currentTargetAngle = maxTiltAngle;
            else
                currentTargetAngle = -maxTiltAngle;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            playerOnPlatform = false;
            currentTargetAngle = 0f;
        }
    }
}

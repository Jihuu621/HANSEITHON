using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform teleportTarget;
    public Transform telTar2;
    public Transform telTar3;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (teleportTarget != null)
            {
                transform.position = teleportTarget.position;
            }
        }
        if (collision.CompareTag("Obstacle1"))
        {
            if (teleportTarget != null)
            {
                transform.position = telTar2.position;
            }
        }
        if (collision.CompareTag("Obstacle2"))
        {
            if (teleportTarget != null)
            {
                transform.position = telTar3.position;
            }
        }
    }
}

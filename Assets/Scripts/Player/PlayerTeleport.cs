using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform teleportTarget; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            if (teleportTarget != null)
            {
                transform.position = teleportTarget.position;
            }
        }
    }
}

using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveDistance = 3f; 
    public float moveSpeed = 2f;     
    private Vector2 startPos;
    private int direction = 1;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        startPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(Vector2.right * direction * moveSpeed * Time.deltaTime);

        if (Vector2.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1;
            startPos = transform.position;

            if (spriteRenderer != null)
                spriteRenderer.flipX = direction == -1;
        }
    }
}

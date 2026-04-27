using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemyScript = other.GetComponent<EnemyAI>();

            if (enemyScript != null)
            {
                enemyScript.TakeDamage(1);
            }
        }
        else if (other.CompareTag("Player"))
        {
        }

        Destroy(gameObject);
    }
}
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
            BossController boss = other.GetComponentInParent<BossController>();
            if (boss == null) boss = other.GetComponent<BossController>();

            EnemyAI enemy = other.GetComponentInParent<EnemyAI>();
            if (enemy == null) enemy = other.GetComponent<EnemyAI>();

            if (boss != null)
                boss.TakeDamage(1);
            else if (enemy != null)
                enemy.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
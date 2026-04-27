using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("settings")]
    public float speed = 2f;
    public int damage = 1;
    public int hp = 3;
    public EnemyHPBar hpBar;
    private int maxHp;

    [Header("patrul")]
    public Transform edgeCheck;
    public LayerMask groundLayer;
    public float edgeCheckRadius = 0.3f;

    [Header("anti stump")]
    public float stuckTimeLimit = 0.3f;

    private float edgeCooldown = 0f;
    private float edgeCooldownTime = 0.5f;

    private int direction = 1;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 lastPosition;
    private float stuckTimer = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        lastPosition = transform.position;
        maxHp = hp;
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(speed * direction, rb.linearVelocity.y);

        if (edgeCooldown <= 0 && edgeCheck != null)
        {
            bool isGroundAhead = Physics2D.OverlapCircle(edgeCheck.position, edgeCheckRadius, groundLayer);
            if (!isGroundAhead)
            {
                TurnAround();
                edgeCooldown = edgeCooldownTime;
            }
        }
        else
        {
            edgeCooldown -= Time.deltaTime;
        }

        CheckIfStuck();

        if (sr != null)
            sr.flipX = (direction == -1);
    }

    void CheckIfStuck()
    {
        float distanceMoved = Vector2.Distance(transform.position, lastPosition);

        if (distanceMoved < 0.01f)
        {
            stuckTimer += Time.deltaTime;
            if (stuckTimer >= stuckTimeLimit)
            {
                TurnAround();
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
            lastPosition = transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    void TurnAround()
    {
        direction *= -1;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hpBar != null) hpBar.UpdateBar(hp, maxHp);
        if (hp <= 0)
            Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (edgeCheck != null)
        {
            Gizmos.color = edgeCooldown > 0 ? Color.yellow : Color.red;
            Gizmos.DrawWireSphere(edgeCheck.position, edgeCheckRadius);
        }
    }
}
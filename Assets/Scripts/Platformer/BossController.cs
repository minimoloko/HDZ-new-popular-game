using System.Collections;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 20;
    public EnemyHPBar hpBar;
    private int currentHealth;

    [Header("References")]
    public Transform player;
    public Transform ballSpawnPoint;
    public Transform knifeSpawnPoint;

    [Header("Jump & Ball")]
    public float jumpForce = 12f;
    public float jumpInterval = 3f;
    public float ballDelay = 1f;
    public GameObject ballPrefab;
    public float ballSpeed = 7f;

    [Header("Knives")]
    public GameObject knifePrefab;
    public float knifeSpeed = 10f;
    public float fanAngle = 70f;
    public int knifeCount = 7;
    public float fanInterval = 5f;

    [Header("Activation")]
    public float activationRadius = 6f;
    public bool isActive = false;

    private Rigidbody2D rb;
    private float jumpTimer;
    private float fanTimer;

    void Start()
    {
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        if (hpBar != null) hpBar.UpdateBar(currentHealth, maxHealth);
    }

    void Update()
    {
        if (player == null) return;

        float dist = Vector2.Distance(transform.position, player.position);
        bool shouldActivate = dist <= activationRadius;

        if (shouldActivate && !isActive)
        {
            isActive = true;
            jumpTimer = 0f;
            fanTimer = 0f;
        }
        else if (!shouldActivate && isActive && dist > activationRadius + 1f)
        {
            isActive = false;
        }

        if (!isActive) return;

        jumpTimer += Time.deltaTime;
        if (jumpTimer >= jumpInterval)
        {
            Jump();
            StartCoroutine(SpawnBallWithDelay());
            jumpTimer = 0f;
        }

        fanTimer += Time.deltaTime;
        if (fanTimer >= fanInterval)
        {
            ShootKnifeFan();
            fanTimer = 0f;
        }
    }

    IEnumerator SpawnBallWithDelay()
    {
        yield return new WaitForSeconds(ballDelay);
        SpawnRollingBall();
    }

    void Jump()
    {
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void SpawnRollingBall()
    {
        if (!ballPrefab || !ballSpawnPoint || !player) return;

        GameObject ball = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        if (ballRb != null)
        {
            float dirX = player.position.x > transform.position.x ? 1f : -1f;
            ballRb.linearVelocity = new Vector2(dirX * ballSpeed, 0);
        }
    }

    void ShootKnifeFan()
    {
        if (!knifePrefab || !knifeSpawnPoint) return;

        Vector2 dirToPlayer = (player.position - knifeSpawnPoint.position).normalized;
        float baseAngle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;

        int gapIndex = Random.Range(0, knifeCount);
        float stepAngle = fanAngle / (knifeCount - 1);
        float startAngle = baseAngle - fanAngle / 2f;

        for (int i = 0; i < knifeCount; i++)
        {
            if (i == gapIndex) continue;

            float angle = startAngle + (i * stepAngle);
            Vector2 direction = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            GameObject knife = Instantiate(knifePrefab, knifeSpawnPoint.position, Quaternion.identity);
            Rigidbody2D knifeRb = knife.GetComponent<Rigidbody2D>();
            if (knifeRb != null)
                knifeRb.linearVelocity = direction * knifeSpeed;

            float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            knife.transform.rotation = Quaternion.Euler(0, 0, rotZ);
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (hpBar != null) hpBar.UpdateBar(currentHealth, maxHealth);
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            AudioManager am = FindObjectOfType<AudioManager>();
            if (am != null)
            {
                am.StopMusic();
                Destroy(am.gameObject);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = isActive ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(transform.position, activationRadius);
    }
}
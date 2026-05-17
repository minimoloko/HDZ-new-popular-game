using UnityEngine;

public class Zombie : Enemy
{
    [Header("Траектория")]
    public TrajectoryType trajectory = TrajectoryType.Straight;
    public float amplitude = 1f;
    public float frequency = 2f;
    public float damageLineX = -4f;

    [Header("Награда")]
    public int scoreReward = 10;

    public enum TrajectoryType
    {
        Straight,
        Sine,
        ZigZag
    }

    private float startY;
    private float timer;
    private int zigZagDirection = 1;

    protected override void Start()
    {
        base.Start();
        startY = transform.position.y;
    }

    protected override void Update()
    {
        // Движение влево
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Проверка на линию урона
        if (transform.position.x < damageLineX)
        {
            GameObject mel = GameObject.FindGameObjectWithTag("Player");
            if (mel != null)
            {
                MelMovement melScript = mel.GetComponent<MelMovement>();
                if (melScript != null)
                {
                    melScript.TakeDamage(1);
                    Debug.Log($"{enemyName} пробежал черту и нанёс урон!");
                }
            }
            Destroy(gameObject);
            return;
        }

        // Траектории
        switch (trajectory)
        {
            case TrajectoryType.Sine:
                timer += Time.deltaTime * frequency;
                float offsetY = Mathf.Sin(timer) * amplitude;
                transform.position = new Vector3(transform.position.x, startY + offsetY, transform.position.z);
                break;

            case TrajectoryType.ZigZag:
                timer += Time.deltaTime * frequency * 2f;
                if (timer >= 0.5f)
                {
                    timer = 0;
                    zigZagDirection *= -1;
                }
                transform.Translate(Vector3.up * zigZagDirection * amplitude * Time.deltaTime * speed);
                break;
        }
    }

    public override void TakeDamage(int damage)
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.zombieHurt);
        health -= damage;
        Debug.Log($"{enemyName} получил урон. Осталось здоровья: {health}");

        if (health <= 0)
        {
            if (SniperGameManager.Instance != null)
                SniperGameManager.Instance.AddKill();
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    protected override void Die()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.zombieDeath);
        Debug.Log($"{enemyName} убит! +{scoreReward} очков");
        base.Die();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{enemyName} коснулся Мела!");
            Destroy(gameObject);
        }
    }
}
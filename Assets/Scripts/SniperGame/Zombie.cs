using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour
{
    [Header("Основные параметры")]
    public string zombieName = "Обычный зомби";
    public int health = 1;
    public float speed = 1.5f;

    [Header("Внешний вид")]
    public Color color = Color.green;
    public Vector3 scale = Vector3.one;

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

    private SpriteRenderer sr;
    private float startY;
    private float timer;
    private int zigZagDirection = 1;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        if (sr != null) sr.color = color;
        transform.localScale = scale;
        startY = transform.position.y;
    }

    void Update()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x < damageLineX)
        {
            GameObject mel = GameObject.FindGameObjectWithTag("Player");
            if (mel != null)
            {
                MelMovement melScript = mel.GetComponent<MelMovement>();
                if (melScript != null)
                {
                    melScript.TakeDamage(1);
                    Debug.Log($"{zombieName} пробежал черту и нанёс урон!");
                }
            }
            Destroy(gameObject);
            return;
        }

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

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{zombieName} получил урон. Осталось здоровья: {health}");

        if (health <= 0)
        {
            if (SniperGameManager.Instance != null)
                SniperGameManager.Instance.AddKill();

            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    IEnumerator FlashRed()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = original;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"{zombieName} коснулся Мела!");
            Destroy(gameObject);
        }
    }
}
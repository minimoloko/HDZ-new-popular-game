using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour
{
    [Header("Основные параметры врага")]
    public string enemyName = "Враг";
    public int health = 1;
    public float speed = 1.5f;

    [Header("Внешний вид")]
    public Color color = Color.white;
    public Vector3 scale = Vector3.one;

    protected SpriteRenderer sr;

    protected virtual void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{enemyName} получил урон. Осталось здоровья: {health}");

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{enemyName} уничтожен!");
        Destroy(gameObject);
    }

    protected IEnumerator FlashRed()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = original;
        }
    }

    public virtual void ApplySettings()
    {
        if (sr != null) sr.color = color;
        transform.localScale = scale;
    }
}
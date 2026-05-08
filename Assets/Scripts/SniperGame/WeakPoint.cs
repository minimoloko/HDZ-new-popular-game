using UnityEngine;

public class WeakPoint : MonoBehaviour
{
    private Boss boss;
    public int health = 2;  
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        boss = GetComponentInParent<Boss>();
        InvokeRepeating(nameof(Blink), 0f, 0.5f);
    }

    public void SetBoss(Boss bossReference)
    {
        boss = bossReference;
    }

    void Blink()
    {
        if (sr != null)
        {
            sr.enabled = !sr.enabled;
        }
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log($"Точка получила урон. Осталось здоровья: {health}");

        if (health <= 0)
        {
            DestroyPoint();
        }
        else
        {
            StartCoroutine(FlashWhite());
        }
    }

    System.Collections.IEnumerator FlashWhite()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            sr.color = original;
        }
    }

    void DestroyPoint()
    {
        Debug.Log("Точка уничтожена!");
        if (boss != null)
            boss.OnWeakPointDestroyed(gameObject);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TakeDamage();
        }
    }
}
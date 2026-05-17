using UnityEngine;

public class MelMovement : MonoBehaviour
{
    public float swayAmount = 0.05f;  
    public float swaySpeed = 0.0000000001f;      
    public int health = 3; 
    public HealthUI healthUI;

    private float startX;
    private SpriteRenderer sr;

    void Start()
    {
        startX = transform.position.x;
        sr = GetComponent<SpriteRenderer>();

        if (healthUI != null)
            healthUI.UpdateHealth(health);
    }

    void Update()
    {
        float offsetX = Mathf.Sin(Time.time * swaySpeed) * swayAmount;
        transform.position = new Vector3(startX + offsetX, transform.position.y, transform.position.z);
    }

    public void TakeDamage(int damage)
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.hurtSound);
        health -= damage;
        Debug.Log($"Мел получил урон! Осталось здоровья: {health}");

        if (healthUI != null)
            healthUI.UpdateHealth(health);

        if (health <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(FlashRed());
        }
    }

    void Die()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.deathSound);
        Debug.Log("Мел погиб!");

        if (SniperGameManager.Instance != null)
            SniperGameManager.Instance.GameDefeat();

        gameObject.SetActive(false);
    }

    System.Collections.IEnumerator FlashRed()
    {
        if (sr != null)
        {
            Color original = sr.color;
            sr.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            sr.color = original;
        }
    }
}
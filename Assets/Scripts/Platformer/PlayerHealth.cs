using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("health settings")]
    public int maxHealth = 3;
    public float invincibilityTime = 1.5f;
    public TextMeshProUGUI hpText;

    private int currentHealth;
    private SpriteRenderer sr;
    private bool isInvincible = false;

    void Start()
    {
        currentHealth = maxHealth;
        sr = GetComponent<SpriteRenderer>();
        hpText.text = hpText.text = $"HP: {System.Math.Max(currentHealth, 0)} / {maxHealth}";
    }
    public void TakeDamage(int damage)
    {
        if (isInvincible) return;

        currentHealth -= damage;
        Debug.Log($"Damage! HP: {currentHealth}");
        UpdateUI();

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvincibilityRoutine());
        }
    }

    void UpdateUI()
    {
        if (currentHealth <= 0)
            hpText.text = "Kotli umerli...";
        else
            hpText.text = $"HP: {System.Math.Max(currentHealth, 0)} / {maxHealth}";
    }

    void Die()
    {
        Debug.Log("death!");
        Destroy(gameObject);
    }
    // miganie i neuazvimost
    System.Collections.IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        float timer = 0;
        while (timer < invincibilityTime)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(0.1f);
            timer += 0.1f;
        }
        sr.enabled = true;
        isInvincible = false;
    }
}
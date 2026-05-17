using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public int damage = 1;
    public float lifetime = 7f;
    void Start() => Destroy(gameObject, lifetime);

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
        
        if (other.CompareTag("Player") || other.CompareTag("Ground"))
            Destroy(gameObject);
    }
}
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public int damage = 1;
    private Ammo ammo;

    void Start()
    {
        ammo = GetComponent<Ammo>();
        if (ammo == null)
        {
            ammo = FindObjectOfType<Ammo>();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (ammo != null && !ammo.TryShoot())
            {
                Debug.Log("Нет патронов или перезарядка!");
                return;
            }
            AudioManager.Instance?.PlaySFX(AudioManager.Instance.shootSound, 0.4f);
            Shoot();
        }
    }

    void Shoot()
    {
        Vector3 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Zombie"))
            {
                Zombie zombie = hit.collider.GetComponent<Zombie>();
                if (zombie != null) zombie.TakeDamage(damage);
            }
            else if (hit.collider.CompareTag("WeakPoint"))
            {
                WeakPoint wp = hit.collider.GetComponent<WeakPoint>();
                if (wp != null) wp.TakeDamage();
            }
            else if (hit.collider.CompareTag("Player"))
            {
                MelMovement mel = hit.collider.GetComponent<MelMovement>();
                if (mel != null) mel.TakeDamage(damage);
            }
        }
    }
}
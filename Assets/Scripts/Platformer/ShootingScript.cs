using UnityEngine;
using TMPro;

public class Shooting : MonoBehaviour
{
    [Header("shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [Header("ammunition")]
    public int maxAmmo = 10;
    public int currentAmmo;
    public float reloadTime = 1.5f;
    public bool isReloading = false;
    public TextMeshProUGUI ammoText;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !isReloading && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }

        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && !isReloading)
        {
            if (currentAmmo > 0)
            {
                nextFireTime = Time.time + fireRate;
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                currentAmmo--;
                UpdateAmmoUI();
            }
            else
            {
                StartCoroutine(Reload());
            }
        }
    }

    System.Collections.IEnumerator Reload()
    {
        isReloading = true;
        ammoText.text = "Reload...";
        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
    }

    void UpdateAmmoUI()
    {
        ammoText.text = $"{currentAmmo} / {maxAmmo}";
    }
}
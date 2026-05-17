using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Ammo : MonoBehaviour
{
    [Header("Параметры")]
    public int maxAmmo = 5;
    public float reloadTime = 1.2f;

    [Header("UI")]
    public TMP_Text ammoText;  

private int currentAmmo;
    private bool isReloading = false;

    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoUI();
    }

    public bool TryShoot()
    {
        if (isReloading) return false;

        if (currentAmmo > 0)
        {
            currentAmmo--;
            UpdateAmmoUI();

            if (currentAmmo == 0)
            {
                StartReload();
            }
            return true;
        }
        else
        {
            StartReload();
            return false;
        }
    }

    void StartReload()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.reloadSound);
        if (isReloading) return;
        StartCoroutine(Reload());
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Перезарядка...");

        if (ammoText != null)
            ammoText.text = "ПЕРЕЗАРЯДКА...";

        yield return new WaitForSeconds(reloadTime);

        currentAmmo = maxAmmo;
        isReloading = false;
        UpdateAmmoUI();
        Debug.Log("Перезарядка завершена!");
    }

    void UpdateAmmoUI()
    {
        if (ammoText != null)
        {
            ammoText.text = $"Патроны: {currentAmmo}/{maxAmmo}";
        }
    }
}
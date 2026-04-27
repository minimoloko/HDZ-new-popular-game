using UnityEngine;

public class EnemyHPBar : MonoBehaviour
{
    public Transform fillBar;

    private Vector3 originalFillScale;

    void Start()
    {
        if (fillBar == null) return;
        originalFillScale = fillBar.localScale;
    }

    public void UpdateBar(float currentHP, float maxHP)
    {
        if (fillBar == null) return;

        float ratio = Mathf.Clamp01(currentHP / maxHP);

        fillBar.localScale = new Vector3(originalFillScale.x * ratio, originalFillScale.y, 1);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] hearts;  
    public Color fullHeartColor = Color.red;
    public Color emptyHeartColor = Color.black;

    public void UpdateHealth(int currentHealth, int maxHealth = 3)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].color = fullHeartColor;
            }
            else
            {
                hearts[i].color = emptyHeartColor;
            }
        }
    }
}
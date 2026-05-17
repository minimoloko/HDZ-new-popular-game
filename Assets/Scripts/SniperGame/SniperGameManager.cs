using UnityEngine;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SniperGameManager : MonoBehaviour
{
    public static SniperGameManager Instance;

    [Header("—˜∏Ú˜ËÍ Û·ËÈÒÚ‚")]
    public int killCount = 0;
    public int killsToSpawnBoss = 10;

    [Header("—Ô‡‚ÌÂ Ë ·ÓÒÒ")]
    public ZombieSpawner zombieSpawner;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;
    public static System.Action OnZombieKilled;

    [Header(" ÓÌÂˆ Ë„˚")]
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;  

    private bool bossSpawned = false;
    private bool isGameEnded = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void AddKill()
    {
        killCount++;
        OnZombieKilled?.Invoke();
        Debug.Log($"”·ËÚÓ ÁÓÏ·Ë: {killCount}/{killsToSpawnBoss}");

        if (killCount >= killsToSpawnBoss && !bossSpawned)
        {
            SpawnBoss();
        }
    }

    void SpawnBoss()
    {
        bossSpawned = true;

        if (zombieSpawner != null)
            zombieSpawner.StopSpawning();

        if (bossPrefab != null && bossSpawnPoint != null)
        {
            GameObject newBoss = Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            Boss bossScript = newBoss.GetComponent<Boss>();
            if (bossScript != null && zombieSpawner != null)
            {
                bossScript.zombieSpawner = zombieSpawner;
            }
        }
    }

    public void GameVictory()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Debug.Log("œŒ¡≈ƒ¿!");
        ShowGameOverPanel("œŒ¡≈ƒ¿!");
    }

    public void GameDefeat()
    {
        if (isGameEnded) return;
        isGameEnded = true;

        Debug.Log("œŒ–¿∆≈Õ»≈!");
        ShowGameOverPanel("œŒ–¿∆≈Õ»≈!");
    }

    void ShowGameOverPanel(string message)
    {
        Time.timeScale = 0f;
        AudioManager am = FindObjectOfType<AudioManager>();
        if (am != null)
        {
            am.StopMusic();
            Destroy(am.gameObject);
        }

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);

        if (gameOverText != null)
            gameOverText.text = message;

        StartCoroutine(LoadStartLobbyAfterDelay(1));
    }

    IEnumerator LoadStartLobbyAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        SceneManager.LoadScene("MainMenu");
    }
}
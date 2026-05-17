using UnityEngine;

public class Boss : Enemy
{
    [Header("Настройки босса")]
    public GameObject weakPointPrefab;
    public Transform[] weakPointPositions;
    public int currentWave = 1;
    public int maxWaves = 2;

    [Header("Призыв зомби")]
    public ZombieSpawner zombieSpawner;
    public int zombiesToSummon = 3;

    [Header("Движение")]
    public float idleSpeed = 0f;
    public float walkSpeed = 1.5f;
    public float killLineX = -8f;

    private bool hasKilled = false;
    private int activePoints = 0;
    private bool isSummoning = false;
    private bool isDefeated = false;

    protected override void Start()
    {
        base.Start();
        speed = walkSpeed;
        SpawnWave();
    }

    protected override void Update()
    {
        if (!isSummoning && !isDefeated)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (!hasKilled && transform.position.x < killLineX)
        {
            KillPlayer();
            hasKilled = true;
        }
    }

    void SpawnWave()
    {
        Debug.Log($"Волна {currentWave} из {maxWaves}");
        activePoints = weakPointPositions.Length;

        foreach (Transform pos in weakPointPositions)
        {
            GameObject point = Instantiate(weakPointPrefab, pos.position, Quaternion.identity);
            point.transform.SetParent(pos);
            WeakPoint wp = point.GetComponent<WeakPoint>();
            if (wp != null) wp.SetBoss(this);
        }
    }

    public void OnWeakPointDestroyed(GameObject weakPoint)
    {
        activePoints--;
        Debug.Log($"Осталось точек: {activePoints}");

        if (activePoints <= 0 && !isSummoning && !isDefeated)
        {
            if (currentWave < maxWaves)
            {
                StartSummoning();
            }
            else
            {
                Defeat();
            }
        }
    }

    void StartSummoning()
    {
        isSummoning = true;
        speed = idleSpeed;
        Debug.Log($"Босс призывает {zombiesToSummon} зомби!");

        if (zombieSpawner != null)
        {
            zombieSpawner.SummonZombies(zombiesToSummon, this);
        }
    }

    public void OnSummonedZombiesDead()
    {
        isSummoning = false;
        speed = walkSpeed;
        currentWave++;
        SpawnWave();
    }

    void Defeat()
    {
        AudioManager.Instance?.PlaySFX(AudioManager.Instance.bossDeath);
        isDefeated = true;
        Debug.Log("БОСС ПОБЕЖДЁН!");

        if (SniperGameManager.Instance != null)
            SniperGameManager.Instance.GameVictory();

        Destroy(gameObject);
    }

    void KillPlayer()
    {
        Debug.Log("Босс пересёк черту! Мел погибает!");

        GameObject mel = GameObject.FindGameObjectWithTag("Player");
        if (mel != null)
        {
            MelMovement melScript = mel.GetComponent<MelMovement>();
            if (melScript != null)
            {
                melScript.TakeDamage(999);
            }
            else
            {
                Destroy(mel);
            }
        }

        Time.timeScale = 0f;
    }
}
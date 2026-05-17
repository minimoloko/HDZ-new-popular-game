using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("Спрайты зомби")]
    public Sprite normalZombieSprite;
    public Sprite tankZombieSprite;
    public Sprite fastZombieSprite;
    public GameObject zombiePrefab;
    public float spawnInterval = 1.5f;
    public float spawnX = 12f;
    public float minY = -3f;
    public float maxY = 3f;
    public int maxZombiesOnScreen = 5;

    private int currentZombies = 0;
    private int summonedZombiesRemaining = 0;
    private Boss currentBoss;

    void Start()
    {
        InvokeRepeating("SpawnZombie", 1f, spawnInterval);
    }

    void SpawnZombie()
    {
        if (currentZombies >= maxZombiesOnScreen) return;
        if (zombiePrefab == null) return;

        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
        GameObject newZombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
        SetupZombieSettings(newZombie);

        ZombieTracker tracker = newZombie.GetComponent<ZombieTracker>();
        if (tracker == null) tracker = newZombie.AddComponent<ZombieTracker>();
        tracker.spawner = this;

        currentZombies++;
    }

    public void SummonZombies(int count, Boss boss)
    {
        summonedZombiesRemaining = count;
        currentBoss = boss;

        for (int i = 0; i < count; i++)
        {
            float randomY = Random.Range(minY, maxY);
            Vector3 spawnPos = new Vector3(spawnX, randomY, 0);
            GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
            SetupZombieSettings(zombie);

            ZombieTracker tracker = zombie.GetComponent<ZombieTracker>();
            if (tracker == null) tracker = zombie.AddComponent<ZombieTracker>();
            tracker.SetSummoned(true);
            tracker.spawner = this;

            currentZombies++;
        }
    }

    private void SetupZombieSettings(GameObject zombie)
    {
        Zombie zScript = zombie.GetComponent<Zombie>();
        SpriteRenderer sr = zombie.GetComponent<SpriteRenderer>();

        if (zScript != null)
        {
            int type = Random.Range(0, 3);
            switch (type)
            {
                case 0:
                    zScript.health = 1;
                    zScript.speed = 1.5f;
                    zScript.color = Color.white; 
                    zScript.scale = Vector3.one;
                    zScript.enemyName = "Обычный зомби";
                    if (normalZombieSprite != null) sr.sprite = normalZombieSprite;
                    break;
                case 1:
                    zScript.health = 3;
                    zScript.speed = 1f;
                    zScript.color = Color.white;
                    zScript.scale = new Vector3(1.2f, 1.2f, 1f);
                    zScript.enemyName = "Танк-зомби";
                    if (tankZombieSprite != null) sr.sprite = tankZombieSprite;
                    break;
                case 2:
                    zScript.health = 1;
                    zScript.speed = 3f;
                    zScript.color = Color.white;
                    zScript.scale = new Vector3(0.8f, 0.8f, 1f);
                    zScript.enemyName = "Быстрый зомби";
                    if (fastZombieSprite != null) sr.sprite = fastZombieSprite;
                    break;
            }

            int traj = Random.Range(0, 3);
            zScript.trajectory = (Zombie.TrajectoryType)traj;

            zombie.transform.localScale = zScript.scale;
        }
    }

    public void OnZombieDied()
    {
        currentZombies--;
    }

    public void OnSummonedZombieDied()
    {
        summonedZombiesRemaining--;
        if (summonedZombiesRemaining <= 0 && currentBoss != null)
        {
            currentBoss.OnSummonedZombiesDead();
            currentBoss = null;
        }
    }

    public void StopSpawning()
    {
        CancelInvoke("SpawnZombie");
        Debug.Log("Спавн зомби остановлен");
    }
}
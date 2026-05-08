using UnityEngine;

public class ZombieTracker : MonoBehaviour
{
    public ZombieSpawner spawner;
    private bool isSummoned = false;

    public void SetSummoned(bool summoned)
    {
        isSummoned = summoned;
    }

    void OnDestroy()
    {
        if (spawner != null)
        {
            if (isSummoned)
                spawner.OnSummonedZombieDied();
            else
                spawner.OnZombieDied();
        }
    }
}
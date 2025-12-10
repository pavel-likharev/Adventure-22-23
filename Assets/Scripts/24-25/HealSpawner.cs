using UnityEngine;

public class HealSpawner : Spawner
{
    private int _healValue;

    public HealSpawner(
        GameObject spawned, 
        Transform spawnPoint, 
        Timer timer, 
        float spawnTime, 
        MonoBehaviour coroutineRunner, 
        int healValue) : 
        base(spawned, spawnPoint, timer, spawnTime, coroutineRunner)
    {
        _healValue = healValue;
    }

    protected override void Spawn()
    {
        base.Spawn();

        GetLastSpawned().GetComponent<Healer>().Inizialize(_healValue);

        Debug.Log("heal value adding");
    }
}

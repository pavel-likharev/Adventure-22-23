using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner
{
    private GameObject _spawnedObject;
    private Timer _timer;
    private float _spawnTime;
    private Transform _spawnPoint;

    private MonoBehaviour _coroutineRunner;
    private Coroutine _spawnProcess;
    private List<GameObject> _spawnedList;

    public Spawner(GameObject spawned, Transform spawnPoint, Timer timer, float spawnTime, MonoBehaviour coroutineRunner)
    {
        _spawnedObject = spawned;
        _timer = timer;
        _coroutineRunner = coroutineRunner;
        _spawnTime = spawnTime;
        _spawnPoint = spawnPoint;

        _spawnedList = new List<GameObject>();
    }

    public bool IsEnable;

    public bool InProcess() => _timer.InProcess(out float elapsedTime);

    public GameObject GetLastSpawned() => _spawnedList[_spawnedList.Count - 1];


    protected virtual void StartSpawnProcess()
    {
        _spawnProcess = _coroutineRunner.StartCoroutine(SpawnProcess());
    }

    private IEnumerator SpawnProcess()
    {
        while (IsEnable)
        {
            _timer.StartProcess(_spawnTime);

            while (_timer.InProcess(out float elapsedTime))
            {
                yield return null;
            }
            
            Spawn();
        }
    }

    protected virtual void Spawn()
    {
        Debug.Log("spawn");

        Vector3 safeSpawnPoint = GetSafeRandomPoint(_spawnPoint);
        GameObject newSpawned = Object.Instantiate(_spawnedObject, safeSpawnPoint, Quaternion.identity);
        _spawnedList.Add(newSpawned);
    }

    private Vector3 GetSafeRandomPoint(Transform spawnPoint, float radius = 2, int maxAttempts = 10)
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector3 randomOnSphere = Random.onUnitSphere;
            randomOnSphere.y = 0.5f;
            Vector3 point = spawnPoint.position + randomOnSphere * radius;

            // Проверяем, нет ли препятствий
            if (IsPointValid(point))
            {
                point.y = spawnPoint.position.y;
                Debug.Log("spawn point find");
                return point;
            }
        }

        // Если не нашли подходящую точку, возвращаем дефолтную
        return spawnPoint.position + spawnPoint.forward * radius;
    }

    private bool IsPointValid(Vector3 point)
    {
        Collider[] colliders = Physics.OverlapSphere(point, 0.5f);

        return colliders.Length == 0; // Точка свободна
    }

    public void SwitchWorkStatus()
    {
        if (IsEnable)
            Disable();
        else
            Enable();
    }

    public void Enable()
    {
        IsEnable = true;
        StartSpawnProcess();
        Debug.Log("enable");
    }

    public void Disable()
    {
        IsEnable = false;
        _coroutineRunner.StopCoroutine(_spawnProcess);
        _spawnProcess = null;
        Debug.Log("disable");
    }
}

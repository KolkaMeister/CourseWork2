using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Monster[] _monsters;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private int _instaSpawnCount;
    [SerializeField] private int _longTimeSpawnCount;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private float _intervalRandomization;

    public event Action SpawnEnded;

    public int TotalMons=>_instaSpawnCount+_longTimeSpawnCount;
    public bool IsSpawnEnded;

    void Start()
    {
        Work();
    }
    private void Work()
    {
        InstSpawn();
        StartCoroutine(IntervalSpawn());
    }

    private void InstSpawn()
    {
        for (int i = 0; i < _instaSpawnCount; i++)
            RandomMobSet();
    }

    private IEnumerator IntervalSpawn()
    {
        for (int i=0;i< _longTimeSpawnCount; i++ ) 
        { 
        yield return new WaitForSeconds(_spawnInterval);
        RandomMobSet();
        }
        SpawnEnded?.Invoke();
        IsSpawnEnded=true;

    }
    private void RandomMobSet()
    {
        var mon = _monsters[UnityEngine.Random.Range(0,_monsters.Length)];
        var point = _spawnPoints[UnityEngine.Random.Range(0,_spawnPoints.Length)];
        SpawnMob(mon,point);
    }

    private void SpawnMob(Monster mon, Transform point)
    {
        Instantiate(mon, point.transform.position, Quaternion.identity);
    }
}

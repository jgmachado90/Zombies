﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    public List<Transform> spawners;
    public GameObject enemyPrefab;
    public List<Enemy> enemies;
    public int currentWaveLevel;
    public float timeBetweenSpawns;
    bool levelStart;


    private void Awake()
    {
        instance = this;
        timeBetweenSpawns = 3f;
    }

    private void Start()
    {
        currentWaveLevel = 1;
        UIManager.instance.ShowWaveCounter(currentWaveLevel);
    }

    private void Update()
    {
        if (!levelStart)
        {
            StartCoroutine(LevelStartCoroutine());
        }
    }

    public void FindSpawns(GameObject gO)
    {
        Spawner[] sP = gO.GetComponentsInChildren<Spawner>();
        foreach(Spawner s in sP)
        {
            spawners.Add(s.transform);
        }
    }

    public void NextWave()
    {
        currentWaveLevel++;
        StopAllCoroutines();
        levelStart = false;
        UIManager.instance.ShowWaveCounter(currentWaveLevel);
    }

    public void RemoveEnemy(Enemy e)
    {
        enemies.Remove(e);
    }

    public IEnumerator LevelStartCoroutine()
    {

        levelStart = true;

        int numberOfZombiesInTheWave = currentWaveLevel * 2;
        int numberOfZombiesSpawned = 0;
        yield return new WaitForSeconds(5f);

        while (true)
        {
            if (numberOfZombiesInTheWave > numberOfZombiesSpawned)
            {
                GameObject newEnemy = Instantiate(enemyPrefab);
                newEnemy.transform.position = spawners[UnityEngine.Random.Range(0,spawners.Count)].transform.position;

                enemies.Add(newEnemy.GetComponent<Enemy>());
                numberOfZombiesSpawned++;
                yield return new WaitForSeconds(timeBetweenSpawns/currentWaveLevel);
            }

            if (enemies.Count == 0)
            {
                NextWave();
            }

            yield return null;
        }

    }

}

using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public bool alive;
    public EnemyInfo enemyInfo;
    public Slider lifeBar;

    public int health;
    public int currentHealth;

    private void Start()
    {
        alive = true;
        health = enemyInfo.health + (WaveManager.instance.currentWaveLevel * 30);
        currentHealth = health;
        //GetComponent<AIDestinationSetter>().target = PlayerInfo.instance.transform;

    }

    private void FixedUpdate()
    {
        if(currentHealth < 0)
        {
            Death();
        }
    }

    internal void GiveDamage(int damage)
    {
        PointsManager.instance.AddPoints(10);
        currentHealth -= damage;
        lifeBar.value = (float)currentHealth / health;

    }
  
    private void Death()
    {
      
        PointsManager.instance.AddPoints(100);
        WaveManager.instance.RemoveEnemy(this);
        Destroy(transform.gameObject);
    }
}

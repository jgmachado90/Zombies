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
    

    public int currentHealth;

    private void Start()
    {
        alive = true;
        currentHealth = enemyInfo.health;
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
        lifeBar.value = (float)currentHealth / enemyInfo.health;

    }
  
    private void Death()
    {
        Debug.Log("enemy died" + gameObject);
        PointsManager.instance.AddPoints(100);
        WaveManager.instance.RemoveEnemy(this);
        Destroy(transform.gameObject);
    }
}

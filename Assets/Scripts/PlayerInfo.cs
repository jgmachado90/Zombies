using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInfo : MonoBehaviour
{
    public static PlayerInfo instance;

    public bool alive;
    public bool revive;
    public Slider lifeBar;

    public float timeToRebuildBarricade;
    public int health;
    public int currentHealth;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = health;
        revive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
            if (revive)
            {
                currentHealth = health;
                revive = false;
                return;
            }
            Death();
        }
    }

    public void GiveDamage(int damage)
    {
        currentHealth -= damage;
        lifeBar.value = (float)currentHealth / health;

    }

    private void Death()
    {
        Destroy(transform.gameObject);
        SceneManager.LoadScene(0);
    }
    

}

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
    public Slider lifeBar;

    public int health;
    public int currentHealth;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        currentHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth < 0)
        {
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public bool alive;
    public int levelOfBarricade;
    public int currentLevelOfBarricade;

    public List<GameObject> barricadeObject;


    private void Start()
    {
        currentLevelOfBarricade = levelOfBarricade;
        alive = true;
    }

    public void DestroyBarricade()
    {
        foreach(GameObject gO in barricadeObject)
        {
            if (gO.activeSelf)
            {
                gO.SetActive(false);
                if (LastBarricadeCheck())
                    alive = false;
                return;
            }
        }
    }

    private bool LastBarricadeCheck()
    {
        bool alive = false;
        foreach (GameObject gO in barricadeObject)
        {
            if (gO.activeSelf)
            {
                alive = true;
            }
        }
        if (!alive)
            return true;
            
        return false;
    }

    public void RebuildBarricade()
    {
        foreach(GameObject gO in barricadeObject)
        {
            if (!gO.activeSelf)
            {
                gO.SetActive(true);
                alive = true;
                return;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      
        if (collision.tag == "Player")
        {

            StopAllCoroutines();
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            StartCoroutine(RebuildBarricadeCoroutine());
        }
    }

    


    IEnumerator RebuildBarricadeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            RebuildBarricade();
        }
        
    }


}

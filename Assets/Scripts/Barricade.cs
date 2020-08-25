using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barricade : MonoBehaviour
{
    public bool alive;
    public int levelOfBarricade;
    public int currentLevelOfBarricade;
    public int pointsOnRebuild;
  

    public List<BarricadeObject> barricadeObject;





    private void Start()
    {
        currentLevelOfBarricade = levelOfBarricade;
        alive = true;
    }

    public void DestroyBarricade()
    {
        foreach(BarricadeObject bO in barricadeObject)
        {
            if (bO.active)
            {
                bO.DisableActiveBarricade();
                if (LastBarricadeCheck())
                    alive = false;
                return;
            }
        }
    }

    private bool LastBarricadeCheck()
    {
        bool alive = false;
        foreach (BarricadeObject bO in barricadeObject)
        {
            if (bO.active)
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
        foreach (BarricadeObject bO in barricadeObject)
        {
            if (!bO.active)
            {
                bO.EnableActiveBarricade();
                PointsManager.instance.AddPoints(50);
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
            yield return new WaitForSeconds(PlayerInfo.instance.timeToRebuildBarricade);
            RebuildBarricade();
        }
        
    }


}

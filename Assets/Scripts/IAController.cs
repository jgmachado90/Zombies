using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAController : MonoBehaviour
{
    public Transform currentTarget;

    private void Start()
    {
        currentTarget = SeekBarricade();
        SetTarget();
    }

    public void SetTarget()
    {
        GetComponent<AIDestinationSetter>().target = currentTarget;
    }


    public Transform SeekBarricade()
    {
        Barricade[] barricade = FindObjectsOfType<Barricade>();

        float distance = 99999;
        Barricade nearestBarricade = null;

        foreach (Barricade b in barricade)
        {
            float distanceFromBarricade = Vector2.Distance(b.transform.position, transform.position);

            if (distanceFromBarricade < distance)
            {
                distance = distanceFromBarricade;
                nearestBarricade = b;
            }
        }

        return nearestBarricade.transform;
    }

    public void SeekPlayer()
    {
        currentTarget = PlayerInfo.instance.transform;
        SetTarget();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "barricade")
        {
            StartCoroutine(DestroyBarricadeCoroutine(collision.GetComponent<Barricade>()));
        }
    }

 
    IEnumerator DestroyBarricadeCoroutine(Barricade b)
    {
        while (b.alive)
        {
            yield return new WaitForSeconds(3f);
            b.DestroyBarricade();
        }
        SeekPlayer();
    }


}

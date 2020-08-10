using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponInfo weaponInfo;
    public Transform weapon;
    public Transform shootPosition;
    public PlayerMovement playerMovement;
    public bool shooting;
    public float shootRotationSpeed;


    public Enemy enemyTarget;
    private void Start()
    {
        shooting = false;
        StartShootCoroutine();
    }

    private void StartShootCoroutine()
    {
        StartCoroutine(ShootCoroutine());
    }



 
    private void FixedUpdate()
    {
       
        if (enemyTarget && !playerMovement.moving)
        { 
            Vector3 vectorToTarget = enemyTarget.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * shootRotationSpeed);

            
            if(Vector3.Angle(transform.right, vectorToTarget) <= 0.2f)
            {
                shooting = true;
            }
            else{
                shooting = false;
            }
        }


    }

    public IEnumerator ShootCoroutine()
    {
        while (true) {
            if (!playerMovement.moving)
            {
                enemyTarget = SeekEnemy();
                if (enemyTarget && shooting)
                {
                    GameObject bullet = Instantiate(weaponInfo.defaultBullet);
                    bullet.transform.position = shootPosition.position;
                    StartCoroutine(BulletTravelCoroutine(enemyTarget, bullet));
                    yield return new WaitForSeconds(weaponInfo.shootSpeed);
                }
                else
                {
                    yield return null;
                }
            }
            else
            {
                yield return null;
            }
        }
       
    }

    public Enemy SeekEnemy()
    {
        Enemy[] enemy = FindObjectsOfType<Enemy>();

        float distance = 99999;
        Enemy nearestEnemy = null;

        foreach(Enemy e in enemy)
        {
            float distanceFromEnemy = Vector2.Distance(e.transform.position, transform.position);
           
            if(distanceFromEnemy < distance)
            {
                distance = distanceFromEnemy;
                nearestEnemy = e;
            }
        }

        return nearestEnemy;
    }



    public IEnumerator BulletTravelCoroutine(Enemy enemy, GameObject bullet)
    {
        Vector3 startMarker = shootPosition.position;
        Vector3 endMarker = enemy.transform.position;

        float startTime = Time.time;
        float speed = 50f;

        float journeyLength = Vector3.Distance(startMarker, endMarker); ;
     
        while (true)
        {
            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / journeyLength;
            bullet.transform.position = Vector3.Lerp(startMarker, endMarker, fractionOfJourney);

            if (bullet.transform.position == endMarker)
            {
                Destroy(bullet);
                yield break;
            }

            yield return null;
        }

    }




}

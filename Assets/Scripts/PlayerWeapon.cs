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
    public bool turning;
    public float shootRotationSpeed;


    public Enemy enemyTarget;
    private void Start()
    {
        turning = false;
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
            Vector3 vectorToTarget = enemyTarget.transform.position - shootPosition.position;
            Debug.DrawRay(transform.position, vectorToTarget);
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * shootRotationSpeed);


            if (Vector3.Angle(transform.right, vectorToTarget) <= 0.01f)
            {
                turning = false;
            }
            else{
         
                turning = true;
            }

          

        }


    }

    public IEnumerator ShootCoroutine()
    {
        while (true) {
            if (!playerMovement.moving)
            {
                enemyTarget = SeekEnemy();
                if (enemyTarget && !turning)
                {
                    GameObject bullet = Instantiate(weaponInfo.defaultBullet);
                    bullet.GetComponent<Bullet>().whoShootsMe = weaponInfo;
                    bullet.transform.position = shootPosition.position;
                    Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                    bulletRb.AddForce(shootPosition.right * weaponInfo.shootParticleSpeed, ForceMode2D.Impulse);
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





}

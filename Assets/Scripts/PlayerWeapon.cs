using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public WeaponInfo weaponInfo;
    public GameObject currentWeapon;
    public Transform weapon;
    public Transform shootPosition;
    public PlayerMovement playerMovement;
    public bool turning;
    public float shootRotationSpeed;
    public LayerMask shootMask;

    public Enemy enemyTarget;
    private void Start()
    {
        turning = false;
        OnChangeWeapon(weaponInfo);
        StartShootCoroutine();

    }

    public void OnChangeWeapon(WeaponInfo newWeapon)
    {
        Destroy(currentWeapon);
        weaponInfo = newWeapon;
        currentWeapon = Instantiate(weaponInfo.weaponPrefab);
        
        currentWeapon.transform.SetParent(transform);
        currentWeapon.transform.position = Vector3.zero;
        currentWeapon.transform.position = new Vector3(transform.position.x , transform.position.y, transform.position.z);
        currentWeapon.transform.rotation = transform.rotation;
        shootPosition = currentWeapon.GetComponentInChildren<Transform>();

    }

    private void StartShootCoroutine()
    {
        StartCoroutine(ShootCoroutine());
    }



 
    private void FixedUpdate()
    {
       
        if (enemyTarget && !playerMovement.moving && CanShoot())
        {
            Vector3 vectorToTarget = enemyTarget.transform.position - shootPosition.position;
            
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
                if (enemyTarget && !turning && CanShoot())
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

    private bool CanShoot()
    {
   
        Vector3 vectorToTarget = enemyTarget.transform.position - shootPosition.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vectorToTarget, Mathf.Infinity, shootMask) ;
        Debug.DrawRay(shootPosition.position, vectorToTarget);
       

        if (hit.collider.tag == "enemy")
        {
            return true;
        }
        return false;
        
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

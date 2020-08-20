using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Enemy enemy;


    void Start()
    {
        StartCoroutine(EnemyAttackCoroutine());

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator EnemyAttackCoroutine() {

        while (true)
        {
            
            if (Vector3.Distance(PlayerInfo.instance.transform.position, transform.position) < 2f)
            {
                PlayerInfo.instance.GiveDamage(enemy.enemyInfo.damage);
                yield return new WaitForSeconds(enemy.enemyInfo.attackSpeed);
            }

            yield return null;
        }
    
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}

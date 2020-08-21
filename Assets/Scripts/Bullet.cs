using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public WeaponInfo whoShootsMe;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("collision tag" + collision.gameObject.tag);

        if (collision.gameObject.GetComponent<Enemy>())
        {
            // Debug.Log("colidiu com enemy");
            collision.gameObject.GetComponent<Enemy>().GiveDamage(whoShootsMe.damage);
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "Environment")
        {
            //Debug.Log("colidiu com wall");
            Destroy(this.gameObject);
        }

    }
  

    

}

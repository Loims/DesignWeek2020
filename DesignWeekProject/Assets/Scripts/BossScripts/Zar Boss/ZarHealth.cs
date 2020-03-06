using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZarHealth : MonoBehaviour
{
    private ZarStates statesParent;

    public float health;

    private void OnEnable()
    {
        statesParent = GetComponent<ZarStates>();

        health = 100f;
    }

    void DamageMe(float damage)
    {
        if (!statesParent.invuln)
        {
            health -= damage;
            statesParent.StartFlash();

            if (health <= 0f)
            {
                statesParent.ChangeState(ZarStates.ZStates.DIE);
                StartCoroutine(DeathAnimation(0.1f));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.layer == 9)
        {
            if (collisionObj.tag == "Red")
            {
                if (statesParent.color == Projectile.Color.RED)
                {
                    DamageMe(1f);
                }
            }
            if (collisionObj.tag == "Blue")
            {
                if (statesParent.color == Projectile.Color.BLUE)
                {
                    DamageMe(1f);
                }
            }
            Destroy(collisionObj);
        }
    }

    private IEnumerator DeathAnimation(float waitTime)
    {
        //Start animation here
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}

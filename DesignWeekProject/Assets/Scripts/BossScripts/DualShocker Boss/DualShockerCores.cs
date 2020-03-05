using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShockerCores : MonoBehaviour
{
    private ObjectPooler pooler;

    private GameObject projectilePrefab;

    private DualShockerStates parentStates;

    [SerializeField] private Projectile.Color color;

    [SerializeField] private float coreHealth;


    private void OnEnable()
    {
        coreHealth = 20f;
        parentStates = transform.parent.parent.GetComponent<DualShockerStates>();
        projectilePrefab = Resources.Load<GameObject>("BasicProjectile");

        if (gameObject.tag == "Red")
        {
            color = Projectile.Color.RED;
        }
        else if (gameObject.tag == "Blue")
        {
            color = Projectile.Color.BLUE;
        }
        else if(gameObject.tag == "Purple")
        {
            color = Projectile.Color.PURPLE;
        }
    }

    private void Start()
    {
        pooler = ObjectPooler.instance;
    }

    public void DamageMe(float damage)
    {
        parentStates.Retaliate(color);

        coreHealth -= damage;
        if (coreHealth <= 0f)
        {
            parentStates.RemoveFromCores(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionObj = collision.gameObject;
        if (collisionObj.layer == 9)
        {
            if (collisionObj.tag == "Red")
            {
                if (color == Projectile.Color.RED || color == Projectile.Color.PURPLE)
                {
                    DamageMe(2f);
                    //Flash white with coroutine
                    Destroy(collisionObj);
                }
            }
            if (collisionObj.tag == "Blue")
            {
                if (color == Projectile.Color.BLUE || color == Projectile.Color.PURPLE)
                {
                    DamageMe(2f);
                    //Flash white with coroutine
                    Destroy(collisionObj);
                }
            }
        }
    }
}

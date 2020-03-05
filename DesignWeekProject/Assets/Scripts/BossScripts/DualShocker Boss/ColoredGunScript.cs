using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredGunScript : MonoBehaviour
{
    private ObjectPooler pooler;

    private GameObject projectilePrefab;

    private DualShockerStates parentStates;

    [SerializeField] private Projectile.Color color;

    private float shootdelay;
    private float gunHealth;

    public LayerMask layer;

    private void OnEnable()
    {
        layer = LayerMask.NameToLayer("Projectile");
        parentStates = transform.parent.parent.GetComponent<DualShockerStates>();
        projectilePrefab = Resources.Load<GameObject>("BasicProjectile");

        if(gameObject.tag == "Red")
        {
            color = Projectile.Color.RED;
        }
        if(gameObject.tag == "Blue")
        {
            color = Projectile.Color.BLUE;
        }

        gunHealth = 20f;
    }

    private void Start()
    {
        pooler = ObjectPooler.instance;
    }

    public void ShootProjectile()
    {
        GameObject projectile = pooler.NewObject(projectilePrefab, transform.position, Quaternion.identity);
        Projectile comp = projectile.GetComponent<Projectile>();

        comp.InitializeBulletVelocity(Vector3.up * -4f);
        comp.AssignColor(color);
        comp.AssignSize(6f);
    }

    public void DamageMe(float damage)
    {
        gunHealth -= damage;
        if(gunHealth <=0f)
        {
            parentStates.RemoveFromColoredGuns(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.LogWarning("CollisionEnter");
        GameObject collisionObj = collision.gameObject;
        if(collisionObj.layer == 9)
        {
            Debug.LogWarning("THIS FUCKING WORKS");
            if (collisionObj.tag == "Red")
            {
                if(color == Projectile.Color.RED)
                {
                    DamageMe(2f);
                    //Flash white with coroutine
                    Destroy(collisionObj);
                }
            }
            if (collisionObj.tag == "Blue")
            {
                if (color == Projectile.Color.BLUE)
                {
                    DamageMe(2f);
                    //Flash white with coroutine
                    Destroy(collisionObj);
                }
            }
        }
    }
}

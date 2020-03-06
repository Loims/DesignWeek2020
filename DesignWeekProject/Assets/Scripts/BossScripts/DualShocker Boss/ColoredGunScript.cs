using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredGunScript : MonoBehaviour
{
    private ObjectPooler pooler;

    private GameObject projectilePrefab;
    private SpriteRenderer spriteRenderer;

    private DualShockerStates parentStates;

    private Sprite redGun;
    private Sprite blueGun;
    private Sprite redFlash;
    private Sprite blueFlash;

    [SerializeField] private Projectile.Color color;

    private float shootdelay;
    private float gunHealth;

    public LayerMask layer;

    private void OnEnable()
    {
        redGun = Resources.Load<Sprite>("boss_gun_red");
        blueGun = Resources.Load<Sprite>("boss_gun_blue");
        redFlash = Resources.Load<Sprite>("boss_gun_red_WHITE");
        blueFlash = Resources.Load<Sprite>("boss_gun_blue_WHITE");

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

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
        comp.AssignSprite("Orb");
        comp.AssignSize(1f);
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

    private IEnumerator FlashRED(float waitTime)
    {
        spriteRenderer.sprite = redFlash;
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.sprite = redGun;
        yield break;
    }

    private IEnumerator FlashBLUE(float waitTime)
    {
        spriteRenderer.sprite = blueFlash;
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.sprite = blueGun;
        yield break;
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
                    DamageMe(1f);
                    StartCoroutine(FlashRED(0.1f));
                    Destroy(collisionObj);
                }
            }
            if (collisionObj.tag == "Blue")
            {
                if (color == Projectile.Color.BLUE)
                {
                    DamageMe(1f);
                    StartCoroutine(FlashBLUE(0.1f));
                    Destroy(collisionObj);
                }
            }
        }
    }
}

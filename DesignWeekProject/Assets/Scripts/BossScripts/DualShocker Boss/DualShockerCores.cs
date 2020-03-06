using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShockerCores : MonoBehaviour
{
    private ObjectPooler pooler;

    private GameObject projectilePrefab;

    private DualShockerStates parentStates;
    [SerializeField] private DualShockerHealth parentHealth;

    private SpriteRenderer spriteRenderer;

    [SerializeField] private Sprite redCore;
    [SerializeField] private Sprite blueCore;
    [SerializeField] private Sprite purpleCore;
    [SerializeField] private Sprite redFlash;
    [SerializeField] private Sprite blueFlash;
    [SerializeField] private Sprite purpleFlash;

    [SerializeField] private Projectile.Color color;

    [SerializeField] private float coreHealth;


    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        redCore = Resources.Load<Sprite>("boss_red_core");
        blueCore = Resources.Load<Sprite>("boss_blue_core");
        purpleCore = Resources.Load<Sprite>("boss_purple_core");
        redFlash = Resources.Load<Sprite>("boss_red_core_WHITE");
        blueFlash = Resources.Load<Sprite>("boss_blue_core_WHITE");
        purpleFlash = Resources.Load<Sprite>("boss_purple_core_WHITE");

        parentStates = transform.parent.parent.GetComponent<DualShockerStates>();
        parentHealth = transform.parent.parent.GetComponent<DualShockerHealth>();
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

        if (color != Projectile.Color.PURPLE)
        {
            coreHealth = 20f;
        }
        else
        {
            coreHealth = 50f;
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
        DecideFlash();
        if(color == Projectile.Color.PURPLE)
        {
            parentHealth.TakeDamage(damage);
            //parentStates.StartCoroutine(StartFlash(0.1f));
        }
        if (coreHealth <= 0f)
        {
            parentStates.RemoveFromCores(this.gameObject);
            Destroy(this.gameObject);
        }
    }

    private void DecideFlash()
    {
        if(color == Projectile.Color.RED)
        {
            StartCoroutine(FlashRED(0.1f));
        }
        else if(color == Projectile.Color.BLUE)
        {
            StartCoroutine(FlashBLUE(0.1f));
        }
        else if(color == Projectile.Color.PURPLE)
        {
            StartCoroutine(FlashPURPLE(0.1f));
        }
    }

    private IEnumerator FlashRED(float waitTime)
    {
        spriteRenderer.sprite = redFlash;
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.sprite = redCore;
        yield break;
    }

    private IEnumerator FlashBLUE(float waitTime)
    {
        spriteRenderer.sprite = blueFlash;
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.sprite = blueCore;
        yield break;
    }

    private IEnumerator FlashPURPLE(float waitTime)
    {
        spriteRenderer.sprite = purpleFlash;
        yield return new WaitForSeconds(waitTime);
        spriteRenderer.sprite = purpleCore;
        yield break;
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
                    DamageMe(1f);
                    Destroy(collisionObj);
                }
            }
            if (collisionObj.tag == "Blue")
            {
                if (color == Projectile.Color.BLUE || color == Projectile.Color.PURPLE)
                {
                    DamageMe(1f);
                    Destroy(collisionObj);
                }
            }
        }
    }
}

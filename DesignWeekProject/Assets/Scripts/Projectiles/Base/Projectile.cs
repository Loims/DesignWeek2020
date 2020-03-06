using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum Color
    {
        NULL,
        RED,
        BLUE,
        PURPLE
    }

    #region Variables
    [Header("Sprites")]
    [SerializeField] protected Sprite redShotStar;
    [SerializeField] protected Sprite blueShotStar;
    [SerializeField] protected Sprite redShotOrb;
    [SerializeField] protected Sprite blueShotOrb;
    [SerializeField] protected Sprite purpleShotOrb;
    
    [Space]
    [Header("Object Pooler")]
    [SerializeField] protected ObjectPooler pooler;

    [Space]
    [Header("Object Information")]

    [SerializeField] protected Rigidbody2D rb2d;
    [SerializeField] protected float lifetime = 5f;
    [SerializeField] protected Color projectileColor;
    #endregion

    private void Awake()
    {
        pooler = ObjectPooler.instance;
        rb2d = GetComponent<Rigidbody2D>();

        redShotStar = Resources.Load<Sprite>("shot_redStar");
        blueShotStar = Resources.Load<Sprite>("shot_blueStar");
        redShotOrb = Resources.Load<Sprite>("shot_redSphere");
        blueShotOrb = Resources.Load<Sprite>("shot_blueSphere");
        purpleShotOrb = Resources.Load<Sprite>("shot_purpleSphere");
    }

    protected virtual void OnEnable()
    {
        transform.localScale = new Vector3(0.185009f, 0.185009f, 0.185009f);
        StartCoroutine(BulletLifeTime(lifetime));
        if(projectileColor == Color.NULL)
        {
            Color newColor = (Color)Random.Range(1, 3);
            Debug.Log(newColor);
            AssignColor(newColor);
        }
    }

    protected virtual void OnDisable()
    {
        projectileColor = Color.NULL;
        StopAllCoroutines();
    }

    /*<summary>
     * Method to give bullet velocity. Should be called
     * when bullet is instantiated anfd by the object instantiating it.
     * If no velocity is provided, bullet will have velocity of 0.
     * Useful if bullet has different functionality.
     * </summary>
     */
    public virtual void InitializeBulletVelocity(Vector3 bulletVelocity)
    {
        rb2d.velocity = bulletVelocity;
    }

    /*<summary>
     * Method used to assign color. Method should be called
     * when bullet is instantiated and by the object instantiating 
     * it. If no color is assigned by the parent object, the projectile
     * will be randomly assigned a color by OnEnable
     * </summary>
     */
    public void AssignColor(Color color)
    {
        projectileColor = color;

    }

    public void AssignSprite(string sprite)
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();

        if (sprite == "Star")
        {
            if (projectileColor == Color.RED)
            {
                renderer.sprite = redShotStar;
            }
            else if(projectileColor == Color.BLUE)
            {
                renderer.sprite = blueShotStar;
            }
        }
        else if (sprite == "Orb")
        {
            if (projectileColor == Color.RED)
            {
                renderer.sprite = redShotOrb;
            }
            else if(projectileColor == Color.BLUE)
            {
                renderer.sprite = blueShotOrb;
            }
            else if(projectileColor == Color.PURPLE)
            {
                renderer.sprite = purpleShotOrb;
            }
        }
    }

    public void AssignSize(float scaleMultiplier)
    {
        transform.localScale *= scaleMultiplier;
    }

    /*<summary>
     * Lifetime coroutine. When the timer reaches 0,
     * the projectile is returned to the object pool.
     * To change lifetime, edit the lifetime variable
     * </summary>
     */
    public IEnumerator BulletLifeTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        pooler.ReturnToPool(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            pooler.ReturnToPool(this.gameObject);
        }
    }
}

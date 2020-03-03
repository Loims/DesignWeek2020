using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    #region Variables
    [Header("Object Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Space]
    [Header("Object Information")]

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float lifetime = 5f;
    #endregion

    private void Awake()
    {
        pooler = ObjectPooler.instance;
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTime(lifetime));
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public virtual void InitializeBullet(Vector3 bulletVelocity)
    {
        rb2d.velocity = bulletVelocity;
    }

    public virtual IEnumerator BulletLifeTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        pooler.ReturnToPool(this.gameObject);
    }
}

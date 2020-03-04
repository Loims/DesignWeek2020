using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [SerializeField] private ObjectPooler pooler;
    [SerializeField] private GameObject projectile;
    [SerializeField] private ZarStates states;
    [SerializeField] private Projectile.Color color;
    [SerializeField] private bool isFiring;
    [SerializeField] private bool coroutineStarted = false;

    private void OnEnable()
    {
        projectile = Resources.Load<GameObject>("BasicProjectile");
        states = transform.parent.parent.GetComponent<ZarStates>();
    }


    void Update()
    {
        if(pooler == null)
        {
            pooler = ObjectPooler.instance;
        }

        isFiring = states.isShooting;
        if (isFiring)
        {
            Debug.Log("ISFIRING");
            color = states.color;
            if (!coroutineStarted)
            {
                StartCoroutine(ShootCoroutine(0.05f));
                coroutineStarted = true;
            }
        }
        else
        {
            StopAllCoroutines();
            coroutineStarted = false;
        }
    }

    private IEnumerator ShootCoroutine(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Debug.Log("SHOOT");
            GameObject newProjectile = pooler.NewObject(projectile, transform.position, Quaternion.identity);
            Projectile projectileComp = newProjectile.GetComponent<Projectile>();

            projectileComp.InitializeBulletVelocity(transform.up * 10f);
            if (color == Projectile.Color.RED)
            {
                projectileComp.AssignColor(Projectile.Color.BLUE);
            }
            else if (color == Projectile.Color.BLUE)
            {
                projectileComp.AssignColor(Projectile.Color.RED);
            }
        }
    }
}

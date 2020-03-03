using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooter : MonoBehaviour
{
    ObjectPooler pooler;

    public GameObject projectilePrefab;
    public Transform gun;
    private bool canShoot = true;
    [SerializeField]
    private float force;
    public float bulletSpeed;

    public Projectile.Color color;

    private void OnEnable()
    {
        pooler = ObjectPooler.instance;
        if(this.gameObject.tag == "Red")
        {
            color = Projectile.Color.RED;
        }
        else if (this.gameObject.tag == "Blue")
        {
            color = Projectile.Color.BLUE;
        }
        else if (this.gameObject.tag == "Purple")
        {
            color = Projectile.Color.PURPLE;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootBullet();
    }

    void shootBullet()
    {
        if (canShoot == true)
        {
            GameObject spawnedProjectile = pooler.NewObject(projectilePrefab, gun.transform.position, Quaternion.identity);

            Projectile component = spawnedProjectile.GetComponent<Projectile>();
            component.InitializeBulletVelocity(transform.up * force);
            component.AssignColor(color);


            //spawnedProjectile.GetComponent<Rigidbody2D>().AddForce(transform.up * force, ForceMode2D.Impulse);
            StartCoroutine(spawnSpeed());
        }
    }
    IEnumerator spawnSpeed()
    {
        canShoot = false;
        yield return new WaitForSeconds(bulletSpeed);
        canShoot = true;
    }




}

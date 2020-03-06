using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayercontrol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletVelocity = 500;
    public float planeSpeed = 10.0f;
    private Transform plane;
    private GameObject Bullet;
    bool muzzle;
    public GameObject muzzleFlash;
    float muzzleTimer = 0;
    AudioSource fireSFX;

    void Start()
    {
        plane = GetComponent<Transform>();
        fireSFX = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal2");
        float y = Input.GetAxis("Vertical2");
        plane.transform.position += new Vector3(x,y,0)*Time.deltaTime* planeSpeed;
        
        if (Input.GetButtonDown("Jump"))
        {
            Fire();
            muzzle = true;
            fireSFX.Play();
        }
        Destroy(Bullet, 1);


        if (muzzle)
        {
            muzzleFlash.SetActive(true);
            muzzleTimer += Time.deltaTime;
            if (muzzleTimer >= 0.07)
            {
                muzzleFlash.SetActive(false);
                muzzleTimer = 0;
                muzzle = false;
            }
        }

    }





    private void Fire()
    {
        Bullet = Instantiate(bulletPrefab, plane.position, plane.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletVelocity);
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Projectile>().projectileColor==Projectile.Color.BLUE)
        {
            Player.instance.DecreasePlayer1Health(5);
        }
    }
}

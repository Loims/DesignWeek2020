using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPlayercontrol : MonoBehaviour
{
    public float planespeed = 10.0f;
    public GameObject bulletPrefab;
    public float bulletVelocity = 500;
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
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        // planeRotation.rotation = Quaternion.Euler(new Vector3(0, 0, x));
        // planeRotation.eulerAngles -= new Vector3(0, 0, x);
        plane.transform.position += new Vector3(x, y, 0) * Time.deltaTime * planespeed;
        if(Input.GetButtonDown("Fire1"))
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
       Bullet= Instantiate(bulletPrefab, plane.position, plane.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up* bulletVelocity);
        
    }
   

}

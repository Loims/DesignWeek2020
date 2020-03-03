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
    void Start()
    {
        plane = GetComponent<Transform>();
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
        }
        Destroy(Bullet, 1);
    }
    private void Fire()
    {
        Bullet = Instantiate(bulletPrefab, plane.position, plane.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletVelocity);
        
    }
}

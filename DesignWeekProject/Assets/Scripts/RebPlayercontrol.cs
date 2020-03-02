using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebPlayercontrol : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject bullet;
    public float velocity = 500;
    private Transform plane;
    void Start()
    {
        plane = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("vertical");
        // planeRotation.rotation = Quaternion.Euler(new Vector3(0, 0, x));
        // planeRotation.eulerAngles -= new Vector3(0, 0, x);
        plane.transform.position += new Vector3(x, 0, 0) * Time.deltaTime * speed;
        if(Input.GetButton("Fire1"))
        {
            GameObject Bullet = Instantiate(bullet, plane.position, plane.rotation);
            Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
        }

    }
    private void Fire()
    {
       GameObject Bullet= Instantiate(bullet, plane.position, plane.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up*velocity);
        Destroy(Bullet, 5);
    }
    
}

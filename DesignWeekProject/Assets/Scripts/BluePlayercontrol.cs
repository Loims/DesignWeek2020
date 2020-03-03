using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePlayercontrol : MonoBehaviour
{
    public GameObject bullet;
    public float velocity = 500;
    public float speed = 10.0f;
    private Transform plane;
    void Start()
    {
        plane = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal2");
        plane.transform.position += new Vector3(x, 0,0)*Time.deltaTime*speed;
        if (Input.GetButton("Jump"))
        {
            GameObject Bullet = Instantiate(bullet, plane.position, plane.rotation);
            Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
        }
    }
    private void Fire()
    {
        GameObject Bullet = Instantiate(bullet, plane.position, plane.rotation);
        Bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * velocity);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private ObjectPooler pooler;

    public GameObject bullet;

    private void Start()
    {
        pooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameObject newBullet = pooler.NewObject(bullet, this.transform);
            newBullet.GetComponent<Projectile>().InitializeBulletVelocity(Vector3.up * 10f);
            Debug.Log(bullet.GetComponent<Rigidbody2D>());
        }
    }
}

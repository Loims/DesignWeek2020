using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    public string targetType = "blue";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BlueEnemy>() != null)
        {
            Destroy(collision.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarMove : MonoBehaviour
{
    public static bool orderGiven;
    public float moveSpeed;
    public Vector3 endPos;
    public Vector3 refVelocity = Vector3.zero;

    void Start()
    {
        endPos = new Vector3(transform.position.x + 20, transform.position.y - 10, transform.position.z);
        orderGiven = false;
    }

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, endPos, ref refVelocity, 1.5f);
        if (transform.position.x > 7)
        {
            if (orderGiven == false)
            {
                orderGiven = true;
            }
            Destroy(gameObject);
        }
    }
}

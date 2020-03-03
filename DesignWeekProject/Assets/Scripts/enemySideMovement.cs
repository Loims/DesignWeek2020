using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySideMovement : MonoBehaviour
{
    [SerializeField]
    private float movementSpeed;
    Vector3 PointA;
    Vector3 PointB;

    [SerializeField]
    private float UpDown;
    [SerializeField]
    private float LeftRight;
    // Use this for initialization
    void Start()
    {

        PointA = new Vector3(transform.position.x, transform.position.y, 0);
        PointB = new Vector3(transform.position.x + LeftRight, transform.position.y + UpDown, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float time = Mathf.PingPong(Time.time * movementSpeed, 1);
        transform.position = Vector3.Lerp(PointA, PointB, time);
    }
}

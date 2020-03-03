using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpray : MonoBehaviour
{
    public GameObject rotPoint;
    public float time;
    int direction = 1;
    public float RotationSpeed;
    bool circleSpray = false;
    bool halCircleSpray = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (halCircleSpray)
        {
            time += Time.deltaTime;

            transform.RotateAround(rotPoint.transform.position, new Vector3(0, 0, 1 * direction), RotationSpeed * Time.deltaTime);
            if (time > 2.0f)
            {
                direction = -1 * direction;
                time = 0.0f;
            }
        }
        if (circleSpray)
        {
            transform.RotateAround(rotPoint.transform.position, new Vector3(0, 0, 1), RotationSpeed * Time.deltaTime);
        }
    }
}


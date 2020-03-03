using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCredits : MonoBehaviour
{
    public Vector3 cameraPos;
    public Vector3 cameraMovedPos;
    public Vector3 refVelocity = Vector3.zero;
    public bool movedDown;
    public bool movedUp;

    void Start()
    {
        cameraPos = transform.position;
        cameraMovedPos = new Vector3(0, -10, -10);
        movedDown = false;
        movedUp = false;
    }

    void LateUpdate()
    {
        if (CreditPressed.credits == true && movedDown == false)
        {
            MoveDown();
        }

        if (CreditPressed.back == true && movedUp == false)
        {
            MoveUp();
        }
    }

    void MoveDown()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraMovedPos, ref refVelocity, 0.75f);
        if (transform.position.y < -9.99)
        {
            movedDown = true;
            movedUp = false;
        }
    }

    void MoveUp()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref refVelocity, 0.75f);
        if (transform.position.y > -0.01 )
        {
            movedUp = true;
            movedDown = false;
        }
    }
}

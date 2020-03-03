using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMove : MonoBehaviour
{
    public bool movedCanvasUp;
    public bool movedCanvasDown;

    void Start()
    {
        movedCanvasUp = false;
        movedCanvasDown = true;
    }
    void Update()
    {
        if (CreditPressed.credits == true && movedCanvasUp == false)
        {
            MoveUp();
        }

        if (CreditPressed.back == true && movedCanvasDown == false)
        {
            MoveDown();
        }
    }

    void MoveUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 7.5f, transform.position.z);
        movedCanvasUp = true;
        movedCanvasDown = false;
    }

    void MoveDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 7.5f, transform.position.z);
        movedCanvasDown = true;
        movedCanvasUp = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToScores : MonoBehaviour
{
    public Vector3 cameraPos;
    public Vector3 cameraMovedPos;
    public Vector3 refVelocity = Vector3.zero;
    public bool movedLeft;
    public bool movedRight;

    void Start()
    {
        cameraPos = transform.position;
        cameraMovedPos = new Vector3(-12.5f, 0, -10);
        movedLeft = false;
        movedRight = false;
    }

    void LateUpdate()
    {
        if (ScorePressed.scores == true && movedLeft == false)
        {
            MoveLeft();
        }

        if (ScorePressed.back2 == true && movedRight == false)
        {
            MoveRight();
        }
    }

    void MoveLeft()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraMovedPos, ref refVelocity, 0.75f);
        if (transform.position.x < -12.49)
        {
            movedLeft = true;
            movedRight = false;
        }
    }

    void MoveRight()
    {
        transform.position = Vector3.SmoothDamp(transform.position, cameraPos, ref refVelocity, 0.75f);
        if (transform.position.x > -0.01)
        {
            movedRight = true;
            movedLeft = false;
        }
    }
}

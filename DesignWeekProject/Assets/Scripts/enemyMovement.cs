using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public GameObject explosion;
    public GameObject scrapMetal;

    public Vector3 StartPos;
    public Vector3 ControlPoint;
    public Vector3 EndPos;
    public float count = 0.0f;
    public float speed;
    void Start()
    {
        ControlPoint = StartPos + (EndPos - StartPos) / 2 + Vector3.up * -5.0f; // Play with 5.0 to change the curve
    }

    // Update is called once per frame
    void Update()
    {

        if (count < 1.0f)
        {
            count += 1.0f * speed * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(StartPos, ControlPoint, count);
            Vector3 m2 = Vector3.Lerp(ControlPoint, EndPos, count);
            Enemy.transform.position = Vector3.Lerp(m1, m2, count);

        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawn : MonoBehaviour
{
    public float timer;
    public float timerLimit;
    public GameObject shootingStar;

    void Start()
    {
        timer = 0;
        timerLimit = 799;
    }

    void FixedUpdate()
    {
        if (StarMove.orderGiven == true)
        {
            timer += 1;
        }
    }

    void Update()
    {
        if (timer > timerLimit)
        {
            Instantiate(shootingStar, new Vector3(0, 5.5f, 3), Quaternion.Euler(new Vector3(0, 0, 55)));
            timer = 0;
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject circle;
    public GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(circle, new Vector3(-3, 0, 0), Quaternion.identity);

        if (OnePlayerStart.playerCount == 2)
        {
            Instantiate(square, new Vector3(3, 0, 0), Quaternion.identity);
        }
    }
}

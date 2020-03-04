using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject circle;
    public GameObject square;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerStart.playerCount == 1)
        {
            Instantiate(circle, new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (PlayerStart.playerCount == 2)
        {
            Instantiate(square, new Vector3(3, 0, 0), Quaternion.identity);
            Instantiate(circle, new Vector3(-3, 0, 0), Quaternion.identity);
        }
    }
}

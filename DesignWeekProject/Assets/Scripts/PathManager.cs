using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    private Transform[] controlpoints;
    int a = 0;

    [System.NonSerialized] public Vector2 p0, p1, p2, p3;
    float time = 0.0f;
    float timeLimit = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        a = this.transform.childCount;
        controlpoints = new Transform[a];
        for (int i = 0; i < a; i++)
        {
            controlpoints[i] = this.transform.GetChild(i);
        }
        RandomPoints();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time>=timeLimit)
        {
            RandomPoints();
            time = 0;
        }
    }
    private void RandomPoints()
    {
        p0 = controlpoints[Random.Range(0, a)].position;
        p1 = controlpoints[Random.Range(0, a)].position;
        p2 = controlpoints[Random.Range(0, a)].position;
        p3 = controlpoints[Random.Range(0, a)].position;
    }
}

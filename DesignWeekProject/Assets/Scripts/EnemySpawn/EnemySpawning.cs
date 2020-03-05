using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    float time = 0;
    float sleepTime = 0;
    public float sleepTimeLimit = 2.0f;
    [SerializeField]public float timeLimit = 0.5f;
    public GameObject[] enemy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sleepTime += Time.deltaTime;
        if (sleepTime < sleepTimeLimit)
        {
            time += Time.deltaTime;
            if (time >= timeLimit)
            {
                Instantiate(enemy[Random.Range(0,enemy.Length)], transform.position, transform.rotation);
                time = 0;
            }
        }
        else if(sleepTime>5.0f)
        {
            time = 0;
            sleepTime = 0;
          
        }
    }

}

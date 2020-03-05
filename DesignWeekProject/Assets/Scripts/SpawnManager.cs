using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int scoreLimit;
 
    public GameObject []BossSpawner;
    public GameObject[] enemySpawners;
    private GameObject boss;
    void Start()
    {
        
    }

   
    void FixedUpdate()
    {
        if(Player.instance.ShowTotalScoreInt()>scoreLimit)
        {
            boss=Instantiate(BossSpawner[Random.Range(0, BossSpawner.Length)], transform.position, transform.rotation);
            scoreLimit += scoreLimit;
            for(int i=0;i< enemySpawners.Length;i++)
            {
                enemySpawners[i].SetActive(false);
            }
        }
    }
}

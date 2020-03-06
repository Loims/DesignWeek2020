using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if(Player.instance.ShowTotalScoreInt()>scoreLimit&& !Player.instance.boss)
        {
            boss=Instantiate(BossSpawner[Random.Range(0, BossSpawner.Length)], transform.position, transform.rotation);
            scoreLimit += scoreLimit;
            Player.instance.boss = true;
            for(int i=0;i< enemySpawners.Length;i++)
            {
                enemySpawners[i].SetActive(false);
            }
        }
        if(!enemySpawners[0].activeSelf&&!Player.instance.boss)
        {
            for (int i = 0; i < enemySpawners.Length; i++)
            {
                enemySpawners[i].SetActive(true);
            }
        }
        if(Player.instance.ShowPlayer1Health()<0.0f)
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}

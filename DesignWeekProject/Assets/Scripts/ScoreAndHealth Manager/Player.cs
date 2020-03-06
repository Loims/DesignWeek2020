using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public static float player1Health = 100;
    public static float player2Health = 100;
    public static int player1Score = 0;
    public static int player2Score = 0;
    public static Player instance = null;
    public bool boss = false;
    public float bulletSpeed = 1f;
    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
        }
        else if(instance!=null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float ShowPlayer1Health()
    {
        return player1Health;
    }
    public float ShowPlayer2Health()
    {
        return player2Health;
    }
    public void DecreasePlayer1Health(float attackValue)
    {
        player1Health -=  attackValue;
    }
    public void DecreasePlayer2Health(float attackValue)
    {
        player2Health -= attackValue;
    }
    public void AddPlayer1Score(int score)
    {
        player1Score += score;
    }
    public string Showplayer1Score()
    {
        return player1Score.ToString();
    }
    public void AddPlayer2Score(int score)
    {
        player2Score += score;
    }
    public string Showplayer2Score()
    {
        return player2Score.ToString();
    }
    public string ShowTotalScore()
    {
        return (player1Score + player2Score).ToString();
    }
    public int ShowTotalScoreInt()
    {
        return (player1Score + player2Score);
    }
    public void Restart()
    {
        player1Health = 100;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScores : MonoBehaviour
{
    //Arrays to store high scores and names, stores top 20
    public static string[] names;
    public static int[] scores;

    void Start()
    {
        names = new string[19];
        scores = new int[19];
    }
}

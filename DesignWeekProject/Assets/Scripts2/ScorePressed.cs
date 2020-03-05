using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePressed : MonoBehaviour
{
    public static bool scores;
    public static bool back2;

    void Start()
    {
        scores = false;
        back2 = false;
    }

    public void ScoresClicked()
    {
        scores = true;
        back2 = false;
    }

    public void BackClicked2()
    {
        back2 = true;
        scores = false;
    }
}

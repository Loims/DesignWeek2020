using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScorePressed : MonoBehaviour
{
    public static bool scores;
    public static bool back2;
    public GameObject Highscores;
    public GameObject MainMenu;

    void Start()
    {
        scores = false;
        back2 = false;
    }

    public void ScoresClicked()
    {
        Highscores.SetActive(true);

        MainMenu.SetActive(false);
    }

    public void BackClicked2()
    {
        Highscores.SetActive(false);
        MainMenu.SetActive(true);

    }
}

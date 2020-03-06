using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStart : MonoBehaviour
{
    public static int playerCount;

    public void StartPlayers()
    {
        if (this.name == "1P Button")
        {
            SceneManager.LoadScene("Main_Stage");
            playerCount = 1;
        }

        if (this.name == "2P Button")
        {
            SceneManager.LoadScene("MainGame");
            playerCount = 2;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnePlayerStart : MonoBehaviour
{
    public static int playerCount;

    public void StartOnePlayer()
    {
        SceneManager.LoadScene("Main_Stage");
        playerCount = 1;
    }
}

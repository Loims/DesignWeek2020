using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreInput : MonoBehaviour
{
    public Text[] tBox;
    Text currentText = null;
    int boxCounter = 0;
    int tBoxCouner1;
    int tBoxCouner2;
    char[] box1 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    int box1Counter = 0;
    char[] box2 = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    int box2Counter = 0;


    // Start is called before the first frame update
    void Start()
    {
        currentText = tBox[0];
        tBox[0].text = box1[0].ToString();
        tBox[1].text = box2[0].ToString();
        Debug.Log(PlayerPrefs.GetInt("HighScore", 0));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            boxCounter++;
            if(boxCounter>1)
            {
                boxCounter = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (boxCounter == 0)
            {
                box1Counter++;
                if (box1Counter >= box1.Length)
                {
                    box1Counter = 0;
                }
                tBox[0].text = box1[box1Counter].ToString();
            }
            else
            {
                box2Counter++;
                if (box2Counter >= box2.Length)
                {
                    box2Counter = 0;
                }
                tBox[1].text = box2[box2Counter].ToString();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            if (boxCounter == 0)
            {
                box1Counter--;
                if (box1Counter <0)
                {
                    box1Counter = box1.Length-1;
                }
                tBox[0].text = box1[box1Counter].ToString();
            }
            else
            {
                box2Counter--;
                if (box2Counter <= 0)
                {
                    box2Counter = box2.Length-1 ;
                }
                tBox[1].text = box2[box2Counter].ToString();
            }
        }
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            
            PlayerPrefs.SetString("Name", tBox[0].text+" " +tBox[1].text);
            // PlayerPrefs.SetInt("HighScore", Player.instance.ShowTotalScoreInt());
            PlayerPrefs.SetInt("HighScore", 100);
            Debug.Log(PlayerPrefs.GetInt("HighScore", 0));
            Debug.Log(PlayerPrefs.GetString("Name"," 0"));
        }
    }
}

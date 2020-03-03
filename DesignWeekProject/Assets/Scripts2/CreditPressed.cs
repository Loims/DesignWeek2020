using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditPressed : MonoBehaviour
{
    public static bool credits;
    public static bool back;

    void Start()
    {
        credits = false;
        back = false;
    }

    public void CreditsClicked()
    {
        credits = true;
        back = false;
    }

    public void BackClicked()
    {
        back = true;
        credits = false;
    }
}

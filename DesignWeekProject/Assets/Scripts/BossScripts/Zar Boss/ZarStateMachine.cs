using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZarStateMachine : MonoBehaviour
{
    #region Variables
    public float health;
    GameObject target;
    [SerializeField] private ZarStates states;
    #endregion

    /// <summary>
    /// Called on object enable. Resets all variables to their default values 
    /// to ensure AI runs properly
    /// </summary>
    private void OnEnable()
    {
        states = GetComponent<ZarStates>();
        health = 100f;
        states.currentState = ZarStates.ZStates.SPAWN;
        target = GameObject.Find("PFB_Player");
    }

    private void Start()
    {
        if (states.currentState == ZarStates.ZStates.NULL)
        {
            states.currentState = ZarStates.ZStates.SPAWN;
        }

    }

    private void Update()
    {
        if (health <= 0f)
        {
            Kill();
        }
        StateMachine();
    }

    /// <summary>
    /// Agent state machine. Handles state changing and state execution
    /// </summary>
    void StateMachine()
    {
        switch (states.currentState)
        {
            case ZarStates.ZStates.SPAWN:
                {
                    states.Spawn();
                    break;
                }
            case ZarStates.ZStates.COLORIZE:
                {
                    states.Colorize();
                    break;
                }
            case ZarStates.ZStates.MOVE:
                {
                    states.Move();
                    break;
                }
            case ZarStates.ZStates.ATTACK:
                {
                    states.Attack();
                    break;
                }
            case ZarStates.ZStates.RETREAT:
                {
                    states.Retreat();
                    break;
                }
        }
    }

    private void Kill()
    {
        throw new NotImplementedException();
    }
}


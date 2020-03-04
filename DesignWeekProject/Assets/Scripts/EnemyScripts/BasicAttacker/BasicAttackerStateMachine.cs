using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicAttackerStateMachine : MonoBehaviour
{
    #region Variables
    public float health;
    [SerializeField] private BasicAttackerStates states;
    #endregion

    /// <summary>
    /// Called on object enable. Resets all variables to their default values 
    /// to ensure AI runs properly
    /// </summary>
    private void OnEnable()
    {
        states = GetComponent<BasicAttackerStates>();
        health = 100f;
        states.currentState = BasicAttackerStates.BAStates.NULL;
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
            case BasicAttackerStates.BAStates.ATTACK:
                {
                    states.Attack();
                    break;
                }
        }
        states.Move();
    }

    private void Kill()
    {
        throw new NotImplementedException();
    }
}


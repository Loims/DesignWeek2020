using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DualShockerStateMachine : MonoBehaviour
{
    #region Variables
    [SerializeField] private DualShockerStates states;
    #endregion

    /// <summary>
    /// Called on object enable. Resets all variables to their default values 
    /// to ensure AI runs properly
    /// </summary>
    private void OnEnable()
    {
        states = GetComponent<DualShockerStates>();
        states.currentState = DualShockerStates.DSStates.SPAWN;
    }

    private void Start()
    {
        if (states.currentState == DualShockerStates.DSStates.NULL)
        {
            states.currentState = DualShockerStates.DSStates.SPAWN;
        }

    }

    private void Update()
    {
        StateMachine();
    }

    /// <summary>
    /// Agent state machine. Handles state changing and state execution
    /// </summary>
    void StateMachine()
    {
        switch (states.currentPhase)
        {
            case DualShockerStates.DSPhases.PHASE1:
                {
                    switch (states.currentState)
                    {
                        case DualShockerStates.DSStates.SPAWN:
                            {
                                states.Spawn();
                                break;
                            }
                        case DualShockerStates.DSStates.MOVEPHASE1:
                            {
                                states.MovePhase1();
                                states.ColoredAttack();
                                break;
                            }
                    }
                    break;
                }
            case DualShockerStates.DSPhases.PHASE2:
                {
                    switch (states.currentState)
                    {
                        case DualShockerStates.DSStates.TRANSITION:
                            {
                                states.Transition();
                                break;
                            }
                        case DualShockerStates.DSStates.MOVEPHASE2:
                            {
                                states.MovePhase2();
                                break;
                            }
                    }
                    break;
                }
                //case ZarStates.ZStates.COLORIZE:
                //    {
                //        states.Colorize();
                //        break;
                //    }
                //case ZarStates.ZStates.MOVE:
                //    {
                //        states.Move();
                //        break;
                //    }
                //case ZarStates.ZStates.ATTACK:
                //    {
                //        states.Attack();
                //        break;
                //    }
                //case ZarStates.ZStates.RETREAT:
                //    {
                //        states.Retreat();
                //        break;
                //    }
        }
    }

    private void Kill()
    {
        throw new NotImplementedException();
    }
}


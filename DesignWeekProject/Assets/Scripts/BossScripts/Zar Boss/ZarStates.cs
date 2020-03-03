using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZarStates : MonoBehaviour
{
    #region Variables
    public enum ZStates
    {
        NULL,
        SPAWN,
        COLORIZE,
        MOVE,
        ATTACK,
        RETREAT
    }

    public ZStates currentState;

    private GameObject cornerParent;
    [SerializeField] private List<GameObject> points;
    #endregion

    private void OnEnable()
    {
        points = new List<GameObject>();
        cornerParent = GameObject.Find("ZarPoints");

        for(int i=0;i<cornerParent.transform.childCount;i++)
        {
            points.Add(cornerParent.transform.GetChild(i).gameObject);
        }
    }

    public void Spawn()
    {
        Vector3 vectorToSpawn = (points[4].transform.position - transform.position);
        float distanceToSpawn = vectorToSpawn.magnitude;
        Debug.DrawLine(transform.position, transform.position + vectorToSpawn, Color.green);
    }

    public void ChangeState(ZStates newState) //Changes state from current state to newState
    {
        currentState = newState;
    }
}
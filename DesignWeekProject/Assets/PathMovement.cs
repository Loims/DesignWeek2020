using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMovement : MonoBehaviour
{
    private Transform[] controlpoints;
    int a = 0;
    Vector2 p0, p1, p2, p3;
    [SerializeField]
    public GameObject points;
    
    private Vector2 gizmoPosition;

    [SerializeField]
    private Transform[] routes;
    private int routeToGo;
    private float tParam;
    private Vector2 enemyPositin;
    private float speedModifier;
    private bool coroutineAllowed;
    void Start()
    {
        routeToGo = 0;
        tParam = 0;
        speedModifier = 0.3f;
        coroutineAllowed = true;

        a = points.transform.childCount;
        controlpoints = new Transform[a];
        for (int i = 0; i < a; i++)
        {
            controlpoints[i] = points.transform.GetChild(i);
        }
        RandomPoints();
    }

    // Update is called once per frame
    void Update()
    {
       if (coroutineAllowed)
            StartCoroutine(GoByTheRoute(routeToGo));
    }
    private IEnumerator GoByTheRoute(int routeNumber)
    {
        coroutineAllowed = false;
        



        while (tParam<1)
        {
            tParam += Time.deltaTime * speedModifier;
            enemyPositin= Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
           
            transform.position = enemyPositin;
            yield return new WaitForEndOfFrame();
        }
        //if(this.gameObject!=null)
        //{
        //    Destroy(this.gameObject);
        //}
        tParam = 0;
        RandomPoints();
        coroutineAllowed = true;
    }
    private void OnDrawGizmos()
    {
        if(controlpoints!=null)
        {
            for (float t = 0; t <= 1; t += 0.05f)
            {
                gizmoPosition = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 + 3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;
                Gizmos.DrawSphere(gizmoPosition, 0.25f);
            }
        }
    }
    private void RandomPoints()
    {
        p0 = controlpoints[Random.Range(0, a)].position;
        p1 = controlpoints[Random.Range(0, a)].position;
        p2 = controlpoints[Random.Range(0, a)].position;
        p3 = controlpoints[Random.Range(0, a)].position;
    }
}

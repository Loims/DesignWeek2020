using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathMovement : MonoBehaviour
{
    Vector2 p0, p1, p2, p3;
    private float tParam;
    private Vector2 enemyPositin;
    [SerializeField] private float speedModifier = 0.3f;
    private bool coroutineAllowed;

    void Start()
    {
        GameObject rand = GameObject.Find("RandomcurvePoints");
        if (rand != null)
        {
            p0 = rand.GetComponent<PathManager>().p0;
            p1 = rand.GetComponent<PathManager>().p1;
            p2 = rand.GetComponent<PathManager>().p2;
            p3 = rand.GetComponent<PathManager>().p3;

        }
        tParam = 0;
        //speedModifier = 0.3f;
        coroutineAllowed = true;

    }

    void Update()
    {
        if (coroutineAllowed)
            StartCoroutine(GoByTheRoute());
    }
    private IEnumerator GoByTheRoute()
    {
        coroutineAllowed = false;




        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            enemyPositin = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;

            transform.position = enemyPositin;
            yield return new WaitForEndOfFrame();
        }
        if(this.gameObject!=null)
        {
            Destroy(this.gameObject);
        }
    }

}
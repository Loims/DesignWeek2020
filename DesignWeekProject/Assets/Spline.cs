
using UnityEngine;
using UnityEngine.U2D;

public class Spline:MonoBehaviour
{
    private Transform[] controlpoints;
    int a = 0;
   [SerializeField]
    public GameObject points;
    
    private Vector2 gizmoPosition;
    private void OnDrawGizmos()
    {
        a = points.transform.childCount;
        controlpoints = new Transform[a];
        for (int i = 0; i < a; i++)
        {
            controlpoints[i] = points.transform.GetChild(i);
        }
        for (float t = 0; t <= 1; t += 0.05f)
        {
            gizmoPosition = Mathf.Pow(1 - t, 3) * controlpoints[Random.Range(0,a)].position + 3 * Mathf.Pow(1 - t, 2) * t * controlpoints[Random.Range(0, a)].position + 3 * (1 - t) * Mathf.Pow(t , 2) * controlpoints[Random.Range(0, a)].position + Mathf.Pow(t, 3) * controlpoints[Random.Range(0, a)].position;
            Gizmos.DrawSphere(gizmoPosition, 0.25f);
        }
      //  Gizmos.DrawLine(new Vector2(controlpoints[0].position.x, controlpoints[0].position.y), new Vector2(controlpoints[1].position.x, controlpoints[1].position.y));
       // Gizmos.DrawLine(new Vector2(controlpoints[2].position.x, controlpoints[2].position.y), new Vector2(controlpoints[3].position.x, controlpoints[3].position.y));

    }
    private void Start()
    {
       a= points.transform.childCount;
        controlpoints = new Transform[a];
        for(int i= 0;i<a;i++)
        {
            controlpoints[i] = points.transform.GetChild(i);
        }
    }
    private void Update()
    {
        
    }
}
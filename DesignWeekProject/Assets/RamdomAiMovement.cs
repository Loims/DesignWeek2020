using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamdomAiMovement : MonoBehaviour
{
    private bool isMoving = false;
    private Vector3 newPosition;
    public float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving)
        {
            newPosition = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(-1.0f, 4.0f), -1.442932f);
            
            isMoving = true;
        }
        else
        {
           transform.position= Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime*speed);
           // transform.Translate(newPosition * Time.deltaTime);
        }

        if(Vector3.Distance(transform.position,newPosition)<0.1f)
        {
            isMoving = false;
        }
    }
}

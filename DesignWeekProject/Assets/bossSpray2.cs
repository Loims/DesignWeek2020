using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSpray2 : MonoBehaviour

{
    public GameObject rotPoint;
  
    int direction = 1;
    public float RotationSpeed;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
    

        transform.RotateAround(rotPoint.transform.position, new Vector3(0, 0, 1), RotationSpeed * Time.deltaTime);
       
    }
}


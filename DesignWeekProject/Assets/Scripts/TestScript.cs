using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    private ObjectPooler pooler;

    public GameObject AI;

    private void Start()
    {
        pooler = ObjectPooler.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            GameObject newAI = pooler.NewObject(AI, this.transform);
        }
    }
}

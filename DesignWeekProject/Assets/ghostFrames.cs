using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostFrames : MonoBehaviour
{


    public float ghostDelay;
    private float ghostDelaySeconds;
    public GameObject ghost;
    public bool makeGhost = true;
    
    void Start()
    {
        ghostDelaySeconds = ghostDelay;
    }

    
    void Update()
    {
        if (makeGhost = true)
        {
            if (ghostDelaySeconds > 0)
            {
                ghostDelaySeconds -= Time.deltaTime;
            }
            else
            {
                //generate a ghost
                GameObject currentGhost = Instantiate(ghost, transform.position, transform.rotation);
                currentGhost.transform.localScale = transform.localScale;
                ghostDelaySeconds = ghostDelay;
            }

        }
    }
}

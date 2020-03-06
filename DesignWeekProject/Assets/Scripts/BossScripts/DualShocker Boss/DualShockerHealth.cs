using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShockerHealth : MonoBehaviour
{
    private DualShockerStates statesParent;

    private float health;

    private void OnEnable()
    {
        statesParent = GetComponent<DualShockerStates>();
        health = 50f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            statesParent.ChangeState(DualShockerStates.DSStates.DIE);
            StartCoroutine(DeathAnimation(0.1f)); ;
        }
    }

    private IEnumerator DeathAnimation(float waitTime)
    {
        //Start animation here
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}

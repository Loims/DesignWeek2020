using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShockerHealth : MonoBehaviour
{
    private float health;

    private void OnEnable()
    {
        health = 30f;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(health <= 0f)
        {
            Destroy(this.gameObject);
        }
    }
}

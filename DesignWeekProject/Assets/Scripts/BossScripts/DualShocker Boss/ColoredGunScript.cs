using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColoredGunScript : MonoBehaviour
{
    private DualShockerStates parentStates;

    [SerializeField] private Projectile.Color color;

    private float shootdelay;

    private void OnEnable()
    {
        parentStates = transform.parent.parent.GetComponent<DualShockerStates>();

        if(gameObject.tag == "Red")
        {
            color = Projectile.Color.RED;
        }
        if(gameObject.tag == "Blue")
        {
            color = Projectile.Color.BLUE;
        }
    }
}

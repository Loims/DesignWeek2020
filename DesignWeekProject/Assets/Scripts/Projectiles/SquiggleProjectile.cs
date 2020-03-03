using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquiggleProjectile : Projectile
{
    private float squiggleInterpolator = 0f;
    private float squiggleScale = 2f;

    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        squiggleInterpolator = 0f;
        base.OnDisable();
    }

    private void Update()
    {
        Vector3 leftSide = Vector3.right * -2f;
        Vector3 rightSide = Vector3.right * 2f;
        squiggleInterpolator = Mathf.Sin(Time.time);

        transform.position = new Vector3(Mathf.Lerp(leftSide.x, rightSide.x, squiggleInterpolator), transform.position.y, transform.position.z);
    }
}

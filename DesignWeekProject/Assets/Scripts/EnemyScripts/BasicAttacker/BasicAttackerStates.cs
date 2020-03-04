using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAttackerStates : MonoBehaviour
{
    public enum BAStates
    {
        NULL,
        ATTACK
    }

    public BAStates currentState;

    [SerializeField] private ObjectPooler pooler;

    [SerializeField] private Camera camera;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Projectile.Color color;

    [SerializeField] private Vector2 spawnPoint;
    [SerializeField] private Vector2 endPoint;
    [SerializeField] private float arcHeight;
    [SerializeField] private float moveInterpolator;

    [SerializeField] private bool isAttacking = false;

    private void OnEnable()
    {
        pooler = ObjectPooler.instance;

        projectile = Resources.Load<GameObject>("BasicProjectile");

        if (camera == null)
        {
            camera = Camera.main;
        }

        spawnPoint = transform.position;
        endPoint = GenerateEndPoint();
        arcHeight = -2.5f;
        moveInterpolator = 0;

        color = (Projectile.Color)Random.Range(1, 3);
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        if(pooler == null)
        {
            pooler = ObjectPooler.instance;
        }
    }

    private void Update()
    {
        Debug.DrawLine(spawnPoint, endPoint, Color.green);
        Debug.DrawLine(spawnPoint, new Vector2(spawnPoint.x, arcHeight), Color.red);
    }

    public void Move()
    {
        transform.position = MathParabola.Parabola(spawnPoint, endPoint, arcHeight, moveInterpolator);
        moveInterpolator += Time.deltaTime / 4;
        if(moveInterpolator > 0.2)
        {
            ChangeState(BAStates.ATTACK);
        }

        if(moveInterpolator > 1)
        {
            isAttacking = false;
            StopAllCoroutines();
            pooler.ReturnToPool(this.gameObject);
        }
    }

    public void Attack()
    {
        if(!isAttacking)
        {
            StartCoroutine(AttackTimer(1f));
            isAttacking = true;
        }
    }

    private Vector2 GenerateEndPoint()
    {
        Vector2 endPointScreenSpace = new Vector2();
        Vector2 endPointWorldSpace = new Vector2();
        if(spawnPoint.x <= 0f)
        {
            endPointScreenSpace.x = (float)camera.pixelWidth + 50;
        }
        else
        {
            endPointScreenSpace.x = -50;
        }
        endPointScreenSpace.y = (camera.pixelHeight / 2);
        endPointWorldSpace = camera.ScreenToWorldPoint(endPointScreenSpace);
        return endPointWorldSpace;
    }

    private IEnumerator AttackTimer(float waitTime)
    {
        while (true)
        {
            GameObject newProjectile = pooler.NewObject(projectile, transform.position, Quaternion.identity);
            Projectile projectileComp = newProjectile.GetComponent<Projectile>();

            projectileComp.InitializeBulletVelocity(transform.up * -3f);
            projectileComp.AssignColor(color);
            yield return new WaitForSeconds(waitTime);
        }
    }

    public void ChangeState(BAStates newState) //Changes state from current state to newState
    {
        currentState = newState;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZarStates : MonoBehaviour
{
    #region Variables
    public enum ZStates
    {
        NULL,
        SPAWN,
        COLORIZE,
        MOVE,
        ATTACK,
        RETREAT,
        DIE
    }

    public ZStates currentState;

    [SerializeField] private GameObject gunGimbal;
    [SerializeField] private GameObject cornerParent;
    [SerializeField] private List<GameObject> points;
    [SerializeField] private int cornerIndex;

    [SerializeField] SpriteRenderer m_spriteRenderer;
    [SerializeField] private Sprite red;
    [SerializeField] private Sprite blue;
    [SerializeField] private Sprite purple;
    [SerializeField] private Sprite white;


    public Projectile.Color color;

    [SerializeField] private float speed = 5f;
    [SerializeField] private int cornerIncrement = 0;
    public bool isShooting = false;
    public bool invuln = true;
    #endregion

    private void OnEnable()
    {
        red = Resources.Load<Sprite>("boss_red");
        blue = Resources.Load<Sprite>("boss_blue");
        purple = Resources.Load<Sprite>("boss_purple");
        white = Resources.Load<Sprite>("boss_white");

        m_spriteRenderer = GetComponent<SpriteRenderer>();
        m_spriteRenderer.sprite = purple;
        color = Projectile.Color.PURPLE;

        points = new List<GameObject>();
        cornerParent = GameObject.Find("ZarPoints");
        gunGimbal = transform.GetChild(0).gameObject;

        for(int i=0;i<cornerParent.transform.childCount;i++)
        {
            points.Add(cornerParent.transform.GetChild(i).gameObject);
        }
    }

    public void Spawn()
    {
        Vector3 vectorToSpawn = (points[4].transform.position - transform.position);
        float distanceToSpawn = vectorToSpawn.magnitude;
        Debug.DrawLine(transform.position, transform.position + vectorToSpawn, Color.green);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToSpawn, 2f * Time.deltaTime);
        if (distanceToSpawn <= 0.01f)
        {
            Debug.Log("Change to colorize");
            color = (Projectile.Color)Random.Range(1, 3);
            ChangeState(ZStates.COLORIZE);
        }
    }

    public void Colorize()
    {
        StartCoroutine(ColorDelay(2f));
    }

    public void Move()
    {
        if(invuln)
        {
            invuln = false;
        }
        Vector3 vectorToCorner = (points[cornerIndex].transform.position - transform.position);
        float distanceToCorner = vectorToCorner.magnitude;
        Debug.DrawLine(transform.position, transform.position + vectorToCorner, Color.green);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToCorner, speed * Time.deltaTime);
        if (distanceToCorner <= 0.01f)
        {
            isShooting = true;
            cornerIncrement++;
            StartCoroutine(RepositionDelay(3f));
            ChangeState(ZStates.ATTACK);
        }
    }

    public void Attack()
    {
        gunGimbal.transform.RotateAround(transform.position, new Vector3(0, 0, 1), 120f * Time.deltaTime);
    }
    public void Retreat()
    {
        Vector3 vectorToSpawn = (points[4].transform.position - transform.position);
        float distanceToSpawn = vectorToSpawn.magnitude;
        Debug.DrawLine(transform.position, transform.position + vectorToSpawn, Color.green);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToSpawn, speed * Time.deltaTime);
        if (distanceToSpawn <= 0.01f)
        {
            Debug.Log("Change to colorize");
            if (color == Projectile.Color.RED)
            {
                color = Projectile.Color.BLUE;
            }
            else if(color == Projectile.Color.BLUE)
            {
                color = Projectile.Color.RED;
            }
            cornerIncrement = 0;
            ChangeState(ZStates.COLORIZE);
        }
    }

    public void Die()
    {
        StopAllCoroutines();
    }

    private void GetNewCornerIndex()
    {
        int newIndex = Random.Range(0, 4);
        while (newIndex == cornerIndex)
        {
            newIndex = Random.Range(0, 4);
        }
        cornerIndex = newIndex;
        return;
    }

    private IEnumerator ColorDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(color == Projectile.Color.RED)
        {
            m_spriteRenderer.sprite = red;
        }
        else if(color == Projectile.Color.BLUE)
        {
            m_spriteRenderer.sprite = blue;
        }
        yield return new WaitForSeconds(waitTime);
        GetNewCornerIndex();
        ChangeState(ZStates.MOVE);
        StopAllCoroutines();
    }

    private IEnumerator RepositionDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isShooting = false;
        if(cornerIncrement >= 5)
        {
            ChangeState(ZStates.RETREAT);
        }
        else
        {
            GetNewCornerIndex();
            ChangeState(ZStates.MOVE);
        }
        yield break;
    }

    public void StartFlash()
    {
        StartCoroutine(Flash(0.1f));
    }

    private IEnumerator Flash(float waitTime)
    {
        m_spriteRenderer.sprite = white;
        yield return new WaitForSeconds(waitTime);

        if (color == Projectile.Color.RED)
        {
            m_spriteRenderer.sprite = red;
        }
        else if (color == Projectile.Color.BLUE)
        {
            m_spriteRenderer.sprite = blue;
        }
        else if (color == Projectile.Color.PURPLE)
        {
            m_spriteRenderer.sprite = purple;
        }
        yield break;
    }

    public void ChangeState(ZStates newState) //Changes state from current state to newState
    {
        currentState = newState;
    }
}
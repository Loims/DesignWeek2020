using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DualShockerStates : MonoBehaviour
{
    public enum DSPhases
    { 
        PHASE1,
        PHASE2
    }

    public enum DSStates
    {
        NULL,
        SPAWN,
        MOVEPHASE1,
        TRANSITION,
        MOVEPHASE2
    }

    [SerializeField] private ObjectPooler pooler;

    private IEnumerator shootCorutine;

    public DSPhases currentPhase;
    public DSStates currentState;

    [SerializeField] private GameObject projectile;

    [SerializeField] private GameObject mainGunsParent;
    [SerializeField] private GameObject coloredGunsParent;
    [SerializeField] private GameObject coresParent;
    [SerializeField] private List<GameObject> mainGuns = new List<GameObject>();
    [SerializeField] private List<GameObject> coloredGuns = new List<GameObject>();
    [SerializeField] private List<GameObject> cores = new List<GameObject>();
    [SerializeField] private List<Transform> movePositions = new List<Transform>();

    int randomTransitionPoint;
    private float phase1Interpolator;
    private bool moveLeft;
    public bool isFiring = false;

    private bool shootPhase1Started = false;

    private void OnEnable()
    {
        pooler = ObjectPooler.instance;
        shootCorutine = ShootMainGuns(1.5f);

        projectile = Resources.Load<GameObject>("BasicProjectile");

        currentPhase = DSPhases.PHASE1;
        currentState = DSStates.SPAWN;

        mainGunsParent = transform.GetChild(0).gameObject;
        coloredGunsParent = transform.GetChild(1).gameObject;
        coresParent = transform.GetChild(2).gameObject;

        foreach(Transform child in mainGunsParent.transform)
        {
            mainGuns.Add(child.gameObject);
        }
        foreach(Transform child in coloredGunsParent.transform)
        {
            coloredGuns.Add(child.gameObject);
        }
        foreach(Transform child in coresParent.transform)
        {
            cores.Add(child.gameObject);
        }
        foreach(Transform child in GameObject.Find("DualShockerPoints").transform)
        {
            movePositions.Add(child);
        }
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        Vector3 vectorToSpawn = (movePositions[4].transform.position - transform.position);
        float distanceToSpawn = vectorToSpawn.magnitude;
        Debug.DrawLine(transform.position, transform.position + vectorToSpawn, Color.green);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToSpawn, 2f * Time.deltaTime);
        if (distanceToSpawn <= 0.01f)
        {
            float distanceBetweenPoints = (movePositions[1].position - movePositions[0].position).magnitude;
            float distanceToDualShocker = (transform.position - movePositions[0].position).magnitude;
            phase1Interpolator = distanceToDualShocker / distanceBetweenPoints;
            moveLeft = Random.Range(0, 2) == 0? true : false;
            isFiring = true;
            Debug.Log("DualShocker stars at interpolation " + phase1Interpolator + " and " + moveLeft);
            ChangeState(DSStates.MOVEPHASE1);
        }
    }

    public void MovePhase1()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            coloredGuns.Clear();
        }

        if(!shootPhase1Started)
        {
            StartCoroutine(shootCorutine);
            shootPhase1Started = true;
        }
        if(moveLeft)
        {
            phase1Interpolator -= (Time.deltaTime / 4);
            if (phase1Interpolator <= 0f)
            {
                moveLeft = false;
            }
        }
        else
        {
            phase1Interpolator += (Time.deltaTime / 4);
            if (phase1Interpolator >= 1f)
            {
                moveLeft = true;
            }
        }
        transform.position = Vector3.Lerp(movePositions[0].position, movePositions[1].position, phase1Interpolator);

        if(coloredGuns.Count == 0)
        {
            Debug.Log("STOPPED");
            StopCoroutine(shootCorutine);
            shootPhase1Started = false;
            randomTransitionPoint = Random.Range(2, 4);
            ChangePhase(DSPhases.PHASE2);
            ChangeState(DSStates.TRANSITION);
        }
    }

    public void Transition()
    {
        Vector3 vectorToTransition = (movePositions[randomTransitionPoint].transform.position - transform.position);
        float distanceToTransition = vectorToTransition.magnitude;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToTransition, 2f * Time.deltaTime);
    }

    private IEnumerator ShootMainGuns(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < mainGuns.Count; i++)
            {
                GameObject newBullet = pooler.NewObject(projectile, mainGuns[i].transform.position, Quaternion.identity);
                Projectile bulletComp = newBullet.GetComponent<Projectile>();

                bulletComp.InitializeBulletVelocity(mainGuns[i].transform.up * 5f);
                bulletComp.AssignColor(Projectile.Color.PURPLE);
            }
        }
    }

    public void ChangePhase(DSPhases newPhase) //Changes phase
    {
        currentPhase = newPhase;
    }

    public void ChangeState(DSStates newState) //Changes state from current state to newState
    {
        currentState = newState;
    }
}

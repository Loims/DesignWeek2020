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

    [SerializeField] private GameObject mainGunsPhase1Parent;
    [SerializeField] private GameObject mainGunsPhase2Parent;
    [SerializeField] private GameObject coloredGunsParent;
    [SerializeField] private GameObject coresParent;
    [SerializeField] private List<GameObject> mainGunsPhase1 = new List<GameObject>();
    [SerializeField] private List<GameObject> mainGunsPhase2 = new List<GameObject>();
    [SerializeField] private List<GameObject> coloredGuns = new List<GameObject>();
    [SerializeField] private List<GameObject> cores = new List<GameObject>();
    [SerializeField] private List<Transform> movePositions = new List<Transform>();

    private GameObject redGun;
    private GameObject blueGun;

    int randomTransitionPoint;
    private float moveInterpolator;
    private bool moveLeft;
    public bool isFiring = false;

    private bool shootPhase1Started = false;
    private bool isFiringRed = false;
    private bool isFiringBlue = false;

    private void OnEnable()
    {
        pooler = ObjectPooler.instance;
        shootCorutine = ShootMainGunsPhase1(1f);

        projectile = Resources.Load<GameObject>("BasicProjectile");

        currentPhase = DSPhases.PHASE1;
        currentState = DSStates.SPAWN;

        mainGunsPhase1Parent = transform.GetChild(0).gameObject;
        mainGunsPhase2Parent = transform.GetChild(1).gameObject;
        coloredGunsParent = transform.GetChild(2).gameObject;
        coresParent = transform.GetChild(3).gameObject;

        foreach(Transform child in mainGunsPhase1Parent.transform)
        {
            mainGunsPhase1.Add(child.gameObject);
        }
        foreach (Transform child in mainGunsPhase2Parent.transform)
        {
            mainGunsPhase2.Add(child.gameObject);
        }
        foreach (Transform child in coloredGunsParent.transform)
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

        redGun = coloredGuns[0];
        blueGun = coloredGuns[1];
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
        coresParent.SetActive(false);
        Vector3 vectorToSpawn = (movePositions[4].transform.position - transform.position);
        float distanceToSpawn = vectorToSpawn.magnitude;
        Debug.DrawLine(transform.position, transform.position + vectorToSpawn, Color.green);

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToSpawn, 2f * Time.deltaTime);
        if (distanceToSpawn <= 0.01f)
        {
            float distanceBetweenPoints = (movePositions[1].position - movePositions[0].position).magnitude;
            float distanceToDualShocker = (transform.position - movePositions[0].position).magnitude;
            moveInterpolator = distanceToDualShocker / distanceBetweenPoints;
            moveLeft = Random.Range(0, 2) == 0? true : false;
            isFiring = true;
            isFiringRed = true;
            isFiringBlue = true;
            Debug.Log("DualShocker stars at interpolation " + moveInterpolator + " and " + moveLeft);
            ChangeState(DSStates.MOVEPHASE1);
        }
    }

    public void MovePhase1()
    {

        if(!shootPhase1Started)
        {
            StartCoroutine(shootCorutine);
            shootPhase1Started = true;
        }
        if(moveLeft)
        {
            moveInterpolator -= (Time.deltaTime / 4);
            if (moveInterpolator <= 0f)
            {
                moveLeft = false;
            }
        }
        else
        {
            moveInterpolator += (Time.deltaTime / 4);
            if (moveInterpolator >= 1f)
            {
                moveLeft = true;
            }
        }
        transform.position = Vector3.Lerp(movePositions[0].position, movePositions[1].position, moveInterpolator);

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

    public void ColoredAttack()
    {
        if(isFiringRed)
        {
            if(coloredGuns.Contains(redGun))
            {
                float randomTime = Random.Range(2f, 3f);
                StartCoroutine(ShootRedGun(randomTime));
                isFiringRed = false;
            }
        }

        if(isFiringBlue)
        {
            if(coloredGuns.Contains(blueGun))
            {
                float randomTime = Random.Range(2f, 3f);
                StartCoroutine(ShootBlueGun(randomTime));
                isFiringBlue = false;
            }
        }
    }

    public void Transition()
    {
        Vector3 vectorToTransition = (movePositions[randomTransitionPoint].transform.position - transform.position);
        float distanceToTransition = vectorToTransition.magnitude;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + vectorToTransition, 2f * Time.deltaTime);

        if(distanceToTransition <= 0.01f)
        {
            float distanceBetweenPoints = (movePositions[3].position - movePositions[2].position).magnitude;
            float distanceToDualShocker = (transform.position - movePositions[2].position).magnitude;
            moveInterpolator = distanceToDualShocker / distanceBetweenPoints;
            moveLeft = Random.Range(0, 2) == 0 ? true : false;
            coresParent.SetActive(true);
            coloredGunsParent.SetActive(false);
            mainGunsPhase1Parent.SetActive(false);
            cores[2].SetActive(false);
            ChangeState(DSStates.MOVEPHASE2);
        }
    }

    public void MovePhase2()
    {
        if(cores.Count == 1)
        {
            cores[0].SetActive(true);
        }

        if (moveLeft)
        {
            moveInterpolator -= (Time.deltaTime/1.5f);
            if (moveInterpolator <= 0f)
            {
                moveLeft = false;
            }
        }
        else
        {
            moveInterpolator += (Time.deltaTime/1.5f);
            if (moveInterpolator >= 1f)
            {
                moveLeft = true;
            }
        }
        transform.position = Vector3.Lerp(movePositions[2].position, movePositions[3].position, moveInterpolator);
    }

    #region Coroutines
    private IEnumerator ShootMainGunsPhase1(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < mainGunsPhase1.Count; i++)
            {
                GameObject newBullet = pooler.NewObject(projectile, mainGunsPhase1[i].transform.position, Quaternion.identity);
                Projectile bulletComp = newBullet.GetComponent<Projectile>();

                bulletComp.InitializeBulletVelocity(mainGunsPhase1[i].transform.up * 5f);
                bulletComp.AssignColor(Projectile.Color.PURPLE);
            }
        }
    }

    private IEnumerator ShootRedGun(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if(redGun == null)
        {
            yield break;
        }
        redGun.GetComponent<ColoredGunScript>().ShootProjectile();
        isFiringRed = true;
        yield break;
    }
    private IEnumerator ShootBlueGun(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (blueGun == null)
        {
            yield break;
        }
        blueGun.GetComponent<ColoredGunScript>().ShootProjectile();
        isFiringBlue = true;
        yield break;
    }
    #endregion

    public void Retaliate(Projectile.Color color)
    {
        foreach(GameObject gun in mainGunsPhase2)
        {
            GameObject newBullet = pooler.NewObject(projectile, gun.transform.position, Quaternion.identity);
            Projectile bulletComp = newBullet.GetComponent<Projectile>();

            bulletComp.InitializeBulletVelocity(gun.transform.up * 5f);
            bulletComp.AssignColor(color);
        }
    }

    public void RemoveFromColoredGuns(GameObject gun)
    {
        if(coloredGuns.Contains(gun))
        {
            coloredGuns.Remove(gun);
        }
    }

    public void RemoveFromCores(GameObject core)
    {
        if(cores.Contains(core))
        {
            Debug.LogWarning("Removed core");
            cores.Remove(core);
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

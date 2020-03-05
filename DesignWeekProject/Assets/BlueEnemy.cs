using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemy : MonoBehaviour
{
    public GameObject ScrapMetal;
    public GameObject Explosion;
    public string type = "blue";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

    }


    private void OnDestroy()
    {
       GameObject explosionClone = Instantiate(Explosion, this.transform.position, Quaternion.identity);
       Destroy(explosionClone, 2f);
       GameObject scrapMetalClone = Instantiate(ScrapMetal, this.transform.position, Quaternion.identity);
        Destroy(scrapMetalClone, 0.7f);
    }

}

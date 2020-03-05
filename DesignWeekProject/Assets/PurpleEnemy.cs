using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleEnemy : MonoBehaviour
{
    [SerializeField]private int score = 100;
    public GameObject Explosion;
    private float health = 100.0f;
    public float damage = 10.0f;
    private bool isMoving = true;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health<=0)
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        health -= damage;
        if (collision.gameObject.name == "RedBullet")
        {
            Player.instance.AddPlayer1Score(score);
        }
        else if (collision.gameObject.name == "BlueBullet")
        {
            Player.instance.AddPlayer2Score(score);
        }
        //mask sprite for damage
    }
    private void OnDestroy()
    {
        Instantiate(Explosion, this.transform.position, this.transform.rotation);
       
    }
}

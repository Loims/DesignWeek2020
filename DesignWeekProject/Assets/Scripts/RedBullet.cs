using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBullet : MonoBehaviour
{
    [SerializeField] private int score = 100;
    public string targetType="red";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<RedEnemy>() )
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Player.instance.AddPlayer1Score(score);
        }
        else if (collision.gameObject.GetComponent<BlueEnemy>() || collision.gameObject.GetComponent<PurpleEnemy>())
        {
            Destroy(this.gameObject);
        }
    }

}

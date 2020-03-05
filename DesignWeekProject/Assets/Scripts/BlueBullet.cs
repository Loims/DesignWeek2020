using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBullet : MonoBehaviour
{
    [SerializeField] private int score = 100;
    public string targetType = "blue";
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BlueEnemy>() )
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            Player.instance.AddPlayer2Score(score);
            Debug.Log(Player.instance.Showplayer2Score());
        }
        else if(collision.gameObject.GetComponent<RedEnemy>() ||collision.gameObject.GetComponent<PurpleEnemy>())
        {
            Destroy(this.gameObject);
        }
    }
}

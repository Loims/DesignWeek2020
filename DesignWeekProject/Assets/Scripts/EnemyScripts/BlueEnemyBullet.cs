using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueEnemyBullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(this.gameObject, 3);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BluePlayercontrol>())
        {
            Player.instance.DecreasePlayer2Health(5);

            Destroy(this.gameObject);

        }
        else if (collision.gameObject.GetComponent<RedPlayercontrol>())
        {
            Destroy(this.gameObject);
        }
    }
}

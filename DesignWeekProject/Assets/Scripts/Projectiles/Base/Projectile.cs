﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public enum Color
    {
        NULL,
        RED,
        BLUE,
        PURPLE
    }

    #region Variables
    [Header("Color Materials (Test Only)")]
    [SerializeField] private Material redMat;
    [SerializeField] private Material blueMat;
    [SerializeField] private Material purpleMat;
    
    [Space]
    [Header("Object Pooler")]
    [SerializeField] private ObjectPooler pooler;

    [Space]
    [Header("Object Information")]

    [SerializeField] private Rigidbody2D rb2d;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private Color projectileColor;
    #endregion

    private void Awake()
    {
        pooler = ObjectPooler.instance;
        rb2d = GetComponent<Rigidbody2D>();

        redMat = Resources.Load<Material>("Red");
        blueMat = Resources.Load<Material>("Blue");
        purpleMat = Resources.Load<Material>("Purple");
    }

    private void OnEnable()
    {
        StartCoroutine(BulletLifeTime(lifetime));
        if(projectileColor == Color.NULL)
        {
            Color newColor = (Color)Random.Range(1, 3);
            Debug.Log(newColor);
            AssignColor(newColor);
        }
    }

    private void OnDisable()
    {
        projectileColor = Color.NULL;
        StopAllCoroutines();
    }

    /*<summary>
     * Method to give bullet velocity. Should be called
     * when bullet is instantiated anfd by the object instantiating it.
     * If no velocity is provided, bullet will have velocity of 0.
     * Useful if bullet has different functionality.
     * </summary>
     */
    public virtual void InitializeBulletVelocity(Vector3 bulletVelocity)
    {
        rb2d.velocity = bulletVelocity;
    }

    /*<summary>
     * Method used to assign color. Method should be called
     * when bullet is instantiated and by the object instantiating 
     * it. If no color is assigned by the parent object, the projectile
     * will be randomly assigned a color by OnEnable
     * </summary>
     */
    public void AssignColor(Color color)
    {
        projectileColor = color;

        Renderer renderer = GetComponent<Renderer>();
        if (color == Color.RED)
        {
            renderer.material = redMat;
        }
        else if (color == Color.BLUE)
        {
            renderer.material = blueMat;
        }
        else if (color == Color.PURPLE)
        {
            renderer.material = purpleMat;
        }
    }

    /*<summary>
     * Lifetime coroutine. When the timer reaches 0,
     * the projectile is returned to the object pool.
     * To change lifetime, edit the lifetime variable
     * </summary>
     */
    public IEnumerator BulletLifeTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        pooler.ReturnToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}

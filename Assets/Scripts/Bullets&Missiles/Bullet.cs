/*****************************************************************************
// File Name : Bullet.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description :  This is a file that controls the player's bullets.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MonoBehaviour
{
    //position of mouse on-screen
    private Vector3 mousePos;
    //where the bullet is aiming to go. essentially a vector2 version of mousePos
    private Vector2 target;
    //game camera
    private Camera mainCam;

    [Tooltip("Rigidbody2D reference to the bullet")]
    [SerializeField] private Rigidbody2D bullet;
    [Tooltip("How fast bullet will travel")]
    [SerializeField] private float speed;

    [Header("References to Player items and Explosion prefabs")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Explosion explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //gets value of items
        playerController = FindObjectOfType<PlayerController>();
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        bullet = GetComponent<Rigidbody2D>();

        //assigns cursor position at clock to mousePos variable
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        //converts mousePos to vector2
        target = new Vector2(mousePos.x, mousePos.y);
    }

    private void Update()
    {
        //stops bullet if game is no longer running
        if (!playerController.gameIsRunning)
        {
            bullet.velocity = Vector2.zero;
        }
    }

    void FixedUpdate()
    {
        BulletExplosion();
    }

    /// <summary>
    /// Launches bullet towards the the target. this also changes the game object's rotation
    /// to face the direction of the target
    /// </summary>
    private void Launch()
    {
        Vector3 direction = mousePos - transform.position;
        Vector3 rotation = transform.position - mousePos;

        bullet.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    /// <summary>
    /// Keeps bullet moving if the bullet has not reached the target variable (cursor click)
    /// when the bullet reaches the target, the bullet objedct is destroyed and an explosion 
    /// is spawned at the same location. 
    /// This is called in FixedUpdate() to monitor movement.
    /// </summary>
    private void BulletExplosion()
    {
        if(bullet.position != target)
        {
            Vector2 explosion = Vector3.MoveTowards(bullet.position, target, speed * Time.deltaTime);
            bullet.MovePosition(explosion);
        }
        else if(bullet.position == target)
        {
            Destroy(gameObject);
            SpawnExplosion(new Vector2(mousePos.x, mousePos.y));
        }

    }
    /// <summary>
    /// This instantiates the explosion gameobject at the point that the bullet object is destroyed.
    /// </summary>
    /// <param name="position">location at which bullet object was destroyed</param>
    public void SpawnExplosion(Vector2 position)
    {
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosionPrefab.Update();
    }

    /// <summary>
    /// Handles collisions of bullet objects with other game objects. if they collide with missiles,
    /// then the bullet is destroyed.
    /// </summary>
    /// <param name="collision">gameobject that has collided with bullet</param>
    private void OnCollisionEnter2D(Collision2D collision)
    { 
        if(collision.transform.tag == "Missile")
        {
            Destroy(gameObject);
        }
    }

}

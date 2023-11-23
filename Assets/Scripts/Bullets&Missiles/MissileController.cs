/*****************************************************************************
// File Name : MissileController.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description :  This is a file that handles the behavior of missile objects.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [Header("Missile references and variables")]
    [Tooltip("Speed of this specific missile object")]
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [Tooltip("Max time before object is destroyed")]
    [SerializeField] private float maxLifetime;

    //refers to a target building
    private GameObject target;
    //converts target building to a vector3 point
    private Vector3 targetVec;

    [Header("References")]
    [SerializeField] private BuildingList buildingList;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Rigidbody2D missile;
    [SerializeField] private Explosion explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        missile.GetComponent<Rigidbody2D>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        //stops object from moving if game is over
        if(!playerController.gameIsRunning)
        {
            missile.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Handles collision of missiles with other game objects. Missiles
    /// are destroyed when in contacts with everything but itself. An explosion occurs
    /// when it his a building.
    /// </summary>
    /// <param name="collision">object that missile collided with</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if(collision.transform.tag == "Constraints")
        {
            Destroy(gameObject);
        }
        if (collision.transform.tag == "Building")
        {
            Destroy(gameObject);
            SpawnExplosion(targetVec);

        }
        if(collision.transform.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Instantiates an explosion where the missile was destroyed
    /// </summary>
    /// <param name="position">position at which missile was destroyed</param>
    public void SpawnExplosion(Vector2 position)
    {
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosionPrefab.Update();
    }

    /// <summary>
    /// Chooses a random building target from the list of 6 buildings
    /// </summary>
    /// <returns>the location of the building</returns>
    private Vector2 GenerateRandomTarget()
    {
        int i = Random.Range(0, buildingList.buildings.Count);
        target = buildingList.buildings[i];
        return target.transform.position;
    }

    /// <summary>
    /// Sets the direction and speed of the missile. This also handles when the
    /// missile is destroyed.
    /// </summary>
    public void SetTrajectory()
    {
        targetVec = GenerateRandomTarget();
        speed = Random.Range(minSpeed, maxSpeed);

        Vector3 direction = targetVec - transform.position;
        
        missile.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        float rot = Mathf.Atan2(transform.position.y, transform.position.x) * Mathf.Rad2Deg;
        missile.transform.rotation = Quaternion.Euler(0, 0, rot -90);

        Destroy(this.gameObject, this.maxLifetime);
    }

}

/*****************************************************************************
// File Name : MissileSpawner.cs
// Author : Isa Luluquisin
// Creation Date : November 21, 2023
//
// Brief Description :  This is a file that handles the behavior of the missile spawner.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    
    [Header("References to Game Objects")]
    [SerializeField] private MissileController missilePrefab;
    [SerializeField] private PlayerController playerInstance;
    [SerializeField] private BoxCollider2D missileSpawner;

    [Header("Range of time in which missiles can spawn")]
    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    //reference to missile coroutine
    public Coroutine MissileRef;

    [Tooltip("Slight variation in trajectory")]
    [SerializeField] private float trajectoryVariance = 10f;

    private void Start()
    {
        missileSpawner = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //references coroutine as long as game is still running
        if (playerInstance.gameIsRunning)
        {
            if (MissileRef == null)
            {
                MissileRef = StartCoroutine(StartMissileSpawns());
            }
        }
        if (!playerInstance.gameIsRunning)
        {
            StopCoroutine(StartMissileSpawns());
        }
    }
    /// <summary>
    /// Calls on Spawn() at variable times. The break between the spawning is randomly
    /// chosen within the range of minSpawnRate and maxSpawnRate.
    /// </summary>
    /// <returns>waits a variable length of time before spawning another missile object </returns>
    public IEnumerator StartMissileSpawns()
    {
        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        yield return new WaitForSeconds(spawnRate);
        Spawn();
        MissileRef = null;
    }

    /// <summary>
    /// Spawns a missile object within the boundaries of the missilespawner game object. 
    /// After instantiating, SetTrajectory() is called to move it towards its target.
    /// </summary>
    private void Spawn()
    {
        Vector2 spawnPoint = GetSpawnArea(missileSpawner.bounds);

        float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        MissileController missile = Instantiate(missilePrefab, spawnPoint, rotation);
        missile.SetTrajectory();
    }
    /// <summary>
    /// Gets a random point inside the missilespawner game object.
    /// </summary>
    /// <param name="bounds"> boundaries of the collider on the missilespawner game object </param>
    /// <returns>a point inside the spawner</returns>
    private Vector2 GetSpawnArea(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }
}

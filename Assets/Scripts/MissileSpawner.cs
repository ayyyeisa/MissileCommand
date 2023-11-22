using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    
    //private Transform[] targets;
    [SerializeField] private MissileController missilePrefab;
    [SerializeField] private PlayerController playerInstance;
    [SerializeField] private BoxCollider2D missileSpawner;

    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    public Coroutine MissileRef;

    [SerializeField] private float trajectoryVariance = 10f;

    private void Start()
    {
        missileSpawner = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
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

    public IEnumerator StartMissileSpawns()
    {
        float spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        yield return new WaitForSeconds(spawnRate);
        Spawn();
        MissileRef = null;
    }

    private void Spawn()
    {
        Vector2 spawnPoint = GetSpawnArea(missileSpawner.bounds);

        float variance = Random.Range(-this.trajectoryVariance, this.trajectoryVariance);
        Quaternion rotation = Quaternion.AngleAxis(variance, Vector3.forward);

        MissileController missile = Instantiate(missilePrefab, spawnPoint, rotation);
        missile.SetTrajectory();
    }

    private Vector2 GetSpawnArea(Bounds bounds)
    {
        return new Vector2(Random.Range(bounds.min.x, bounds.max.x), Random.Range(bounds.min.y, bounds.max.y));
    }
}

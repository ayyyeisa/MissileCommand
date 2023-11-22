using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] buildings;
    private Transform[] targets;
    [SerializeField] private MissileController missilePrefab;
    [SerializeField] private PlayerController playerInstance;

    [SerializeField] private float minSpawnRate;
    [SerializeField] private float maxSpawnRate;

    public Coroutine MissileRef;


    private void Start()
    {
        GetBuildingPositions();
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

    private void GetBuildingPositions()
    {
        for(int i = 0; i < buildings.Length; i++)
        {
            targets[i] = buildings[i].transform;
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
         MissileController missile = Instantiate(missilePrefab, this.transform.position, Quaternion.EulerAngles(0, 0, missileDirection);
         Instantiate(missile, this.transform.position, rotation);

     //   missile.SetTrajectory(//towards target)

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameManager))]
public class JumperSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject jumperPrefab;

    float lastSpawnTime;

    [Range(0,5)]
    public float spawnDelay = 3.0f;
    [Range(0,2)]
    public float deltaRandomSpawn = 0.5f;

    private float randomSpawnDelay;
    private bool stop = false;
    private bool starting = true;

    private List<GameObject> jumpers = new List<GameObject>();

    void Start()
    {
        if (jumperPrefab == null)
            return;

        randomSpawnDelay = spawnDelay;
        SpawnJumper();
    }

    private void Update()
    {
        if(starting && !stop && Time.time > lastSpawnTime + randomSpawnDelay)
        {
            SpawnJumper();
        }
    }

    private void SpawnJumper()
    {
        
        {
            lastSpawnTime = Time.time;
            randomSpawnDelay = Random.Range(spawnDelay - deltaRandomSpawn, spawnDelay + deltaRandomSpawn);
            GameObject jumper = Instantiate(jumperPrefab);

            jumpers.Add(jumper);

            JumperController jumperController = jumper.GetComponentInChildren<JumperController>();

            jumperController.jumperSpawner = this;
        }
    }

    public void DestroyJumper(GameObject jumper)
    {
        jumpers.Remove(jumper);

        Destroy(jumper);
    }


    public void Stop()
    {
        stop = true;

        for (int i = jumpers.Count - 1; i >= 0; i--)
        {
            DestroyJumper(jumpers[i]);
        }
    }

    public void Starting()
    {
        starting = false;
    }
}

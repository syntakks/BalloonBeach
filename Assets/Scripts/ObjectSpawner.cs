using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject player;
    public GameObject[] trianglePrefabs;
    private Vector3 spawnPosition; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, spawnPosition); 
        if (distanceToHorizon < 120)
        {
            SpawnObstacle();
        }
    }

    private void SpawnObstacle()
    {
        spawnPosition = new Vector3(0, 0, spawnPosition.z + 30);
        GameObject obstacle = Instantiate(trianglePrefabs[Random.Range(0, trianglePrefabs.Length)], spawnPosition, Quaternion.identity);
    }
}

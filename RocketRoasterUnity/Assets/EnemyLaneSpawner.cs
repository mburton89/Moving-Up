using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaneSpawner : MonoBehaviour
{
    public int numberOfLanesToSpawn;
    public float minDistanceBetweenLanes;
    public float maxDistanceBetweenLanes;
    float currentY;

    public GameObject lanePrefab;

    void Start()
    {
        for (int i = 0; i < numberOfLanesToSpawn; i++)
        {
            currentY += Random.Range(minDistanceBetweenLanes, maxDistanceBetweenLanes);
            Instantiate(lanePrefab, new Vector3(0, currentY, 0), transform.rotation, null);
        }
    }
}

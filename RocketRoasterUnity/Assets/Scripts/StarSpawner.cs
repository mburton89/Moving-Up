using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _starPrefab;
    [SerializeField] private int _numberOfStarsToSpawn;
    [SerializeField] private int _maxX;
    [SerializeField] private int _maxY;
    [SerializeField] private int _maxZ;
    [SerializeField] private int _minZ;

    void Start()
    {
        for (int i = 0; i < _numberOfStarsToSpawn; i++)
        {
            float randX = Random.Range(-_maxX, _maxX);
            float randY = Random.Range(-_maxY, _maxY);
            float randZ = Random.Range(_minZ, _maxZ);
            Vector3 spawnPosition = new Vector3(randX, randY, randZ);
            Instantiate(_starPrefab, spawnPosition, transform.rotation, transform);
        }
    }
}

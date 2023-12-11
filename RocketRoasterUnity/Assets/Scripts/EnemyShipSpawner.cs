using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemyPrefabs;
    [SerializeField] float maxX;
    [SerializeField] float ySpawnPos;

    [SerializeField] float _distanceBetweenSpawns;

    private float _prevY;
    private float _currentY;

    public Transform parent;

    void Start()
    {
        _currentY = transform.position.y;
        _prevY = _currentY;
    }

    void Update()
    {
        _currentY = transform.position.y;

        if (_currentY - _prevY >= _distanceBetweenSpawns)
        {
            SpawnEnemy();
            _prevY = _currentY;
        }
    }

    void SpawnEnemy()
    {
        int rand = Random.Range(0, 2);
        Vector3 spawnPos;

        if (rand == 0)
        {
            spawnPos = new Vector3(-maxX, transform.position.y, 0);
            GameObject prefabToSpawn = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
            GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, transform.rotation, parent);
            newEnemy.GetComponent<Enemy>().Init(1, 50);
        }
        else
        {
            spawnPos = new Vector3(maxX, transform.position.y, 0);
            GameObject prefabToSpawn = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
            GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, transform.rotation, parent);
            newEnemy.GetComponent<Enemy>().Init(-1, 50);
        }
    }
}

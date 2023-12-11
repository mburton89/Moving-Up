using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLane : MonoBehaviour
{
    [SerializeField] List<GameObject> _enemyPrefabs;
    Vector3 spawnPos;
    public float maxX;

    public Transform parent;

    [HideInInspector] public int xDirection;

    public float minSpeed;
    public float maxSpeed;

    public float minSeconds;
    public float maxSeconds;

    private void Start()
    {
        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            spawnPos = new Vector3(-maxX, transform.position.y, 0);
            xDirection = 1;
            SpawnEnemy(new Vector3(spawnPos.x + 0, spawnPos.y, spawnPos.z));
            SpawnEnemy(new Vector3(spawnPos.x + 6, spawnPos.y, spawnPos.z));
            SpawnEnemy(new Vector3(spawnPos.x + 12, spawnPos.y, spawnPos.z));
        }
        else
        {
            spawnPos = new Vector3(maxX, transform.position.y, 0);
            xDirection = -1;
            SpawnEnemy(new Vector3(spawnPos.x + 0, spawnPos.y, spawnPos.z));
            SpawnEnemy(new Vector3(spawnPos.x - 6, spawnPos.y, spawnPos.z));
            SpawnEnemy(new Vector3(spawnPos.x - 12, spawnPos.y, spawnPos.z));
        }

        StartCoroutine(SpawnCo());
    }

    void SpawnEnemy()
    {
        GameObject prefabToSpawn = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        GameObject newEnemy = Instantiate(prefabToSpawn, spawnPos, transform.rotation, parent);
        newEnemy.GetComponent<Enemy>().Init(xDirection, Random.Range(minSpeed, maxSpeed));
    }

    void SpawnEnemy(Vector3 newPosition)
    {
        GameObject prefabToSpawn = _enemyPrefabs[Random.Range(0, _enemyPrefabs.Count)];
        GameObject newEnemy = Instantiate(prefabToSpawn, newPosition, transform.rotation, parent);
        newEnemy.GetComponent<Enemy>().Init(xDirection, Random.Range(minSpeed, maxSpeed));
    }

    private IEnumerator SpawnCo()
    {
        yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        SpawnEnemy();
        StartCoroutine(SpawnCo());
    }
}

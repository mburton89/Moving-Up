using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipSpawner : MonoBehaviour
{
    public static EnemyShipSpawner Instance;

    public Transform SpawnPoint;

    [HideInInspector] public int startingNumberOfShips;
    [HideInInspector] public int currentWave;


    private void Awake()
    {
        Instance = this;
        currentWave = 1;
    }

  
    void SpawnWaveOfShips()
    {

    }
}

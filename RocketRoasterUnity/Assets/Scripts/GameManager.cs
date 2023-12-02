using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void HandlePlayerDestroyed()
    {
        if (EnemyShipSpawner.Instance.currentWave > PlayerPrefs.GetInt("HighestWave"))
        {
            PlayerPrefs.SetInt("HighestWave", EnemyShipSpawner.Instance.currentWave);
        }

        StartCoroutine(EndSessionCo());
    }

    private IEnumerator EndSessionCo()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}

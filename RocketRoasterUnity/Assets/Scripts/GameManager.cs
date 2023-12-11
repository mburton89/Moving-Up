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
        Application.targetFrameRate = 60;
    }

    public void HandlePlayerDestroyed()
    {
        StartCoroutine(EndSessionCo());
    }

    private IEnumerator EndSessionCo()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
    }
}

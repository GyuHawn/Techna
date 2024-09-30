using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GameOverReady());
    }

    IEnumerator GameOverReady()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Main");
    }
}

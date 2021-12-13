using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private GameObject _menuPlayer;

    private void Start()
    {
        _asteroidSpawner._inMenu = true;
    }

    public void Play()
    {
        StartCoroutine(P());
    }

    public void Exit()
    {
        StartCoroutine(E());
    }

    private IEnumerator P()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Game");
    }

    private IEnumerator E()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }


}

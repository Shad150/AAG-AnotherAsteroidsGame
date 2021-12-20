using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AsteroidSpawner _asteroidSpawner;
    [SerializeField] private EnemyBehaviour _eb;
    [SerializeField] private GameObject _menuPlayer;
    [SerializeField] private GameObject _menuControls;
    [SerializeField] private Text _controlsText;
    private bool _controlsActive;

    private void Start()
    {
        _menuControls.SetActive(false);
        _asteroidSpawner._inMenu = true;
        _eb._inMenu = true;
        StartCoroutine(TextControls());
    }

    public void Play()
    {
        StartCoroutine(P());
    }

    public void Exit()
    {
        StartCoroutine(E());
    }

    public void Controls()
    {
        if (!_controlsActive)
        {
            _menuControls.SetActive(true);
            _controlsActive = true;
        }
        else
        {
            _menuControls.SetActive(false);
            _controlsActive = false;
        }
    }

    private IEnumerator P()
    {
        yield return new WaitForSeconds(0.5f);
        _asteroidSpawner._inMenu = false;
        _eb._inMenu = false;
        SceneManager.LoadScene("Game");
    }

    private IEnumerator E()
    {
        yield return new WaitForSeconds(0.5f);
        Application.Quit();
    }

    private IEnumerator TextControls()
    {
        yield return new WaitForSeconds(1f);
        _controlsText.enabled = false;
        yield return new WaitForSeconds(1f);
        _controlsText.enabled = true;

        StartCoroutine(TextControls());
    }

}

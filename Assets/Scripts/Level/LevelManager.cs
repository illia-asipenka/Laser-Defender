using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    Scene myScene;
    [SerializeField] private int _deathDelay = 1;


    private void Start()
    {
        myScene = SceneManager.GetActiveScene();
    }

    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("Game");
        FindObjectOfType<GameSession>().ResetGame();
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad());
    }

    private IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(_deathDelay);
        SceneManager.LoadScene("Win");
    }

    public void QuitGame ()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

   



}

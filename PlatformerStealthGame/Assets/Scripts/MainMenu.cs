using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("game quit");
    }
    public void ReplayGame()
    {
        SceneManager.LoadScene("Boss Scene");
    }
    public void TrueEndCredit()
    {
        SceneManager.LoadScene("credit");
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Main-menu");
    }
    public void EndlessMode()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 16);
    }
}
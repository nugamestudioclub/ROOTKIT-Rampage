using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    public static void ToMain()
    {
        SceneManager.LoadScene("Main", LoadSceneMode.Single);
    }

    public static void ToTitle()
    {
        SceneManager.LoadScene("Title", LoadSceneMode.Single);
    }

    public static void ToWin()
    {
        SceneManager.LoadScene("Win", LoadSceneMode.Single);
    }

    public static void ToGameOver()
    {
        SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
    }

    public static void ToCredits()
    {
        SceneManager.LoadScene("Credits", LoadSceneMode.Single);
    }

    /*
     * Quits the game
     */
    public static void QuitGame()
    {
        Application.Quit();
    }
}

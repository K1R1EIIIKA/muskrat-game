using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void Menu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

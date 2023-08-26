using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;

    public static bool IsDead;
    public static bool IsPaused;

    private void Awake()
    {
        Restart();
    }

    void Update()
    {
        if (Player.Health <= 0)
            Death();

        if (Input.GetKeyDown(KeyCode.Escape) && !IsDead)
        {
            if (!IsPaused)
                Pause();
            else
                Unpause();
        }
    }

    private void Death()
    {
        deathScreen.SetActive(true);
        IsDead = true;
        Time.timeScale = 0;
    }

    private void Pause()
    {
        pauseScreen.SetActive(true);
        Time.timeScale = 0;
        IsPaused = true;
    }

    private void Unpause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    private void Restart()
    {
        Player.Health = 5;
        Player.Points = 0;
        Time.timeScale = 1;
        IsPaused = false;
        IsDead = false;
    }
}

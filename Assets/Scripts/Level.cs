using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject deathScreen;
    
    [SerializeField] private GameObject winScreen;
    [SerializeField] private TextMeshProUGUI pointsText;

    [SerializeField] private TextMeshProUGUI healthPoints;
    [SerializeField] private TextMeshProUGUI pointsPoints;

    public static bool IsDead;
    public static bool IsPaused;
    public static bool IsWin;

    private void Awake()
    {
        Restart();
    }

    void Update()
    {
        healthPoints.text = "Health: " + Player.Health;
        pointsPoints.text = "Points: " + Player.Points;
        
        if (Player.Health <= 0)
            Death();
        
        if (IsWin)
            Win();

        if (Input.GetKeyDown(KeyCode.Escape) && !IsDead)
        {
            if (!IsPaused)
                Pause();
            else
                Unpause();
        }
    }

    private void Win()
    {
        winScreen.SetActive(true);
        pointsText.text = "You collected " + Player.Points + " points!!";
        
        Time.timeScale = 0;
        IsPaused = true;
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

    public void Unpause()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        IsPaused = false;
    }

    private void Restart()
    {
        Player.Health = 5;
        Player.Points = 0;
        PlayerMovement.CanMove = true;
        Time.timeScale = 1;
        IsPaused = false;
        IsDead = false;
        IsWin = false;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    [SerializeField] private GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) PausePressed();
    }

    public void PausePressed()
    {
        if (isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            canvas.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            isPaused = true;
            canvas.SetActive(true);
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ReportBug()
    {
        
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }
}

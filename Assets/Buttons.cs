using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject PauseMenuCanvas;
    public static bool Paused = false; // Default to not paused
    public GameObject Player;
    public GameObject gameOverCanvas;

    private void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseMenu();
        }
        if (Player == null && gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(true);

        }
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Should Quit the game");

    }
    public void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Menu()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1f;
    }
    private void TogglePauseMenu()
    {
        if (Paused)
        {
            PauseMenuCanvas.SetActive(false);
            Player.SetActive(true);
            Time.timeScale = 1f;
            Paused = false;
        }
        else
        {
            PauseMenuCanvas.SetActive(true);
            Player.SetActive(false); // Optional: If you want to hide the player during pause
            Time.timeScale = 0f;
            Paused = true;
        }
    }
    public void Play()
    {
        PauseMenuCanvas.SetActive(false);
        Player.SetActive(true);
        Time.timeScale = 1f;
        Paused = false;
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

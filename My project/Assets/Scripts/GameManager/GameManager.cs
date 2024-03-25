using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;

    // Property to get the paused state
    public bool IsPaused
    {
        get { return isPaused; }
    }

    // Method to pause the game
    public void PauseGame()
    {
        Time.timeScale = 0f; // Set time scale to 0 to pause the game
        isPaused = true;
    }

    // Method to unpause the game
    public void UnpauseGame()
    {
        Time.timeScale = 1f; // Set time scale back to 1 to unpause the game
        isPaused = false;
    }
}

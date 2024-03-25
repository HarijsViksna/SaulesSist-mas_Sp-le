using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouse : MonoBehaviour
{
    // Reference to GameManager
    public GameManager gameManager;

    void Update()
    {
        // Check if the game is paused
        if (!gameManager.IsPaused)
        {
            // If the game is not paused, hide and lock the cursor
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            // If the game is paused, unlock and show the cursor
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}

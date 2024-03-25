using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAnswers : MonoBehaviour
{
    public GameObject answerPanel;
    public GameObject previousPanel; // Reference to the previously visible panel
    public GameManager gameManager; // Reference to the GameManager

    public void OpenAnswer()
    {
        // Pause the game
        gameManager.PauseGame();

        // Hide the previously visible panel if it's active
        if (previousPanel != null)
        {
            previousPanel.SetActive(false);
        }

        // Show the quiz panel
        answerPanel.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenQuizPanel : MonoBehaviour
{
    public GameObject quizPanel;
    public GameObject previousPanel; // Reference to the previously visible panel
    public GameManager gameManager; // Reference to the GameManager

    public void OpenQuiz()
    {
        // Pause the game
        gameManager.PauseGame();

        // Hide the previously visible panel if it's active
        if (previousPanel != null)
        {
            previousPanel.SetActive(false);
        }

        // Show the quiz panel
        quizPanel.SetActive(true);
    }
}

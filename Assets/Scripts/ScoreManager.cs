using System.Collections;
using System.Collections.Generic;
using TMPro; // Required for TextMeshPro
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0; // Initialize score
    public TextMeshProUGUI scoreText; // Reference to the UI text

    // Method to increment score
    public void IncrementScore()
    {
        score++;
        UpdateScoreText();
        Debug.Log("Score updated to: " + score);
    }

    // Method to update the score display
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score.ToString(); // Update score text
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText(); // Initialize the score display
    }
}

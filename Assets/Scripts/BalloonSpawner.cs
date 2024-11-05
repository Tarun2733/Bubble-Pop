using System.Collections;
using UnityEngine;
using TMPro; // Required if using TextMeshProUGUI
using UnityEngine.SceneManagement; // Needed to reload the scene
using UnityEngine.UI; // Needed to reference the button

public class BalloonSpawner : MonoBehaviour
{
    public GameObject balloonPrefab;
    public GameObject explosionEffectPrefab;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton; // Reference to Restart button
    public float spawnInterval = 1f;
    public int score;

    private bool isGameOver = false;
    private bool isGameStarted = false; // Track if the game has started

    void Start()
    {
        if (scoreText == null)
            scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();

        if (gameOverText == null)
            gameOverText = GameObject.Find("GameOverText").GetComponent<TextMeshProUGUI>();

        if (restartButton == null)
            restartButton = GameObject.Find("RestartButton").GetComponent<Button>();

        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false); // Hide restart button initially

        restartButton.onClick.AddListener(RestartGame); // Assign RestartGame method to the button's onClick
    }

    public void StartSpawning() // Method to start balloon spawning
    {
        isGameStarted = true; // Set the game as started
        score = 0; // Reset the score
        scoreText.text = "Score: " + score.ToString(); // Update score display
        gameOverText.gameObject.SetActive(false); // Hide game over text
        restartButton.gameObject.SetActive(false); // Hide restart button
        StartCoroutine(SpawnBalloons()); // Start spawning balloons
    }

    IEnumerator SpawnBalloons()
    {
        while (!isGameOver && isGameStarted)
        {
            float minX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float maxX = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
            float spawnY = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y + 1;

            Vector3 spawnPosition = new Vector3(Random.Range(minX, maxX), spawnY, 0);

            GameObject balloon = Instantiate(balloonPrefab, spawnPosition, Quaternion.identity);
            balloon.GetComponent<Balloon>().explosionEffectPrefab = explosionEffectPrefab;

            yield return new WaitForSeconds(spawnInterval);

            if (spawnInterval > 0.5f)
                spawnInterval -= 0.01f;
        }
    }

    public void IncrementScore()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
    }

    public void GameOver()
    {
        isGameOver = true;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true); // Show the restart button
        Time.timeScale = 0; // Pause the game
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Resume the game before restarting
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}

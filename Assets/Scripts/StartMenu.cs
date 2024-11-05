using UnityEngine;
using UnityEngine.UI; // Needed to reference the button
using TMPro; // Required if using TextMeshProUGUI

public class StartMenu : MonoBehaviour
{
    public GameObject startMenu; // Reference to the Start Menu UI
    public BalloonSpawner balloonSpawner; // Reference to the BalloonSpawner
    public AudioSource backgroundMusic; // Reference to the AudioSource

    void Start()
    {
        // Ensure the start menu is active at the start
        startMenu.SetActive(true);
        Time.timeScale = 0; // Pause the game
        backgroundMusic.Play(); // Play background music
    }

    public void StartGame()
    {
        startMenu.SetActive(false); // Hide the start menu
        Time.timeScale = 1; // Resume the game
        balloonSpawner.StartSpawning(); // Call the method to start spawning balloons
    }
}

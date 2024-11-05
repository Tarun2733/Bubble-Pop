using UnityEngine;

public class Balloon : MonoBehaviour
{
    public GameObject explosionEffectPrefab; // Reference to the particle explosion prefab
    private AudioSource explosionAudioSource; // Reference to the AudioSource for explosion sound
    public float speed = 2f; // Speed of the balloon
    private Vector2 direction; // Direction of movement
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private BalloonSpawner balloonSpawner; // Reference to the BalloonSpawner

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
        balloonSpawner = FindObjectOfType<BalloonSpawner>(); // Find the BalloonSpawner in the scene
        SetRandomColor();
        direction = new Vector2(Random.Range(-1f, 1f), -1f).normalized;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (viewportPosition.x < 0 || viewportPosition.x > 1)
        {
            direction.x *= -1;
        }

        if (viewportPosition.y < 0)
        {
            balloonSpawner.GameOver();
            Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        balloonSpawner.IncrementScore(); // Increment score on click
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject explosion = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            explosionAudioSource = explosion.GetComponent<AudioSource>();
            if (explosionAudioSource != null)
            {
                explosionAudioSource.Play();
            }
            Destroy(explosion, 1f); // Clean up explosion after 1 second
        }
    }

    private void SetRandomColor()
    {
        Color[] colors = { Color.red, Color.black, Color.blue, Color.green };
        spriteRenderer.color = colors[Random.Range(0, colors.Length)];
    }
}

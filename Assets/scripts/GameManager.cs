using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public Transform[] spawnPoints;  // Array of spawn points
    int score = 0;

    public string playerName = "Player";  // You can link this to an input field later

    public TextMeshProUGUI scoreText;
    public GameObject PlayButton;
    public GameObject Player;

    private bool gameActive = false;

    // Start is called before the first frame update
    void Start()
    {

        //playerName = PlayerPrefs.GetString("PlayerName", "Player");
        // Ensure the player is inactive at the start
        Player.SetActive(false);
        PlayButton.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        // If the game is not active, stop spawning obstacles
        if (!gameActive)
        {
            StopCoroutine("SpawnObstacles");
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (gameActive)
        {
            float waitTime = Random.Range(0.5f, 3f);
            yield return new WaitForSeconds(waitTime);

            // Randomly pick a spawn point from the array of spawn points
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the obstacle at the selected spawn point
            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
        }
    }

    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    // This function will be called to start the game
    public void GameStart()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameActive = true;

        playerName = PlayerPrefs.GetString("PlayerName", "Player");

        Player.SetActive(true);
        PlayButton.SetActive(false);

        StartCoroutine("SpawnObstacles"); // Start spawning obstacles after clicking Play button
        InvokeRepeating("ScoreUp", 2f, 1f); // Start scoring when the game starts
    }

    // This function will be called when the player collides with an obstacle
    public void GameOver()
    {
        
        gameActive = false;
        CancelInvoke("ScoreUp");

        Debug.Log("GameOver() called");

        // Deactivate player movement and show UI
        Player.SetActive(false);
        PlayButton.SetActive(true);

        DatabaseManager.Instance.SaveScore(playerName, score);

    }

    // Handle collision with obstacles (you must add a Collider to both the player and obstacles)
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) // Ensure the obstacle has the "Obstacle" tag
        {
            GameOver();  // Call GameOver when the player collides with an obstacle
        }
    }

}

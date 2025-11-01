using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject obstacle;
    public Transform[] spawnPoints;  
    int score = 0;

    public string playerName = "Player";  

    public TextMeshProUGUI scoreText;
    public GameObject PlayButton;
    public GameObject Player;

    private bool gameActive = false;

    
    void Start()
    {

        
       
        Player.SetActive(false);
        PlayButton.SetActive(true);

    }

   
    void Update()
    {
        
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

            
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            
            Instantiate(obstacle, spawnPoint.position, Quaternion.identity);
        }
    }

    void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }

    
    public void GameStart()
    {
        score = 0;
        scoreText.text = score.ToString();
        gameActive = true;

        playerName = PlayerPrefs.GetString("PlayerName", "Player");

        Player.SetActive(true);
        PlayButton.SetActive(false);

        StartCoroutine("SpawnObstacles"); 
        InvokeRepeating("ScoreUp", 2f, 1f); 
    }

    
    public void GameOver()
    {
        
        gameActive = false;
        CancelInvoke("ScoreUp");

       
        Player.SetActive(false);
        PlayButton.SetActive(true);

        DatabaseManager.Instance.SaveScore(playerName, score);

    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")) 
        {
            GameOver();  
        }
    }

}

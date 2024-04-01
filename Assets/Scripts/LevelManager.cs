using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static bool isGameOver = false;
    public string sameLevel;
    public string nextLevel;
    public Text scoreText;
    public Text gameStateText; 
    public AudioClip gameOverSFX;
    public AudioClip gameWonSFX;
    public static int scoreToBeat = 10;

    GameObject player;
    PlayerHealth playerHealth;


    
    
    // Start is called before the first frame update
    void Start() {
        isGameOver = false;
        EnemyHit.score = 0;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        setScoreText();
    }

    // Update is called once per frame
    void Update() {
        if (!isGameOver) {
            if (playerHealth.currentHealth <= 0) {
                LevelLost();
            }
        }
        setScoreText();
    }

    public void LevelLost() {
        isGameOver = true;
        gameStateText.text = "Game Over";
        gameStateText.gameObject.SetActive(true);

        Camera.main.GetComponent<AudioSource>().pitch = 1;
        AudioSource.PlayClipAtPoint(gameOverSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(sameLevel)) {
            Invoke("LoadSameLevel", 2);
        }
        
    }

    public void LevelBeat() {
        isGameOver = true;
        gameStateText.text = "Game Won";
        gameStateText.gameObject.SetActive(true);
        
        Camera.main.GetComponent<AudioSource>().pitch = 2;
        AudioSource.PlayClipAtPoint(gameWonSFX, Camera.main.transform.position);

        if (!string.IsNullOrEmpty(nextLevel)) {
            Invoke("LoadNextLevel", 2);
        }

    }

    void LoadSameLevel() {
        SceneManager.LoadScene(sameLevel);
    }
    
    void LoadNextLevel() {
        SceneManager.LoadScene(nextLevel);
    }

   
    private void setScoreText() {
        scoreText.text = "Score: " + EnemyHit.score.ToString();
    }
}

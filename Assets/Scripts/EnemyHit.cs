using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    public GameObject enemyKilled;
    
    public static int score = 0;
    public static int scoreValue = 1;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Projectile")) {
            score += scoreValue;
            destroyEnemy();
            if (score >= LevelManager.scoreToBeat) {
                FindObjectOfType<LevelManager>().LevelBeat();
            }
        }
    }

    void destroyEnemy() {
        Instantiate(enemyKilled, transform.position, transform.rotation);
        gameObject.SetActive(false);
        
        Instantiate(enemyKilled, transform.position, transform.rotation);

        Destroy(gameObject, 0.5f);
    }
}
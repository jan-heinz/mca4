using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 10f;
    public float minDistance = 2f;
    public int damageAmount = 2;


    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void Update() {
        if (!LevelManager.isGameOver) {
            float step = moveSpeed * Time.deltaTime;
            float distance = Vector3.Distance(transform.position, player.position);

            if (distance > minDistance) {
                transform.LookAt(player);
                transform.position = Vector3.MoveTowards(transform.position, player.position, step);
            }
        }
       
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            var playerHealth = other.GetComponent<PlayerHealth>();
            playerHealth.takeDamage(damageAmount);
        }
    }
}
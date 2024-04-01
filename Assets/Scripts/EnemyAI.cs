using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
    public enum FSMStates {
        Patrol,
        Chase,
        Attack
    }

    public GameObject player;
    public FSMStates currentState;
    public float enemyChaseSpeed = 5;
    public float enemyPatrolSpeed = 2.5f;
    public float chaseDistance = 10;
    public float attackDistance = 2;
    public float fov = 45f;
    public Transform bullseye;

    float distanceToPlayer;
    Vector3 nextDestination;
    int currentDestinationIndex = 0;
    NavMeshAgent agent;
    GameObject[] wanderPoints;



    // Start is called before the first frame update
    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        wanderPoints = GameObject.FindGameObjectsWithTag("WanderPoint");

        agent = GetComponent<NavMeshAgent>();
        init();
    }

    void init() {
        currentState = FSMStates.Patrol;
        findNextPoint();
    }

    void findNextPoint() {
        nextDestination = wanderPoints[currentDestinationIndex].transform.position;
        // 0->1->2->0
        currentDestinationIndex = (currentDestinationIndex + 1) % wanderPoints.Length;
        agent.SetDestination(nextDestination);

    }

    // Update is called once per frame
    void Update() {
        distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        switch (currentState) {
            case FSMStates.Patrol:
                UpdatePatrolState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
        }
    }

    void UpdatePatrolState() {
        agent.stoppingDistance = 0;
        agent.speed = enemyPatrolSpeed;

        if (Vector3.Distance(transform.position, nextDestination) < 2) {
            findNextPoint();
        }
        else if (isPlayerInFOV()) {
            currentState = FSMStates.Chase;
        }

        faceTarget(nextDestination);
        agent.SetDestination(nextDestination);
    }

    void UpdateChaseState() {
        nextDestination = player.transform.position;
        agent.stoppingDistance = attackDistance;
        agent.speed = enemyChaseSpeed;


        if (distanceToPlayer <= attackDistance) {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > chaseDistance) {
            findNextPoint();
            currentState = FSMStates.Patrol;
        }

        faceTarget(nextDestination);
    }

    void UpdateAttackState() {
        nextDestination = player.transform.position;

        if (distanceToPlayer <= attackDistance) {
            currentState = FSMStates.Attack;
        }
        else if (distanceToPlayer > attackDistance && distanceToPlayer <= chaseDistance) {
            currentState = FSMStates.Chase;
        }
        else if (distanceToPlayer > chaseDistance) {
            currentState = FSMStates.Patrol;
        }

        faceTarget(nextDestination);
    }

    void faceTarget(Vector3 target) {
        Vector3 directionToTarget = target - transform.position;
        directionToTarget.y = 0;

        if (directionToTarget == Vector3.zero) return; 

        directionToTarget.Normalize(); 
        Quaternion lookRotation = Quaternion.LookRotation(directionToTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10 * Time.deltaTime);
    }

    
    bool isPlayerInFOV() {
        Vector3 directionToPlayer = player.transform.position - bullseye.position;
        RaycastHit hit;

        if (Vector3.Angle(directionToPlayer, bullseye.forward) <= fov) {
            if (Physics.Raycast(bullseye.position, directionToPlayer, out hit, chaseDistance)) {
                if (hit.collider.CompareTag("Player")) {
                    return true;
                }

                return false;
            }

            return false;
        }

        return false;
    }
}

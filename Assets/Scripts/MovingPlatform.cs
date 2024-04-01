using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform[] points;
    public float speed = 3f;
    public Collider platformTrigger;
    
    int nextPointIndex = 0;
    float reachThreshold = 0.1f;
    Transform player;

    void Start() {
        if (points.Length > 0) {
            transform.position = points[0].position;
        }
    }

    void Update() {
        if (points.Length < 2) 
            return;

        movePlatform();
    }

    void movePlatform() {
        Transform targetPoint = points[nextPointIndex];
        transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPoint.position) < reachThreshold) {
            nextPointIndex = (nextPointIndex + 1) % points.Length;
        }
    }
}
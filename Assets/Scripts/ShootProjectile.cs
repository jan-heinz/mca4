using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject propProjectilePrefab;
    public float projectileSpeed = 100f;
    public Image reticleImage;
    public Color reticleEnemyColor;
    public Color reticlePropColor;
    
  

    Color originalReticleColor;
    GameObject currentProjectilePrefab;
    
    // Start is called before the first frame update
    void Start() {
        originalReticleColor = reticleImage.color;
        currentProjectilePrefab = projectilePrefab;
    }

    // Update is called once per frame
    void Update() {
        if (!LevelManager.isGameOver) {
            if (Input.GetButtonDown("Fire1")) {
                GameObject projectile =
                    Instantiate(currentProjectilePrefab, transform.position + transform.forward,
                        transform.rotation) as GameObject;

                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

                projectile.transform.SetParent(GameObject.FindGameObjectWithTag("ProjectileParent").transform);
            }
        }
    }
    
    void FixedUpdate() {
        reticleEffect();
    }

    void reticleEffect() {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity)) {
            if (hit.collider.CompareTag("Enemy")) {
                currentProjectilePrefab = projectilePrefab;
                reticleImage.color = reticleEnemyColor;
            } 
            else if (hit.collider.CompareTag("Prop")) {
                currentProjectilePrefab = propProjectilePrefab;
                reticleImage.color = reticlePropColor;
            }
        } else {
            reticleImage.color = originalReticleColor;
        }
    }
}
    



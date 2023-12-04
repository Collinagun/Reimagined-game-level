using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject player; // This will be assigned in Start()

    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        // Check if the bullet collided with the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            // Destroy the bullet after 2 seconds
            Destroy(gameObject, 2f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with the AI
        if (collision.gameObject.CompareTag("Ai"))
        {
            // Destroy the AI
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
    }
}
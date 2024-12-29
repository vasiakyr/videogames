using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectile : MonoBehaviour
{
    public float lifeTime = 5f; // διάρκεια  του fire
    public int damage = 1; // ζημιά στον παίκτη

    void Start()
    {
        // Καταστρέφει το fire μετά από συγκεκριμένο χρόνο
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Η φωτιά χτύπησε τον παίκτη!");
            
            // Βρίσκουμε το script του παίκτη και καλούμε τη μέθοδο TakeDamage
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Εδώ περνάμε τη ζημιά
            }

            // Καταστρέφουμε τη φωτιά
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Wall"))
        {
            // Καταστρέφουμε τη φωτιά αν χτυπήσει τοίχο ή άλλο εμπόδιο
            Destroy(gameObject);
        }
    }
}

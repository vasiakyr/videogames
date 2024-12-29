using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifeTime = 5f; // Διάρκεια ζωής του projectile
    public int damage = 1; // Ζημιά που προκαλεί

    void Start()
    {
        // Καταστρέφει το projectile μετά από συγκεκριμένο χρόνο
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Projectile hit the player!");
            PlatformerPlayer player = collision.GetComponent<PlatformerPlayer>();
            if (player != null)
            {
                player.SendMessage("TakeDamage", damage);
            }
            Destroy(gameObject); // Καταστρέφει το projectile
        }
        else if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject); // Καταστρέφει το projectile αν χτυπήσει εμπόδιο
        }
    }
}

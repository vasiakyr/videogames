using UnityEngine;

public class Spike : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ελέγχουμε αν το αντικείμενο που συγκρούεται είναι ο παίκτης
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ο Παίκτης έχασε ζωή! Συγκρούστηκε με τον Εχθρό.");

            // Αναζητούμε τον Player
            PlatformerPlayer player = collision.gameObject.GetComponent<PlatformerPlayer>();

            if (player != null)
            {
                // Χάνουμε μία ζωή από τον παίκτη
                player.TakeDamage();

                // Αν ο παίκτης έχει 0 ζωές, κάνουμε restart του παιχνιδιού
                if (player.GetHealth() <= 0)
                {
                    // Επαναφορά του παίκτη στην αρχική του θέση
                    player.transform.position = player.StartingPosition(); // Χρησιμοποιούμε τη μέθοδο StartingPosition()
                    player.ResetHealth(); // Επαναφορά των ζωών του παίκτη
                }
            }
        }
    }
}

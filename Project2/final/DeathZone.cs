//για τον κύκλο πίσω απο τον εχθρό μαϊμού
//

using UnityEngine;

public class DeathZone : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ελέγχουμε αν το αντικείμενο που συγκρούεται είναι ο παίκτης
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Playerd got hit!");

            // Αναζητούμε τον Player
            PlatformerPlayer player = collision.gameObject.GetComponent<PlatformerPlayer>();

            if (player != null)
            {
                // health του player -1
                player.TakeDamage();

                // Αν ο παίκτης έχει 0 ζωές, κάνουμε restart του παιχνιδιού
                if (player.GetHealth() <= 0)
                {
                    // Επαναφορά του παίκτη στην αρχική του θέση
                    player.transform.position = player.StartingPosition(); 
                    player.ResetHealth(); // Επαναφορά των ζωών του παίκτη
                }
            }
        }
    }
}

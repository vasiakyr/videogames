//script για το bullet του enemy2(monkey)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2.0f; 
    public float moveDistance = 5.0f; 

    private Vector3 startPos; 
    private int direction = 1; 

    void Start()
    {
        startPos = transform.position; // Αποθήκευση της αρχικής θέσης
    }

    void Update()
    {
        // Υπολογισμός της κίνησης
        float movement = direction * speed * Time.deltaTime;
        transform.Translate(movement, 0, 0);

        // Έλεγχος αν έφτασε την απόσταση
        if (Vector3.Distance(startPos, transform.position) >= moveDistance)
        {
            direction *= -1; // Αντιστροφή κατεύθυνσης
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Ελέγχουμε αν το αντικείμενο που συγκρούεται είναι ο παίκτης
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Playerd got hit!");

            PlatformerPlayer player = collision.gameObject.GetComponent<PlatformerPlayer>();

            if (player != null)
            {
                // player health -1
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

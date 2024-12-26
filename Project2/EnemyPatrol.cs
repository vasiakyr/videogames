using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2.0f; // Ταχύτητα κίνησης
    public float moveDistance = 5.0f; // Απόσταση που κινείται δεξιά-αριστερά

    private Vector3 startPos; // Αρχική θέση
    private int direction = 1; // 1 για δεξιά, -1 για αριστερά

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
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Ο παίκτης χτυπήθηκε από τον εχθρό!");
            // Εδώ μπορείτε να καλέσετε τη μέθοδο TakeDamage() του παίκτη
        }
    }
}


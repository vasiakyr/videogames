// ερωτημα 2 αυτο με το μήνυμα 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Για την εμφάνιση μηνύματος

public class Goal : MonoBehaviour
{
    public string victoryMessage = "Συγχαρητήρια! Κέρδισες!";
    public Text victoryText; // Αναφορά στο UI στοιχείο που θα εμφανίζει το μήνυμα

    private bool gameWon = false;

    void Start()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false); // Αρχικά κρύβουμε το μήνυμα
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !gameWon)
        {
            gameWon = true;
            Debug.Log(victoryMessage);

            if (victoryText != null)
            {
                victoryText.text = victoryMessage;
                victoryText.gameObject.SetActive(true); // Εμφάνιση του μηνύματος
            }

            // Εάν θέλουμε να παγώσουμε το παιχνίδι
            Time.timeScale = 0; 
        }
    }
}

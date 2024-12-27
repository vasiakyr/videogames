using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Για μηνυμα

public class Goal : MonoBehaviour
{
    public string victoryMessage = "Congratulations! You won!";
    public Text victoryText; //  εμφανίζει το μήνυμα

    private bool gameWon = false;

    void Start()
    {
        if (victoryText != null)
        {
            victoryText.gameObject.SetActive(false); // αρχικά  το μήνυμα δε φαινεται
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

            // για να παγωσει- σταματησει το παιχνιδι
            Time.timeScale = 0; 
        }
    }
}

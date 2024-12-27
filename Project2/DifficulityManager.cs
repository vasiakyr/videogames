//script που συνδέεται με το canvas και παίρνει την τιμή που διαλέγει ο παίκτης απο το dropdown του μενού
//και την προσαρμόζει στον enemy

using TMPro; // Για το TMP_Dropdown
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public TMP_Dropdown difficultyDropdown; // Αναφορά στο TMP_Dropdown
    public EnemyMovement enemyMovement; // Αναφορά στο EnemyMovement για να ρυθμίσουμε την ταχύτητα του εχθρού

    void Start()
    {
        // Αρχικοποίηση της ταχύτητας με βάση την επιλεγμένη δυσκολία
        UpdateEnemySpeed();

        // Προσθήκη listener για την αλλαγή της δυσκολίας από το dropdown
        difficultyDropdown.onValueChanged.AddListener(delegate {
            UpdateEnemySpeed();
        });
    }

    // Ενημέρωση της ταχύτητας του εχθρού ανάλογα με την επιλογή του dropdown
    void UpdateEnemySpeed()
    {
        int selectedDifficulty = difficultyDropdown.value;

        switch (selectedDifficulty)
        {
            case 0: // Easy
                enemyMovement.speed = 1.0f; // Μειωμένη ταχύτητα
                break;
            case 1: // Medium
                enemyMovement.speed = 2.0f; // Κανονική ταχύτητα
                break;
            case 2: // Hard
                enemyMovement.speed = 3.0f; // Αυξημένη ταχύτητα
                break;
            default:
                enemyMovement.speed = 2.0f; // Κανονική ταχύτητα
                break;
        }
    }
}

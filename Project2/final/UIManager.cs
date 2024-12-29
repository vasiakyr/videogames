using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI failText; // Αναφορά στο TextMeshProUGUI για να εμφανίζεται το κείμενο των αποτυχιών

    void Start()
    {
        // Αρχικοποιούμε το κείμενο με την τρέχουσα τιμή αποτυχιών
        UpdateFailCount();
    }

    void Update()
    {
        // Ενημερώνουμε το κείμενο της αποτυχίας σε κάθε frame
        UpdateFailCount();
    }

    void UpdateFailCount()
    {
        // Παίρνουμε την τιμή των αποτυχιών από το PlayerPrefs και την εμφανίζουμε
        int failCount = PlayerPrefs.GetInt("FailCount", 0);
        failText.text = "Αποτυχίες: " + failCount.ToString();
    }

    // Μέθοδος για την αύξηση του αριθμού αποτυχιών
    public void IncreaseFailCount()
    {
        // Παίρνουμε τον τρέχοντα αριθμό των αποτυχιών
        int currentFailCount = PlayerPrefs.GetInt("FailCount", 0);

        // Αυξάνουμε τον αριθμό κατά 1
        currentFailCount++;

        // Αποθηκεύουμε ξανά τον αριθμό των αποτυχιών
        PlayerPrefs.SetInt("FailCount", currentFailCount);

        // Ενημερώνουμε το κείμενο
        UpdateFailCount();
    }
}

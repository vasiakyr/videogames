using UnityEngine;
using TMPro; // Εισαγωγή του TextMeshPro

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
}

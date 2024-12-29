using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Το UI του μενού παύσης
    private bool isPaused = false;  // Ελέγχει αν το παιχνίδι είναι σε παύση

    void Start()
    {
        // Αρχικά απενεργοποιούμε το μενού
        pauseMenuUI.SetActive(false);
    }

    void Update()
    {
        // Παύση του παιχνιδιού όταν πατηθεί το πλήκτρο Escape ή P
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
                ContinueGame();  // Αν το παιχνίδι είναι ήδη σε παύση, συνεχίζουμε
            else
                PauseGame();  // Αλλιώς το παύουμε
        }
    }

    // Συνάρτηση για να παύσουμε το παιχνίδι
    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);  // Εμφάνιση του μενού παύσης
        Time.timeScale = 0f;  // Σταματάμε το χρόνο του παιχνιδιού (παύση)
        isPaused = true;  // Το παιχνίδι είναι σε παύση
    }

    // Συνάρτηση για να συνεχίσουμε το παιχνίδι
    public void ContinueGame()
    {
        pauseMenuUI.SetActive(false);  // Κρύβουμε το μενού παύσης
        Time.timeScale = 1f;  // Επαναφέρουμε το χρόνο του παιχνιδιού (συνέχιση)
        isPaused = false;  // Το παιχνίδι δεν είναι πια σε παύση
    }

    // Συνάρτηση για να κάνουμε επανεκκίνηση του παιχνιδιού και να μηδενίσουμε τις αποτυχίες
    public void RestartGame()
    {
        Debug.Log("Restart button clicked, FailCount reset to 0");
        PlayerPrefs.SetInt("FailCount", 0);  // Μηδενίζουμε τις αποτυχίες
        Time.timeScale = 1f;  // Επαναφέρουμε το χρόνο του παιχνιδιού
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Επανεκκίνηση της σκηνής
    }
}

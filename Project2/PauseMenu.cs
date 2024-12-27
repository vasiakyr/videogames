//όταν ο παίκτης πατάει esc του εμφανίζεται το μενού τον επιλογών, για την ώρα μπορεί να κάνει μόνο restart το παιχνίδι


using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel; // Αναφορά στο Panel του μενού παύσης
    private bool isPaused = false; // Κατάσταση παύσης

    void Start()
    {
        // Σιγουρέψου ότι το Panel είναι κρυμμένο στην αρχή
        pausePanel.SetActive(false);
    }

    void Update()
    {
        // Ενεργοποίηση/Απενεργοποίηση παύσης με το πλήκτρο Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        pausePanel.SetActive(true); // Εμφάνιση του Panel
        Time.timeScale = 0; // Πάγωμα χρόνου παιχνιδιού
    }

    public void ResumeGame()
    {
        isPaused = false;
        pausePanel.SetActive(false); // Απόκρυψη του Panel όταν ξεκινάει να τρέχει το παιχνίδι
        Time.timeScale = 1; // Επαναφορά χρόνου παιχνιδιού
    }

    public void RestartGame()
    {
        Time.timeScale = 1; // Επαναφορά χρόνου σε κανονική ροή
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Επανεκκίνηση σκηνής
    }
}

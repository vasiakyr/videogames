//τώρα όταν πατάει ο παίκτης restart μηδενίζεται ο μετρητής των αποτυχιών του


using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI; // Αναφορά στο GameObject του μενού παύσης

    void Start()
    {
        // Εξασφαλίζουμε ότι το μενού παύσης είναι ανενεργό στην αρχή του παιχνιδιού
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        else
        {
            Debug.LogError("Το pauseMenuUI δεν έχει οριστεί στον Unity Inspector!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseMenuUI.activeSelf)
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
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0; // Παύση χρόνου
        }
    }

    public void ResumeGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
            Time.timeScale = 1; // Επανεκκίνηση χρόνου
        }
    }

    public void RestartGame()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }

        // Μηδενισμός αποτυχιών
        PlayerPrefs.SetInt("FailCount", 0);

        // Επαναφορά του χρόνου και επανεκκίνηση της σκηνής
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

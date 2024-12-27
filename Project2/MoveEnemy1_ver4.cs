using UnityEngine;
using UnityEngine.SceneManagement; // Για να χρησιμοποιήσουμε το SceneManager

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Ταχύτητα κίνησης
    public float leftLimit = -5f; // Αριστερό όριο (x θέση)
    public float rightLimit = 5f; // Δεξιό όριο (x θέση)

    private bool movingRight = true; // Αν ο εχθρός κινείται δεξιά

    void Update()
    {
        // Κίνηση δεξιά-αριστερά
        if (movingRight)
        {
            if (transform.position.x + speed * Time.deltaTime <= rightLimit)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(rightLimit, transform.position.y);
                movingRight = false;
                Flip(); 
            }
        }
        else
        {
            if (transform.position.x - speed * Time.deltaTime >= leftLimit)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(leftLimit, transform.position.y);
                movingRight = true;
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ο Παίκτης έχασε! Συγκρούστηκε με τον Εχθρό.");
            
            // Αναζητούμε το UIManager στη σκηνή και καλούμε τη μέθοδο IncreaseFailCount
            UIManager uiManager = FindObjectOfType<UIManager>();
            if (uiManager != null)
            {
                uiManager.IncreaseFailCount();  // Αύξηση του αριθμού αποτυχιών
            }

            RestartGame();
        }
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

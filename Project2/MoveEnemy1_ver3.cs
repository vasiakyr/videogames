//γινεται restart του παιχνιδιού όταν ο παίκτης πέσει πάνω στον enemy1
//αυτό γίνετια χάρης τον SceneManager


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
            // Αν η τρέχουσα θέση + η ταχύτητα κίνησης υπερβαίνει το δεξιό όριο, σταματάμε την κίνηση
            if (transform.position.x + speed * Time.deltaTime <= rightLimit)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(rightLimit, transform.position.y); // Ρυθμίζουμε τη θέση στα όρια
                movingRight = false; // Γύρνα αριστερά
                Flip(); // Αντιστροφή της κατεύθυνσης του sprite
            }
        }
        else
        {
            // Αν η τρέχουσα θέση - η ταχύτητα κίνησης είναι μεγαλύτερη ή ίση από το αριστερό όριο, σταματάμε την κίνηση
            if (transform.position.x - speed * Time.deltaTime >= leftLimit)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);
            }
            else
            {
                transform.position = new Vector2(leftLimit, transform.position.y); // Ρυθμίζουμε τη θέση στα όρια
                movingRight = true; // Γύρνα δεξιά
                Flip(); // Αντιστροφή της κατεύθυνσης του sprite
            }
        }
    }

    // Αντιστροφή της κατεύθυνσης του sprite
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Αντιστροφή του x άξονα
        transform.localScale = scale;
    }

    // Εμφάνιση ορίων στον Editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftLimit, transform.position.y, transform.position.z), 
                        new Vector3(rightLimit, transform.position.y, transform.position.z));
    }

    // Ανίχνευση σύγκρουσης με τον παίκτη
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Αν ο εχθρός συγκρουστεί με το Player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Εμφάνιση μηνύματος στην κονσόλα
            Debug.Log("Ο Παίκτης έχασε! Συγκρούστηκε με τον Εχθρό.");
            
            // Επανεκκίνηση του παιχνιδιού (φόρτωση της τρέχουσας σκηνής)
            RestartGame();
        }
    }

    // Μέθοδος για να επανεκκινήσει το παιχνίδι
    void RestartGame()
    {
        // Φόρτωση της τρέχουσας σκηνής για επανεκκίνηση
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

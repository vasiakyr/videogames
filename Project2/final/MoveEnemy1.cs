//για τον enemy1 δηλαδή τον πρώτο που συναντά και καλλύπτει απλά συνεχώς μια συγκεκριμένη διαδρομή

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; 
    public float leftLimit = -5f; 
    public float rightLimit = 5f;

    private bool movingRight = true; 

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
            Debug.Log("Ο Παίκτης έχασε ζωή! Συγκρούστηκε με τον Εχθρό.");
            
            // Αναζητούμε τον Player
            PlatformerPlayer player = collision.gameObject.GetComponent<PlatformerPlayer>();

            if (player != null)
            {
                // Μείωση της ζωής του παίκτη
                player.TakeDamage();

                // Αν ο παίκτης έχει 0 ζωές, κάνουμε restart του παιχνιδιού
                if (player.GetHealth() <= 0)
                {
                    RestartGame();
                }
            }
        }
    }

    void RestartGame()
    {
        // Αυξάνουμε τον αριθμό των αποτυχιών
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.IncreaseFailCount();  
        }

        // restart του παιχνιδιού
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; // Ταχύτητα κίνησης
    public float leftLimit = -0.5f; // Αριστερό όριο (x θέση)
    public float rightLimit = 0.5f; // Δεξιό όριο (x θέση)

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
}

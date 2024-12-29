using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D box;
    public float jumpForce = 12.0f;
    private int health = 3; // ζωές του παίκτη
    private Vector3 startingPosition;

    private Collider2D currentPlatform;

    // Νέα παράμετρος για την δύναμη του trampoline
    public float trampolineJumpForce = 15.0f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        startingPosition = transform.position;
    }

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.linearVelocity.y);
        body.linearVelocity = movement;

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x - .1f, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x + .1f, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = hit != null;
        if (grounded && hit.CompareTag("Platform"))
        {
            currentPlatform = hit;
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollisionTemporarily());
            }
        }

        anim.SetFloat("speed", Mathf.Abs(deltaX));
    }

    IEnumerator DisableCollisionTemporarily()
    {
        if (currentPlatform != null)
        {
            Physics2D.IgnoreCollision(box, currentPlatform, true);
            body.linearVelocity = new Vector2(body.linearVelocity.x, -2.0f);
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(box, currentPlatform, false);
        }
    }

    public void TakeDamage()
    {
        health--;
        Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void Die()
    {
        Debug.Log("Player has died!");

        // αυξάνουμε τον αριθμό των αποτυχιών όταν ο παίκτης χάνει όλες τις ζωές του
        // ο αριθμός αυτός φαίνεται στην οθόνη του παιχνιδιού
        UIManager uiManager = FindObjectOfType<UIManager>();
        if (uiManager != null)
        {
            uiManager.IncreaseFailCount();
        }

        // restart όταν χάνει κ τις 3 ζωές και επαναφορά της ζωής του
        transform.position = startingPosition;
        health = 3;
    }

    //έλεγχος επαφής με εμπόδια ή τραμπολίνο
void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("Spike"))
    {
        TakeDamage(); // Χάνει ζωή όταν ακουμπάει το Spike
    }
    else if (collision.CompareTag("DeathZone"))
    {
        TakeDamage(); 
    }
    else if (collision.CompareTag("Trampoline"))
    {
        Debug.Log("Player stepped on trampoline");
        body.linearVelocity = new Vector2(body.linearVelocity.x, trampolineJumpForce); // Άλμα με trampoline
    }
}

    public void ResetHealth()
    {
        health = 3; // Επαναφορά των ζωών του παίκτη
    }

    public Vector3 StartingPosition()
    {
        return startingPosition; // Επιστρέφει την αρχική θέση του παίκτη
    }
}

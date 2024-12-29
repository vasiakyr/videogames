using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f; // Ταχύτητα κίνησης
    private Rigidbody2D body; // Αναφορά στο Rigidbody2D
    private Animator anim; // Αναφορά στον Animator
    private BoxCollider2D box; // Collider του παίκτη
    public float jumpForce = 12.0f; // Δύναμη άλματος
    public float trampolineJumpForce = 20.0f; // Δύναμη για trampoline

    private int health = 3; // Ζωές του παίκτη
    private Collider2D currentPlatform; // Πλατφόρμα κάτω από τον παίκτη

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // Κίνηση παίκτη
        float deltaX = Input.GetAxis("Horizontal") * speed;
        Vector2 movement = new Vector2(deltaX, body.velocity.y); // Ενημέρωση της ταχύτητας
        body.velocity = movement;

        // Ανίχνευση επαφής με το έδαφος
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x - .1f, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x + .1f, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = hit != null; // Ελέγχουμε αν ο παίκτης ακουμπάει σε κάτι

        if (grounded && hit.CompareTag("Platform"))
        {
            currentPlatform = hit; // Καταχωρούμε την πλατφόρμα κάτω από τον παίκτη
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        // Άλμα
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Ενημέρωση του Animator για την κίνηση
        anim.SetFloat("speed", Mathf.Abs(deltaX));

        // Αν ο παίκτης είναι πάνω σε μια κινούμενη πλατφόρμα, τον συνδέουμε με αυτήν
        if (currentPlatform != null)
        {
            MovingPlatform platform = currentPlatform.GetComponent<MovingPlatform>();
            if (platform != null)
            {
                transform.parent = platform.transform; // Ο παίκτης ακολουθεί την πλατφόρμα
            }
        }
        else
        {
            transform.parent = null; // Αν δεν είναι πάνω σε πλατφόρμα, αποσυνδέεται
        }

        // Αν ο παίκτης θέλει να κατέβει από την πλατφόρμα
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentPlatform != null)
            {
                StartCoroutine(DisableCollisionTemporarily());
            }
        }
    }

    // Αποσύνδεση του παίκτη από την πλατφόρμα όταν πατάει το πλήκτρο S ή DownArrow
    IEnumerator DisableCollisionTemporarily()
    {
        if (currentPlatform != null)
        {
            Physics2D.IgnoreCollision(box, currentPlatform, true);
            body.velocity = new Vector2(body.velocity.x, -2.0f); // Κίνηση προς τα κάτω
            yield return new WaitForSeconds(0.5f);
            Physics2D.IgnoreCollision(box, currentPlatform, false);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Spike"))
        {
            Debug.Log("Player hit a spike!");
            TakeDamage();
        }
        else if (collision.CompareTag("DeathZone"))
        {
            Debug.Log("Player fell into a pit!");
            Die();
        }
        else if (collision.CompareTag("Trampoline"))
        {
            Debug.Log("Player stepped on trampoline");
            body.velocity = new Vector2(body.velocity.x, trampolineJumpForce);
        }
    }

    void TakeDamage()
    {
        health--;
        Debug.Log("Player health: " + health);
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player has died!");
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}

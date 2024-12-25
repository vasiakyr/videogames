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
        Vector2 movement = new Vector2(deltaX, body.linearVelocity.y); //vector 2 διαστάσεων τώρα αφού έχουμε 2d κόσμο
        body.linearVelocity = movement;

        // Ανίχνευση επαφής με το έδαφος
        //ώστε να μην μπορεί να κάνει κ άλματα στον αέρα
        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x - .1f, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x + .1f, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = hit != null; // Ελέγχουμε αν ο παίκτης ακουμπάει στο έδαφος
        if (grounded && hit.CompareTag("Platform"))
        {
            currentPlatform = hit; // Καταχωρούμε την πλατφόρμα κάτω από τον παίκτη
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        // Άλμα
        if (grounded && Input.GetKeyDown(KeyCode.Space)) //έλεγχος αν είναι στο έδαφος κ πατήθηκε το κουμπί space
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        // Κατάβαση από πλατφόρμα
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Key S pressed");
            if (currentPlatform != null)
            {
                Debug.Log("Ignoring collision with platform: " + currentPlatform.name);
                StartCoroutine(DisableCollisionTemporarily());
            }
            else
            {
                Debug.Log("No platform detected below player.");
            }
        }

        MovingPlatform platform = null; //για να μην πέφτει ο παίκτης όταν είναι πάνω στο platform κ αυτό κινείται

        if (hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }

        if (platform != null)
        {
            transform.parent = platform.transform;
        }

        else
        {
            transform.parent = null;
        }

        // Ενημέρωση του Animator
        anim.SetFloat("speed", Mathf.Abs(deltaX)); // μεταβλητή που δείχνει πότε αλλάζουμε behavior στο map του window animator
        //(abs) γιατί αν κινούμαστε αριστερά τότε το speed παίρνει αρνητικές 
    }

//ερώτημα 1: πατώντας το κουμπί "S" ο παίκτης μπορεί να καταίβει αμέσως απο την κινούμενη πλατφόρμα
    IEnumerator DisableCollisionTemporarily()
{
    if (currentPlatform != null)
    {
        Debug.Log("Ignoring collision with: " + currentPlatform.name);
        Physics2D.IgnoreCollision(box, currentPlatform, true);

        // Προσθήκη ώθησης προς τα κάτω
        body.linearVelocity = new Vector2(body.linearVelocity.x, -2.0f);

        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreCollision(box, currentPlatform, false);
        Debug.Log("Collision restored with platform: " + currentPlatform.name);
    }
    else
    {
        Debug.Log("No platform to ignore collision with");
    }
}

}

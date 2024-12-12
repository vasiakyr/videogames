using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 4.5f;
    private Rigidbody2D body;
    private Animator anim; //για την επεξεργασία του animator
    private BoxCollider2D box;
    public float jumpForce = 12.0f;

    void Start() 
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    void Update()
        // κωδικασ γαι κίνηση
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        //2 επειδή ειμαστε 2d
        Vector2 movement = new Vector2(deltaX, body.linearVelocity.y);
        body.linearVelocity = movement; // αφου 2d

        Vector3 max = box.bounds.max;
        Vector3 min = box.bounds.min;
        Vector2 corner1 = new Vector2(max.x - .01f, (min.y - .1f));
        Vector2 corner2 = new Vector2(min.x - .01f, (min.y - .2f));
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;

        if (hit != null)
        {
            grounded = true;
        }

        body.gravityScale = (grounded && Mathf.Approximately(deltaX, 0)) ? 0 : 1;

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        MovingPlatform platform = null;

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

        anim.SetFloat("speed", Mathf.Abs(deltaX)); //ορίζουμε τη παράμετρο setFloat, εδώ speed

        Vector3 pScale = Vector3.one;
        if (platform != null)
        {
            pScale = platform.transform.localScale;
        }
        // deltax δίνει το speed, σε απότυλη τιμή γιατι αν κινούμαι αριστερά θα είναι αρνητική η τιμή
        // το speed το θέλω πάντα θετικό

        //μέθοδος που δίνει τη σύγκριση του deltax με το 0, όταν ειναι κοντά στο 0 δίνει 0 
        if (!Mathf.Approximately(deltaX, 0))  
        {
            // όταν κινείται ο παίκτης γίνεται αυτό , η sign δίνει το πρόσημο
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
    }
}



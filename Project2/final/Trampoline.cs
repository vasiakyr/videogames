using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float trampolineJumpForce = 20.0f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player stepped on trampoline");
            Rigidbody2D playerBody = collision.GetComponent<Rigidbody2D>();
            if (playerBody != null)
            {
                playerBody.linearVelocity = new Vector2(playerBody.linearVelocity.x, trampolineJumpForce);
            }
        }
    }
}

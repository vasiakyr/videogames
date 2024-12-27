using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 10; //  η ζωη που μπορει να έχειο παικτης μας 
    private int currentHealth; // η ζωη του τωρα 

    void Start()
    {
        currentHealth = maxHealth; // οριζω ζωη παικτη 
    }

    // για οταν πρεπει ο παικτης να χασει ζωη εχω->
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // μειωση ζωής

        Debug.Log("Ο παίκτης χτυπήθηκε! Υπόλοιπο ζωής: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die(); // ο παικτης έχασε
        }
    }
    //όταν πεθαινει και πρεπει να αρχίσει από την αρχη 
    void Die()
    {
        Debug.Log("Ο παίκτης έχασε!");
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet; // Το αντικείμενο της σφαίρας
    public Transform bulletPos; // Η θέση από την οποία θα εκτοξευθεί η σφαίρα
    private float timer; // Χρονομέτρηση για τη δημιουργία της σφαίρας

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Αυξάνει το χρονόμετρο με το χρόνο του τελευταίου frame
        timer += Time.deltaTime;

        // Όταν το timer ξεπεράσει τα 2 δευτερόλεπτα, εκτοξεύουμε τη σφαίρα
        if (timer > 2f)
        {
            timer = 0f; // Επαναφορά του timer
            shoot(); // Εκτέλεση της συνάρτησης shoot
        } 
    }

    // Συνάρτηση για να εκτοξεύσει τη σφαίρα
    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity); // Δημιουργία της σφαίρας στη θέση του bulletPos
    }
}

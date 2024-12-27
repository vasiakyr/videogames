using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public float detectionRange = 10f; // Εύρος ανίχνευσης
    public float fieldOfView = 45f; // Γωνία ανίχνευσης
    public Transform firePrefab; // Αναφορά στο prefab του fireball
    public Transform firePoint; // Σημείο εκτόξευσης fire projectiles
    public float fireRate = 1f; // Ρυθμός ρίψης
    public float projectileSpeed = 5f; // Ταχύτητα των projectiles
    public LayerMask detectionLayer; // Layer για τον παίκτη
    public float activationDistance = 5f; // Απόσταση για ενεργοποίηση της ρίψης

    private Transform player;
    private float nextFireTime;
    private Animator animator; // Αναφορά στον Animator για τα animations

    void Start()
    {
        // Εντοπίζει τον παίκτη μέσω του Tag "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Παίρνουμε τον Animator του εχθρού
    }

    void Update()
    {
        if (PlayerInFOV() && IsPlayerInRange())
        {
            if (Time.time >= nextFireTime)
            {
                ShootProjectile();
                nextFireTime = Time.time + 1f / fireRate; // Υπολογισμός του χρόνου για το επόμενο fire
            }
        }
    }

    // Ελέγχει αν ο παίκτης είναι εντός του πεδίου οπτικής γωνίας (FOV)
    bool PlayerInFOV()
    {
        if (player == null)
            return false;

        // Υπολογίζει την απόσταση
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
            return false;

        // Υπολογίζει τη γωνία μεταξύ του εχθρού και του παίκτη
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float angle = Vector3.Angle(transform.right, directionToPlayer);

        if (angle > fieldOfView / 2f)
            return false;

        // Χρησιμοποιεί Raycast για να ελέγξει αν ο παίκτης είναι ορατός
        RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, detectionRange, detectionLayer);
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }

    // Ελέγχει αν ο παίκτης είναι εντός της καθορισμένης απόστασης
    bool IsPlayerInRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        return distanceToPlayer <= activationDistance;
    }

    // Ρίχνει το fireball προς τον παίκτη
    void ShootProjectile()
    {
        if (firePoint == null || firePrefab == null)
            return;

        // Δημιουργία του fireball
        Transform fireball = Instantiate(firePrefab, firePoint.position, firePoint.rotation);

        // Ορισμός κατεύθυνσης και ταχύτητας
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (player.position - firePoint.position).normalized;
            rb.linearVelocity = direction * projectileSpeed;
        }

        // Ενεργοποιεί το animation της ρίψης
        animator.SetTrigger("Shoot"); // Παίρνει το trigger "Shoot" για να παίξει το animation

        // Προαιρετικά: Καταστροφή του projectile μετά από 5 δευτερόλεπτα
        Destroy(fireball.gameObject, 5f);
    }

    void OnDrawGizmosSelected()
    {
        // Σχεδιάζει το FOV και το detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        Gizmos.color = Color.blue;
        Vector3 fovLine1 = Quaternion.Euler(0, 0, fieldOfView / 2) * transform.right * detectionRange;
        Vector3 fovLine2 = Quaternion.Euler(0, 0, -fieldOfView / 2) * transform.right * detectionRange;
        Gizmos.DrawLine(transform.position, transform.position + fovLine1);
        Gizmos.DrawLine(transform.position, transform.position + fovLine2);

        // Σχεδιάζει την περιοχή για την ενεργοποίηση των φωτιών
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activationDistance); // Δείχνει την απόσταση ενεργοποίησης
    }
}

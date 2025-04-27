using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [Header("Enemy movement")]
    public float movementSpeed = 2f;
    public float stopDistance = 3f;
    public float offsetDistance = 1f;
    public DirectionController directionController;
    public MonoBehaviour[] scriptsToDisable;

    [Header("Enemy shooting")]
    public float shootDistance = 5f;
    public float shootCooldown = 0.2f;
    public Shooter shooter;
    public Rigidbody2D rb;
    public AudioSource shootSound;

    private float _lastShotTime = Mathf.NegativeInfinity;
    private GameObject player;


    void FixedUpdate()
    {
        if (player == null)
        {
            rb.velocity = Vector2.zero;
            toggleScripts(true);
            return;
        }

        toggleScripts(false);

        // Calculate direction and distances
        float distanceToPlayer = Mathf.Abs(player.transform.position.x - rb.transform.position.x);
        float faceDirection = Mathf.Sign(player.transform.position.x - rb.transform.position.x);


        bool playerIsToRight = player.transform.position.x > rb.transform.position.x;



        // Flip visual
        directionController?.FaceDirection(faceDirection);

        //// Sync shooter with facing direction
        shooter.facingRight = faceDirection >= 0;

        // Shoot if in range and cooldown passed
        if (distanceToPlayer <= shootDistance && Time.time >= _lastShotTime + shootCooldown)
        {
            shooter.Shoot();
            if(shootSound != null)
            {
                shootSound.Play();
            }
            _lastShotTime = Time.time;
        }

        // Move if too far from player
        if (distanceToPlayer > stopDistance)
        {
            Vector2 direction = playerIsToRight ? Vector2.right : Vector2.left;
            rb.velocity = new Vector2(direction.x * movementSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = null;
        }
    }

    private void toggleScripts(bool enabled)
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = enabled;
        }
    }
}

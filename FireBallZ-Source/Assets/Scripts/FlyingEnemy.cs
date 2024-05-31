using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Pathfinding;

public class FlyingEnemy : MonoBehaviour
{
    public float health = 3.0f;
    private float initialHitpoints;
    private SpriteRenderer sr;
    private AIPath aiPath;
    public Transform player;
    public float detectionRadius = 10f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        initialHitpoints = health;
        sr = GetComponent<SpriteRenderer>();
        aiPath = GetComponent<AIPath>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > detectionRadius) {
            aiPath.maxSpeed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        } else {
            aiPath.maxSpeed = 2;
            rb.constraints = RigidbodyConstraints2D.None;
        }

        if (aiPath.desiredVelocity.x >= 0.01f) {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        } else if (aiPath.desiredVelocity.x <= -0.01f) {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fire")) {
            health--;
            UpdateOpacity();
        }
    }

    void UpdateOpacity()
    {
        float opacity = health / initialHitpoints;  // Calculate new opacity
        Color color = sr.color;
        color.a = opacity;
        sr.color = color;
    }
}

/**
public class FlyingEnemy : MonoBehaviour
{
    public int health = 7;
    private int initialHitpoints;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        initialHitpoints = health;
        // sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fire")) {
            health--;
            UpdateOpacity();
        }
    }

    void UpdateOpacity()
    {
        float opacity = (float)health / (float)initialHitpoints;  // Calculate new opacity
        Color color = sr.color;
        color.a = opacity;
        sr.color = color;
    }
}
**/
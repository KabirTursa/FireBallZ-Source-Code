using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingGFX : MonoBehaviour
{
    public AIPath aiPath;
    public Transform player;
    private float detectionRadius = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    // Update is called once per frame
    void Update()
    {
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
}


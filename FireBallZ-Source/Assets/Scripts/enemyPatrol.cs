using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        else 
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currentPoint.position) < 0.3f && currentPoint == pointB.transform)
        {
            flip();
                currentPoint = pointA.transform;
        }

         if(Vector2.Distance(transform.position, currentPoint.position) < 0.3f && currentPoint == pointA.transform)
        {
            flip();
                currentPoint = pointB.transform;
        }
    }

    private void flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointA.transform.position, 0.3f);
        Gizmos.DrawWireSphere(pointB.transform.position, 0.3f);
        Gizmos.DrawLine(pointA.transform.position, pointB.transform.position);
    }
}

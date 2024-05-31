using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    public float speed;
    public float jumpPower;
    private CapsuleCollider2D collider;
    public LayerMask groundLayer;
    private float horizontalInput;
    private bool isOnGround;

    [Header("multiple Jumps")]
    public int extraJumps;
    private int jumpCounter;

    // Start is called before the first frame update
    void Start()
    {
        isOnGround = true;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);


        //flip player when moving left or right
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector2.one;
        }
        else if (horizontalInput < -0.1f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        //Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());

        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);

        if (isGrounded())
        {
            jumpCounter = extraJumps;
        }   
    }
  
    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
            isOnGround = false;
        }
        else
        {
            if (jumpCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                jumpCounter--;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }

    private bool isGrounded()
    {
        //still bugs out when you bonk ceiling (it gives you an extra midair jump), but does not bug out on corner tiles
        Vector3 colliderBoundsSize = new Vector3(collider.bounds.size.x / 2, collider.bounds.size.y, collider.bounds.size.z);
        RaycastHit2D raycastHit = Physics2D.BoxCast(collider.bounds.center, colliderBoundsSize, 0, Vector2.down, 0.1f, groundLayer);
        RaycastHit2D raycastHit2 = Physics2D.BoxCast(collider.bounds.center, colliderBoundsSize, 0, Vector2.up, 0.1f, groundLayer);
        return ((raycastHit.collider != null || raycastHit2.collider != null) && isOnGround);
        //return isOnGround;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }
}

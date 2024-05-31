using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class health : MonoBehaviour
{
    [Header ("Health")]
    public float startingHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    public bool dead { get; private set; }
    private bool canTakeDamage;

    public Text respawnText;

    [Header("iFrames")]
    public float iFramesDuration;
    public int numberOfFlashes;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        startingHealth = gameHealthManager.Instance.maxHitPoints;
        canTakeDamage = true;
        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("BossBullet") && canTakeDamage) {
            TakeDamage(1.0f);
        }

        if(collision.gameObject.CompareTag("PenguinBullet") && canTakeDamage) {
            TakeDamage(0.5f);
        }

        if(collision.gameObject.CompareTag("Bat") && canTakeDamage) {
            TakeDamage(1.5f);
        }

        if (collision.gameObject.CompareTag("Spikes"))
        {
            TakeDamage(5f); //instakill
        }
        if (collision.gameObject.CompareTag("Health"))
        {
            increaseHealth();
        }
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            //bool isGrounded = animator.GetBool("grounded");
            //animator.SetBool("grounded", true);
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
            //if (!isGrounded)
            {
                //animator.SetBool("grounded", false);
            }
        }
        else
        {
            if (!dead)
            {
                animator.SetBool("grounded", true); //ensures the death animation will play regardless if the player is in the air or on the ground
                animator.SetTrigger("die");
                GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
                respawnText.gameObject.SetActive(true);
                //Player dies
                GetComponent<playerMovement>().enabled = false;
                GetComponent<playerAttack>().enabled = false;
                dead = true;
                //Application.Quit();
            }
           
        }
    }
 
    private IEnumerator Invulnerability()
    {

        Physics2D.IgnoreLayerCollision(6,7, true);
        canTakeDamage = false;
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(6, 7, false);
        canTakeDamage = true;
    }

    public void addHealth(float val)
    {
        currentHealth = Mathf.Clamp(currentHealth + val, 0, startingHealth);
    }

    public void respawnHelper()
    {
        dead = false;
        addHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        GetComponent<Rigidbody2D>().freezeRotation = true;
        
        GetComponent<playerMovement>().enabled = true;
        GetComponent<playerAttack>().enabled = true;

        StartCoroutine(Invulnerability()); //give player invulnerability on respawn
    }

    public void increaseHealth()
    { 
        gameHealthManager.Instance.IncreaseMaxHitPoints(); //test
        startingHealth = gameHealthManager.Instance.maxHitPoints; //test
        currentHealth++;
    }
}

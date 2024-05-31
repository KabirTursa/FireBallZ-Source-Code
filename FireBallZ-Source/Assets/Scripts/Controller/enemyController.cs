using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    private Vector2 originalPosition;
    //private Quaternion originalRotation;
    private float originalHealth;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = transform.position;
        if (gameObject.tag == "Enemy")
        {
            originalHealth = GetComponent<EnemyHealth>().healthAmount;
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalColor = spriteRenderer.color;
        }

        if (gameObject.tag == "Bat")
        {
            originalHealth = GetComponent<FlyingEnemy>().health;
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (gameObject.tag == "Boss")
        {
            originalHealth = GetComponent<HealthManager>().healthAmount;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetEnemy()
    {
        Debug.Log("resetting enemy");
        //gameObject.SetActive(true); //reactivate enemy
        transform.position = originalPosition;

        if (gameObject.tag == "Bat")
        {
            GetComponent<FlyingEnemy>().health = originalHealth;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().color = Color.white;
            GetComponent<Collider2D>().enabled = true;
        }

        if (gameObject.tag == "Enemy")
        {
            GetComponent<EnemyHealth>().healthAmount = originalHealth;
            GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<SpriteRenderer>().color = originalColor;
            GetComponent<Collider2D>().enabled = true;


            //turn on scripts specifically for range penguins
            if (GetComponent<enemyShooting>() != null)
            {
                GetComponent<enemyShooting>().enabled = true;
            }
            if (GetComponent<enemyPatrol>() != null)
            {
                GetComponent<enemyPatrol>().enabled = true;
            }

            //turn on scripts specifically for melee penguins
            if (GetComponent<MeleePatrol>() != null)
            {
                GetComponentInParent<MeleePatrol>().enabled = true;
            }
            if (GetComponent<PenguinMelee>() != null)
            {
                GetComponent<PenguinMelee>().enabled = true;
            }
        }
        if (gameObject.tag == "Boss")
        {
            //GetComponent<SpriteRenderer>().enabled = true;
            //GetComponent<Collider2D>().enabled = true;
            GetComponent<HealthManager>().healthAmount = originalHealth;
            GetComponent<HealthManager>().healthBar.fillAmount = 1;
            GetComponent<HealthManager>().parent.SetActive(true);
        }
    
    }
}

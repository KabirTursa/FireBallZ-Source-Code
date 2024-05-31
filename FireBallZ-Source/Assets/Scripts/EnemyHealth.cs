using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float healthAmount = 4.0f;
    private SpriteRenderer enemyRenderer;

    // Start is called before the first frame update
    void Start()
    {
        enemyRenderer = GetComponent<SpriteRenderer>(); // Get the SpriteRenderer component
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Fire")
        {
            Debug.Log("Hit enemy");
            healthAmount--;
            if (enemyRenderer != null)
            {
                enemyRenderer.color = new Color(enemyRenderer.color.r, enemyRenderer.color.g
                    , enemyRenderer.color.b, enemyRenderer.color.a * 0.8f);
            }
            


            if (healthAmount == 0)
            {
                GetComponent<SpriteRenderer>().enabled =false;
                GetComponent<Collider2D>().enabled = false;

                if (GetComponent<enemyShooting>() != null)
                {
                    GetComponent<enemyShooting>().enabled = false;
                }
                if (GetComponent<enemyPatrol>() != null)
                {
                    GetComponent<enemyPatrol>().enabled = false;
                }


                // Melee Animation and Patrol
                // But right now the melee penguin destroy but the animation still plays and 
                // the player still take damage

                if (GetComponent<MeleePatrol>() != null)
                {
                    GetComponentInParent<MeleePatrol>().enabled = false;
                }
                if (GetComponent<PenguinMelee>() != null)
                {
                    GetComponent<PenguinMelee>().enabled = false;
                }
            }
        }
    }
}

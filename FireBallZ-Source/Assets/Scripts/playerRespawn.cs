using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;


public class playerRespawn : MonoBehaviour
{
   
    private Transform currentCheckpoint;
    private health playerHealth;
    enemyController[] enemies;
    private Vector2 startPosition;
    [SerializeField] HealthManager bossHealth;

    public Text respawnText;

    void Start()
    {
        startPosition = transform.position;
        playerHealth = GetComponent<health>();
        enemies = FindObjectsByType<enemyController>(FindObjectsSortMode.None);
        Debug.Log("enemies in array: " + enemies.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<health>().dead == true && Input.GetKeyDown(KeyCode.F))
        {
            Respawn();
        }
    }

    public void Respawn()
    {
        respawnText.gameObject.SetActive(false);
        if (currentCheckpoint == null)
        {
            transform.rotation = Quaternion.identity;
            transform.position = startPosition;
            
        }
        else
        {
            transform.rotation = Quaternion.identity;
            transform.position = currentCheckpoint.position;
        }
        //bossHealth.resetBoss();  //give boss max health back to fix bug where boss lost health if boss was too close to a checkpoint //was this an issue ???
        playerHealth.respawnHelper();
        resetEnemies();

    }

    void resetEnemies()
    {
        Debug.Log("player is respawning, so is the enemy");
        //Debug.Log("enemies being reset are: " + enemies.Length);
        /*
        foreach (enemyController enemy in enemies)
        {
                Debug.Log("found enemy in array, resetting");
                enemy.resetEnemy(); // Reset the enemy
          
        } */

        if (enemies != null)
        {
            foreach (enemyController enemy in enemies)
            {
                if (enemy != null)
                {
                    enemy.resetEnemy();
                }
                
            }
        }
        else
        {
            Debug.LogWarning("No enemies found in the array.");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}

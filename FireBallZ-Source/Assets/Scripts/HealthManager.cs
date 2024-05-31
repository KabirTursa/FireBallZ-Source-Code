using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public GameObject parent;
    public Image healthBar;
    public float healthAmount;
    private float initialHealth;
    public Transform player; // Reference to the player character
    public float detectionRadius;
    [SerializeField] Vector2 initialPosition;

    public GameObject bossDefeatedText;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;
        initialHealth = healthAmount;
        parent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {   
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        
        if (distanceToPlayer <= detectionRadius) {
            parent.SetActive(true); // Show the health bar
        } else {
            parent.SetActive(false); // Hide the health bar
            healthAmount = initialHealth;
            healthBar.fillAmount = healthAmount / initialHealth;
        }

        if (healthAmount == 0) {
            Destroy(transform.gameObject);
            bossDefeatedText.SetActive(true);
            //GetComponent<SpriteRenderer>().enabled = false;
            //GetComponent<Collider2D>().enabled = false;

            parent.SetActive(false);
            //Debug.Log("application quit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Fire")) {
            TakeDamage(2);
        }
    }

    public void TakeDamage(float damage) {
        if (healthAmount > 0) {
            healthAmount -= damage;
        }
        healthBar.fillAmount = healthAmount / initialHealth;
    }

    public float getInitialHealth()
    {
        return initialHealth;
    }

    public void resetBoss()  //called when player dies and respawns
    {
        transform.position = initialPosition;
        healthAmount = initialHealth;
    }
}

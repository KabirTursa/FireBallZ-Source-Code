using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public Transform player; // Reference to the player character
    public float shootingRadius = 5f;
    public int shootingSpeed = 2;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        timer += Time.deltaTime;

        if (distanceToPlayer <= shootingRadius && FindAnyObjectByType<health>().dead == false)
        {
            Debug.Log(FindAnyObjectByType<health>().dead);

            if (timer > shootingSpeed) {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot() {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}

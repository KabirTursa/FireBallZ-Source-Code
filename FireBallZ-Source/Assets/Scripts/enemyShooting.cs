using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform firePoint;

    private float timer;
    private GameObject player;

    public float shootCooldown;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(distance);

        if (distance < 10 && FindAnyObjectByType<health>().dead == false)
        {
            timer += Time.deltaTime;
            
            if (timer > shootCooldown && FindAnyObjectByType<health>().dead == false)
            {
            timer = 0;
            shoot();
            }
        }   

        
    }

    void shoot()
    {
        Instantiate(bullet, firePoint.position, Quaternion.identity);
    }
}

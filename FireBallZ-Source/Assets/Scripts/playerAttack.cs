using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    private Animator Animator;
    public float attackCooldown;
    public Transform firePoint;
    public GameObject[] fireballs;
    private playerMovement playerMovement = null;
    private float cooldownTimer = Mathf.Infinity;

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        playerMovement = GetComponent<playerMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.M)) && cooldownTimer > attackCooldown)
        {
            attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void attack()
    {
        Animator.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[findFireball()].transform.position = firePoint.position;
        fireballs[findFireball()].GetComponent<projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }

    private int findFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}

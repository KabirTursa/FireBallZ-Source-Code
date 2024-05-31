using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class level_Switch : MonoBehaviour
{
    // sceneBuildIndex is the index of the scene you want to switch to
    // the sceneBuildIndex can be found in the build settings
    public int sceneBuildIndex;

    public Text bossStatus;

    private playerRespawn playerRespawn;

    private bool canRespawn;

    private void Start()
    {
        // Find the player object by tag and get the Health component
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerRespawn = player.GetComponent<playerRespawn>();
        }

    }

    private void Update()
    {
        if (canRespawn && Input.GetKeyDown(KeyCode.F))
        {
            playerRespawn.Respawn();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject targetObject = GameObject.Find("Boss");
            if (targetObject == null)
            {
                // Load the scene with the sceneBuildIndex
                Application.LoadLevel(sceneBuildIndex);

                Debug.Log("Switching to next scene");
            }
            else
            {
                Debug.Log("The boss is not dead yet!");
                bossStatus.gameObject.SetActive(true);
                canRespawn = true;

            }





        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bossStatus.gameObject.SetActive(false);
        canRespawn = false;
    }
}

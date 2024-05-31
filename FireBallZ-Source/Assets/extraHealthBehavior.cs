using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class extraHealthBehavior : MonoBehaviour
{
    public GameObject extraHealthText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            extraHealthText.SetActive(true);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthBar : MonoBehaviour
{
    public health playerHealth;
    public Image totalhealthBar;
    public Image currentHealthBar;

    // Start is called before the first frame update
    void Start()
    {
        totalhealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

    // Update is called once per frame
    void Update()
    {
        totalhealthBar.fillAmount = playerHealth.startingHealth / 10; // test
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10;
    }

}

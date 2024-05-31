using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameHealthManager : MonoBehaviour
{
    public static gameHealthManager Instance { get; private set; }

    public int maxHitPoints = 3; // Default max HP

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncreaseMaxHitPoints()
    {
        maxHitPoints += 1;
    }
}

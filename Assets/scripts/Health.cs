using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    public int health { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public int getMaxHealth()
    {
        return maxHealth;
    }
}

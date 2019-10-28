using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider bar;
    [SerializeField]
    GameObject master;
    Health health;
    string name;

    // Start is called before the first frame update
    void Start()
    {
        bar = GetComponent<Slider>();
        name = master.name;
        setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (master == null  || master.activeInHierarchy == false)
        {
            master = GameObject.Find(name);
            setup();
        }
        bar.value = health.health;
        
    }

    void setup()
    {
        health = master.GetComponent<Health>();
        bar.maxValue = health.getMaxHealth();
        bar.value = health.health;
    }
}
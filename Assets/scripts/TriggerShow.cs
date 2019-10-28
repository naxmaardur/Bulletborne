using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerShow : MonoBehaviour
{
    [SerializeField]
    GameObject[] toShow;


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject g in toShow)
            {
                g.SetActive(true);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject g in toShow)
            {
                g.SetActive(false);
            }
        }
    }
}

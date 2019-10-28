using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.hasAxe = true;
            player.lastAxe = null;

            Destroy(gameObject);
        }
    }
}

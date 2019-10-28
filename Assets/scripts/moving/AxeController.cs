using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(movementController))]
public class AxeController : MovingObject
{
    movementController controller;
    public PlayerController player;
    public GameObject groundAxe;
    public Vector3 input;
   
    [SerializeField]
    float MaxMoveTime = 0.5f;
    public float time = 0;
    bool spawn = false;
    [SerializeField]
    private int dammage;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<movementController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time < MaxMoveTime)
        {
            time += Time.deltaTime;
            CalculateVelocity(input);
            controller.move(velocity);
        }
        if (time >= MaxMoveTime)
        {
            Spawn();
        }
        if(controller.collisions.lastTag == "ground")
        {
            Spawn();
        }

    }
    void Spawn()
    {
        if (!spawn)
        {
            spawn = true;
            GameObject axe = Instantiate(groundAxe, transform.position, Quaternion.identity);
            player.lastAxe = axe;
            Destroy(gameObject);
        }
    }
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "ground")
        {
            Spawn();

        }
        if(collision.transform.tag == "enemy")
        {
            collision.gameObject.SendMessage("Hit", dammage);
            player.SendMessage("shake");
        }
    }
}

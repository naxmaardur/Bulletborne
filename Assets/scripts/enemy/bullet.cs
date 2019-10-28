using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(movementController))]
public class bullet : MovingObject
{
    movementController controller;
    [SerializeField]
    Vector3 direction;
    [SerializeField]
    int dammage = 1;
    [SerializeField]
    GameObject explotion;
   
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<movementController>();
    }

    // Update is called once per frame
    void Update()
    {
            Move();
    }

    private void Move()
    {
        if (direction.x != 0 || direction.z != 0)
        {
            CalculateVelocity(direction);
        }
        else
        {
            //unity is blijkbaar vergeten dat transform.forward all een direction is ¯\_(ツ)_/¯
 
            CalculateVelocity(Vector3.forward);
           
        }
       // Debug.Log(velocity);
        controller.move(velocity);
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.tag == "ground")
        {
            DestroyThis();
        }
        if (collision.transform.tag == "Player")
        {
            Debug.Log("" + collision.gameObject.name);
            DestroyThis();
            collision.gameObject.SendMessage("removeHealth", dammage);
        }
    }

    private void DestroyThis()
    {
        GameObject e = Instantiate(explotion, transform.position, Quaternion.identity);
        Quaternion q = e.transform.rotation;
        q.eulerAngles = new Vector3(90, q.eulerAngles.y, q.eulerAngles.z);
        e.transform.rotation = q;
        Destroy(gameObject);
    }
    public void setDirection(Vector3 dir)
    {
        dir.y = 0;
        direction = dir;
    }
}

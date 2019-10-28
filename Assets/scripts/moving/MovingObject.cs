using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{

    protected float velocityXSmoothing;
    protected float velocityZSmoothing;
    protected Vector3 velocity;
    [SerializeField]
    protected float moveSpeed;
    [SerializeField]
    protected float XTimeGround = .1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected void CalculateVelocity(Vector3 dir)
    {
      
        float targetVelocityX = dir.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, XTimeGround);
        float targetVelocityZ = dir.z * moveSpeed;
        velocity.z = Mathf.SmoothDamp(velocity.z, targetVelocityZ, ref velocityZSmoothing, XTimeGround);
    }
}

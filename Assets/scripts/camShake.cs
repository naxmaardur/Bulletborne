using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camShake : MonoBehaviour
{
    bool shaking;
    Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void shake()
    {
        if (!shaking)
        {
            anim.SetTrigger("shake");
            shaking = true;
        }
    }
    public void stopShake()
    {
        shaking = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shadow : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position);
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(90, q.eulerAngles.y, q.eulerAngles.z);
        transform.rotation = q;
    }
}

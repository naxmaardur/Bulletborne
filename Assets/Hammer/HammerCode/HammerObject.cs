using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HammerObject : MonoBehaviour
{
    [SerializeField]
    int status = 0;
    int statusOld = 15;
    [SerializeField]
    GameObject[] tiles;
    GameObject render;
    [SerializeField]
    LayerMask mask;
    HammerController hammer;
    // Start is called before the first frame update
    void Start()
    {
        hammer = GameObject.FindGameObjectWithTag("Hammer").GetComponent<HammerController>();
        tiles = hammer.tiles;
        
    }

    // Update is called once per frame
    void Update()
    {
        check(); 
    }


    public void check()
    {
        status = 0;
        for (int i = 0; i < 4; i++)
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.position, Quaternion.Euler(0, 90*i, 0) * Vector3.forward, out hit, 4,mask))
            {
                if (hit.transform.gameObject != gameObject)
                {
                    switch (i)
                    {
                        case 0:
                            status += 1;
                            break;
                        case 1:
                            status += 2;
                            break;
                        case 2:
                            status += 4;
                            break;
                        case 3:
                            status += 8;
                            break;
                    }
                }
            }
        }
        Render();
    }

    private void Render()
    {
        if(status != statusOld)
        {
            Debug.Log(status);
            statusOld = status;
            if (render)
            {
               Destroy(render);
            }
            render = Instantiate(tiles[status], transform.position, Quaternion.Euler(90,0,0));
        }
       
    }

    private void Remove()
    {
        if (render)
        {
            Destroy(render);
        }
    }

    void spawnObject(GameObject master)
    {
        Remove();
        Instantiate(tiles[status], transform.position, Quaternion.Euler(90, 0, 0), master.transform);
        hammer.removeformArray(gameObject);
        Destroy(gameObject);
    }
}
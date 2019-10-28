using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HammerController : MonoBehaviour
{
    public GameObject[] tiles;
    [SerializeField]
    private GameObject[] placedTiles;
    [SerializeField]
    private GameObject tileObject;
    [SerializeField]
    private Camera cam;
    Vector3 lastClick;
    [SerializeField]
    LayerMask mask;
    [SerializeField]
    float gridSize;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.ambientIntensity = 8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 v3 = getPosition();
            RaycastHit hit;
            
            if(Physics.Raycast(v3 + (Vector3.up*68), Vector3.down * 68, out hit, 68, mask))
            {
                if (hit.transform.tag != "HammerTile") {
                    placeTile(v3);
                }
            }
            else
            {
                placeTile(v3);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 v3 = getPosition();
            RaycastHit hit;

            if (Physics.Raycast(v3 + (Vector3.up * 68), Vector3.down * 68, out hit,68, mask))
            {
                if (hit.transform.tag == "HammerTile")
                {
                    removeformArray(hit.transform.gameObject);
                    hit.transform.gameObject.SendMessage("Remove");
                    Destroy(hit.transform.gameObject);
                }
            }
        }

    }

    Vector3 getPosition()
    {
        var v3 = Input.mousePosition;
        v3.z = 68;
        v3 = cam.ScreenToWorldPoint(v3);

        v3.x = Mathf.Round(Mathf.Round(v3.x) / gridSize) * gridSize;
        v3.z = Mathf.Round(Mathf.Round(v3.z) / gridSize) * gridSize;
        return v3;
    }


    void placeTile(Vector3 pos)
    {
        GameObject g = Instantiate(tileObject, pos, Quaternion.identity);
        addToArray(g);
        RunCheck();

    }

    public void RunCheck()
    {
        foreach (GameObject g in placedTiles)
        {
           // g.SendMessage("check");
        }
    }
    public void build()
    {
        GameObject master = new GameObject();
        master.name = "hammer Build";
        Instantiate(master, new Vector3(), Quaternion.identity);
        foreach (GameObject g in placedTiles)
        {
            g.SendMessage("spawnObject",master);
        }
    }



    void addToArray(GameObject g)
    {
        System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(placedTiles);
        list.Add(g);
        placedTiles = list.ToArray();
    }

    public void removeformArray(GameObject g)
    {
        System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>(placedTiles);
        list.Remove(g);
        placedTiles = list.ToArray();
    }
}

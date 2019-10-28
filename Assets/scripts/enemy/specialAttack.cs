using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class specialAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject[] toStart;
    [SerializeField]
    private bool onebyeone;
    bool spawning;
    [SerializeField]
    float delayTime = 0.1f;
    bool cankill;
    // Start is called before the first frame update
    void Start()
    {

        if (onebyeone)
        {
            StartCoroutine(Delay());
        }
        else
        {
            Instant();
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        if (cankill)
        {
            int count = 0;

            Transform[] children = transform.GetComponentsInChildren<Transform>();
            int count2 = children.Length;
            foreach (Transform child in children)
            {
                if (child.parent == transform)
                    count++;
            }
            if (count == children.Length)
            {
                Destroy(gameObject);
            }
        }
    }

    void Instant()
    {
        
        foreach (GameObject g in toStart)
        {
            if(g != null)
                g.SetActive(true);
           
        }
       
        cankill = true;
    }

    IEnumerator Delay()
    {
        foreach (GameObject g in toStart)
        {
            yield return new WaitForSeconds(delayTime);
            if(g != null)
                g.SetActive(true);
        }
        cankill = true;
        //yield return null;
    }


    IEnumerator DelayCo(GameObject g)
    {
        yield return new WaitForSeconds(0);
       
    }

}

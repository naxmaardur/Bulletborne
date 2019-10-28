using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    [SerializeField]
    GameObject enemies;
    GameObject copy;
    // Start is called before the first frame update
    void Start()
    {
        if (!enemies)
            enemies = GameObject.FindGameObjectWithTag("Enemies");
        enemies.SetActive(false);
        copy = Instantiate(enemies, Vector3.zero, Quaternion.identity);
        copy.SetActive(true);
    }

    public void Respawn()
    {
        Destroy(copy);
        copy = Instantiate(enemies, Vector3.zero, Quaternion.identity);
        copy.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cheatsController : MonoBehaviour
{
    [SerializeField]
    GameObject cheatMenu;
    bool hammerOn;
    [SerializeField]
    private GameObject hammer;
    [SerializeField]
    InputField input;
    bool active;
    bool light;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            Cursor.lockState = active?CursorLockMode.Locked:CursorLockMode.None;
            Cursor.visible = active?false:true;
            cheatMenu.SetActive(active?false:true);
            active = !active;
        }
    }



    public void GetInput()
    {
        Debug.Log("cheat");
        Debug.Log(input.text);
        switch (input.text)
        {
            case "hammer":
                Hammer();
                break;
            case "hammer build":
                Build();
                break;
            case "light":
                Light();
                break;
        }

    }
    void Light()
    {
        light = !light;
        RenderSettings.ambientIntensity = light?8:0.2f;
    }

    void Build()
    {
        GameObject.FindGameObjectWithTag("Hammer").SendMessage("build");
    }

    void Hammer()
    {
        if (!hammerOn)
        {
            Instantiate(hammer, Vector3.zero, Quaternion.identity);
            GameObject.FindGameObjectWithTag("Player").SetActive(false);
        }
    }
}

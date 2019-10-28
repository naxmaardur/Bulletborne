using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[RequireComponent(typeof(PlayerController))]
public class InputController : MonoBehaviour {

	PlayerController player;
	bool JumpDown;
	bool JumpWasDown;
	bool Fire1Down;
	bool Fire1WasDown;
	bool Fire2Down;
	bool Fire2WasDown;
	bool Fire3Down;
	bool Fire3WasDown;
    float x;
    float y;

    private void Awake()
    {
        LoadSettings();
    }

    // Use this for initialization
    void Start () {
		player = GetComponent<PlayerController> ();
	}

    public void LoadSettings()
    {
        GameSettings settings = new GameSettings();
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json") == true)
        {
            settings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
            
        }
        StaticMaster.settings = settings;
    }

    // Update is called once per frame
    void Update () {
        x = Input.GetKey(StaticMaster.settings.input.right) ? 1 : Input.GetKey(StaticMaster.settings.input.left)?-1:0;
        y = Input.GetKey(StaticMaster.settings.input.forward) ? 1 : Input.GetKey(StaticMaster.settings.input.backward) ? -1 : 0;
        //Directional input
        Vector3 input = new Vector3 (x,0,y);
		player.SetDiractionalInput (input);



		//jump Input detection
		
		
		
		

		//change state Input
		

		//special move Input
		if (Input.GetKeyDown(StaticMaster.settings.input.run)) {
			Fire2Down = true;
		}
		if (Fire2Down && !Fire2WasDown) {
			Fire2WasDown = true;
			player.Dash();
		}
		if (Input.GetKeyUp(StaticMaster.settings.input.run)) {
			Fire2Down = false;
		}
		if (!Fire2Down && Fire2WasDown) {
			Fire2WasDown = false;

		}

		//attack Input
		if (Input.GetKeyDown(StaticMaster.settings.input.interact)) {
			Fire1Down = true;
		}
		if (Fire1Down && !Fire1WasDown) {
			Fire1WasDown = true;
            player.TrythrowAxe();
		}
		if (Input.GetKeyUp(StaticMaster.settings.input.interact)) {
			Fire1Down = false;
		}
		if (!Fire1Down && Fire1WasDown) {
			Fire1WasDown = false;
		
		}

	}
}

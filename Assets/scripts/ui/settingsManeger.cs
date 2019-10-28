using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.IO;

public class settingsManeger : MonoBehaviour
{
    [SerializeField]
    private GameSettings gameSettings;

    [SerializeField]
    private Animator anim;

    [Header("setting Tabs")]
    [SerializeField]
    private Canvas video;
    [SerializeField]
    private Canvas audioCanvas;
    [SerializeField]
    private Canvas input;
    [SerializeField]
    private Canvas defaultC;
    [SerializeField]
    private Canvas applyCanvas;
    [SerializeField]
    private Canvas mainMenu;
    [SerializeField]
    private Canvas thisCanvas;

    [Header("video")]
    [SerializeField]
    private Toggle fullscreenToggle;
    [SerializeField]
    private Dropdown resolutionDropdown;
    [SerializeField]
    private Dropdown textureDropdown;
    [SerializeField]
    private Dropdown antialiasingDropdown;
    [SerializeField]
    private Dropdown vSyncDropdown;
    [SerializeField]
    private Slider gammaSilder;
    private Resolution[] resolutions;

    [Header("audio")]
    [SerializeField]
    private Slider master;
    [SerializeField]
    private Slider environment;
    [SerializeField]
    private Slider voice;
    [SerializeField]
    private Slider music;
    [SerializeField]
    private Slider sound;
    [Header("audio mixers")]
    [SerializeField]
    private AudioMixer masterMixer;
    [SerializeField]
    private AudioMixer environmentMixer;
    [SerializeField]
    private AudioMixer voiceMixer;
    [SerializeField]
    private AudioMixer musicMixer;
    [SerializeField]
    private AudioMixer soundMixer;

    [Header("input")]
    [SerializeField]
    private Slider sensitivity;
    [SerializeField]
    private Toggle inverseY;
    [SerializeField]
    private Toggle inverseX;
    [SerializeField]
    private Button forward;
    [SerializeField]
    private Button backward;
    [SerializeField]
    private Button left;
    [SerializeField]
    private Button right;

    [SerializeField]
    private Button jump;

    [SerializeField]
    private Button run;
    [SerializeField]
    private Button crouch;

    [SerializeField]
    private Button lightButton;

    [SerializeField]
    private Button menu;
    [SerializeField]
    private Button interact;

    // Start is called before the first frame update
    void Start()
    {
        gameSettings = new GameSettings();
        StaticMaster.settings = gameSettings;
        //video AddListener
        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        textureDropdown.onValueChanged.AddListener(delegate { OnTextureChange(); });
        antialiasingDropdown.onValueChanged.AddListener(delegate { OnAntialiaingChange(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { OnVSyncChange(); });
        gammaSilder.onValueChanged.AddListener(delegate { OnGammaChanged(); });


        //audio AddListener
        master.onValueChanged.AddListener(delegate { OnMasterChanged(); });
        music.onValueChanged.AddListener(delegate { OnMusicChanged(); });
        environment.onValueChanged.AddListener(delegate { OnEnvironmentChanged(); });
        voice.onValueChanged.AddListener(delegate { OnVoiceChanged(); });
        sound.onValueChanged.AddListener(delegate { OnSoundChanged(); });

        //input AddListener
        sensitivity.onValueChanged.AddListener(delegate { OnSensitivityChanged(); });
        inverseY.onValueChanged.AddListener(delegate { OnInverseYChanged(); });
        inverseX.onValueChanged.AddListener(delegate { OnInverseXChanged(); });
        forward.onClick.AddListener(delegate { OnForwardPressed(); });
       backward.onClick.AddListener(delegate { OnBackWardsPressed(); });
        left.onClick.AddListener(delegate { OnLeftPressed(); });
        right.onClick.AddListener(delegate { OnRightPressed(); });

        jump.onClick.AddListener(delegate { OnJumpPressed(); });

        run.onClick.AddListener(delegate { OnRunPressed(); });
        crouch.onClick.AddListener(delegate { OnCrouchPressed(); });

        lightButton.onClick.AddListener(delegate { OnLightPressed(); });

        menu.onClick.AddListener(delegate { OnMenuPressed(); });
        interact.onClick.AddListener(delegate { OnInteractPressed(); });



        //getting screen resolutions
        resolutions = Screen.resolutions;
        Resolution current = Screen.currentResolution;
        int i = -1;
        //adding screen resolutions to dropdown
        foreach(Resolution resolution in resolutions)
        {
            i++;
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
            if(resolution.ToString() == current.ToString())
            {
                resolutionDropdown.value = i;
            }
        }
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json") == true)
        {
            LoadSettings();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //video triggers
    public void OnFullscreenToggle()
    {
       gameSettings.fullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, gameSettings.fullscreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }
    public void OnTextureChange()
    {
        gameSettings.textureQuality = QualitySettings.masterTextureLimit = textureDropdown.value;
    }
    public void OnAntialiaingChange()
    {
        int value;
        QualitySettings.antiAliasing = (value = (int)Mathf.Pow(2f, antialiasingDropdown.value)) == 6?8:value;
        gameSettings.antialiasing = antialiasingDropdown.value;
    }
    public void OnVSyncChange()
    {
        gameSettings.vSync = QualitySettings.vSyncCount = vSyncDropdown.value;
    }
    public void OnGammaChanged()
    {
        RenderSettings.ambientLight = new Color(gammaSilder.value, gammaSilder.value, gammaSilder.value, 1.0f);

    }
    //audio triggers
    public void OnMasterChanged()
    {
        masterMixer.SetFloat("volume", master.value);
        gameSettings.master = master.value;
    }
    public void OnMusicChanged()
    {
        musicMixer.SetFloat("volume", music.value);
        gameSettings.music = music.value;
    }
    public void OnVoiceChanged()
    {
        voiceMixer.SetFloat("volume", voice.value);
        gameSettings.voice = voice.value;
    }
    public void OnEnvironmentChanged()
    {
        environmentMixer.SetFloat("volume", environment.value);
        gameSettings.environment = environment.value;
    }
    public void OnSoundChanged()
    {
        soundMixer.SetFloat("volume", sound.value);
        gameSettings.sound = sound.value;
    }


    //Input Triggers
    public void OnSensitivityChanged()
    {
        gameSettings.input.sensitivity = sensitivity.value;
    }
    public void OnInverseYChanged()
    {
        gameSettings.input.inverseY = inverseY.isOn;
    }
    public void OnInverseXChanged()
    {
        gameSettings.input.inverseX = inverseX.isOn;
    }
    public void OnForwardPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.forward = result,forward));
        
    }
    public void OnBackWardsPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.backward = result, backward));
    }
    public void OnLeftPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.left = result, left));
        
    }
    public void OnRightPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.right = result, right));
      
    }
    public void OnJumpPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.jump = result, jump));
        
    }
    public void OnRunPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.run = result, run));
     
    }
    public void OnCrouchPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.crouch = result, crouch));
        
    }
    public void OnLightPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.light = result, lightButton));
        
    }
    public void OnMenuPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.menu = result, menu));
        
    }
    public void OnInteractPressed()
    {
        StartCoroutine(waitforInput(result => gameSettings.input.interact = result, interact));
        
    }

    public void setButtonString(Button b, KeyCode k)
    {
        b.transform.GetChild(0).GetComponent<Text>().text = "" + k;
    }


    public void saveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gameSettings.json", jsonData);
    }

    public void LoadSettings()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesettings.json") == true)
        {
            gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gameSettings.json"));
        }
        StaticMaster.settings = gameSettings;
        fullscreenToggle.isOn = gameSettings.fullscreen;
        resolutionDropdown.value = gameSettings.resolutionIndex;
        textureDropdown.value = gameSettings.textureQuality;
        antialiasingDropdown.value = gameSettings.antialiasing;
        vSyncDropdown.value = gameSettings.vSync;
        gammaSilder.value = gameSettings.gamma;
        resolutionDropdown.RefreshShownValue();

        //audio
        master.value = gameSettings.master;
        music.value = gameSettings.music;
       // environment.value = gameSettings.environment;
       // voice.value = gameSettings.voice;
        sound.value = gameSettings.sound;

        //input
        sensitivity.value = gameSettings.input.sensitivity;
        inverseY.isOn = gameSettings.input.inverseY;
        inverseX.isOn = gameSettings.input.inverseX;
        setButtonString(forward, gameSettings.input.forward);
        setButtonString(backward, gameSettings.input.backward);
        setButtonString(left, gameSettings.input.left);
        setButtonString(right, gameSettings.input.right);

        setButtonString(jump, gameSettings.input.jump);

        setButtonString(run, gameSettings.input.run);
        setButtonString(crouch, gameSettings.input.crouch);

        setButtonString(lightButton, gameSettings.input.light);

        setButtonString(menu, gameSettings.input.menu);
        setButtonString(interact, gameSettings.input.interact);
    }

    public void back()
    {
        StartCoroutine(backC());
    }
    public void toSettings()
    {
        StartCoroutine(toSettingsC());
    }

    public void showVideo()
    {
        StartCoroutine(showVideoC());
    }

    public void showAudio()
    {
        StartCoroutine(showAudioC());
    }
    public void showInput()
    {
        StartCoroutine(showInputC());
    }
    public void showmenu()
    {
        StartCoroutine(showmenuC());
    }
    public IEnumerator waitforInput(System.Action<KeyCode> k,Button b)
    {
            bool done = false;
            while (!done)
            {
                if (Input.anyKeyDown)
                {
                foreach (KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKey(kcode))
                    {
                        setButtonString(b, kcode);
                        k(kcode);
                    }
                }
                done = true; 
                }
            yield return new WaitForSeconds(0);
        }
    }
    void AnimNext()
    {
        if (anim)
        {
            anim.SetTrigger("turnPage");
        }
    }
    void AnimBack()
    {
        if (anim)
        {
            anim.SetTrigger("BackPage");
        }
    }

   IEnumerator backC()
    {
        AnimBack();
        LoadSettings();
        video.enabled = false;
        audioCanvas.enabled = false;
        input.enabled = false;
        applyCanvas.enabled = false;
        mainMenu.enabled = false;
        yield return new WaitForSeconds(0);
        defaultC.enabled = true;
        thisCanvas.enabled = true;
    }

    IEnumerator toSettingsC()
    {
        AnimNext();
        LoadSettings();
        video.enabled = false;
        audioCanvas.enabled = false;
        input.enabled = false;
        applyCanvas.enabled = false;
        mainMenu.enabled = false;
        yield return new WaitForSeconds(0);
        defaultC.enabled = true;
        thisCanvas.enabled = true;
    }
    IEnumerator showVideoC()
    {
        AnimNext();
        defaultC.enabled = false;
        yield return new WaitForSeconds(0);
        video.enabled = true;
        applyCanvas.enabled = true;
    }
    IEnumerator showAudioC()
    {
        AnimNext();
        defaultC.enabled = false;
        yield return new WaitForSeconds(0);
        audioCanvas.enabled = true;
        applyCanvas.enabled = true;
    }
    IEnumerator showInputC()
    {
        AnimNext();
        defaultC.enabled = false;
        yield return new WaitForSeconds(0);
        input.enabled = true;
        applyCanvas.enabled = true;
        
    }
    IEnumerator showmenuC()
    {
        AnimBack();
        video.enabled = false;
        audioCanvas.enabled = false;
        input.enabled = false;
        defaultC.enabled = false;
        applyCanvas.enabled = false;
        thisCanvas.enabled = false;
        yield return new WaitForSeconds(0);
        mainMenu.enabled = true;
    }
}


[System.Serializable]
public class GameSettings
{
    //video
    public bool fullscreen;
    public int textureQuality;
    public int antialiasing;
    public int vSync;
    public int resolutionIndex;
    public float gamma;

    //audio
    public float master;
    public float environment;
    public float voice;
    public float music;
    public float sound;

    //input
    public InputSettings input;

    public GameSettings()
    {
        input = new InputSettings();
        fullscreen = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class InputSettings
{
    public KeyCode forward;
    public KeyCode backward;
    public KeyCode left;
    public KeyCode right;

    public KeyCode jump;

    public KeyCode run;
    public KeyCode crouch;

    public KeyCode light;

    public KeyCode menu;

    public KeyCode interact;

    public float sensitivity;
    public bool inverseY;
    public bool inverseX;

    public  InputSettings()
    {
        sensitivity = 3;
        forward = KeyCode.W;
        backward = KeyCode.S;
        left = KeyCode.A;
        right = KeyCode.D;

        jump = KeyCode.Space;

        run = KeyCode.LeftShift;
        crouch = KeyCode.LeftControl;

        light = KeyCode.F;

        menu = KeyCode.Escape;

        interact = KeyCode.Mouse0;
    }
}

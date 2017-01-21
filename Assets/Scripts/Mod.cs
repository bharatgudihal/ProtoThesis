﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Controls
{
    public static string Up = "Up";
    public static string Down = "Down";
    public static string Left = "Left";
    public static string Right = "Right";
}

public enum ModSpot
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3
}

public abstract class Mod : MonoBehaviour
{

    public ModSpot myModSpot;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButton(Controls.Up) && myModSpot == ModSpot.Up) ||
            (Input.GetButton(Controls.Down) && myModSpot == ModSpot.Down) ||
            (Input.GetButton(Controls.Left) && myModSpot == ModSpot.Left) ||
            (Input.GetButton(Controls.Right) && myModSpot == ModSpot.Right))
        {

            Activate();
        }
    }

    public abstract void Activate();
}
using System.Collections;
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
    public bool isAttached;
    [SerializeField] protected JoystickMovement joystickMovement;

    public float health = 100f;

    public bool isEnabled;

    private void Awake()
    {

    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled)
        {
            if (!Input.GetButton("RightBumper") && ((Input.GetButton(Controls.Up) && myModSpot == ModSpot.Up) ||
                (Input.GetButton(Controls.Down) && myModSpot == ModSpot.Down) ||
                (Input.GetButton(Controls.Left) && myModSpot == ModSpot.Left) ||
                (Input.GetButton(Controls.Right) && myModSpot == ModSpot.Right)))
            {

                Activate();
                Fatigue();
                if (health <= 0)
                {
                    Dettach();
                }
            }
            if ((Input.GetButtonUp(Controls.Up) && myModSpot == ModSpot.Up) ||
                (Input.GetButtonUp(Controls.Down) && myModSpot == ModSpot.Down) ||
                (Input.GetButtonUp(Controls.Left) && myModSpot == ModSpot.Left) ||
                (Input.GetButtonUp(Controls.Right) && myModSpot == ModSpot.Right))
            {
                DeActivate();
            }
        }
    }

	public virtual void Activate(){
		isAttached = true;
	}

	public virtual void DeActivate(){
		isAttached = false;
	}

    public abstract void Fatigue();

    public virtual void Attach(JoystickMovement joystickMovement) {
        this.joystickMovement = joystickMovement;
        isAttached = true;
    }

    public virtual void Dettach() {
        joystickMovement = null;
        isAttached = false;
    }
}
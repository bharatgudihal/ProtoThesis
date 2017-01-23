using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles the Mod Interface. 
/// 
/// </summary>
/// 
public enum ModTypes
{
    NONE,
    GRENADE_LAUNCHER = 3,
    MASHOT_GUN = 1,
    SWORD = 4,
    JET_ENGINE = 7,
    SHIELD = 5,
    X_RAY = 11,
    ROCKET_LAUNCHER = 10
}

public class InterfaceManager : MonoBehaviour {

    [SerializeField]
    Slider mod1FatigueCounter, mod2FatigueCounter, mod3FatigueCounter, mod4FatigueCounter;

    [SerializeField]
    Slider mod1CooldownSlider, mod2CooldownSlider, mod3CooldownSlider, mod4CooldownSlider;

    [SerializeField]
    Button mod1Button, mod2Button, mod3Button, mod4Button;

    [SerializeField]
    Image mod1Image, mod2Image, mod3Image, mod4Image;

    [SerializeField]
    Sprite noSpriteImage;

    //0 = ready, 1 = using, 2 = empty
    [SerializeField]
    Sprite[] grenadeIcons = new Sprite[3];
    [SerializeField]
    Sprite[] mashotgunIcons = new Sprite[3];
    [SerializeField]
    Sprite[] swordIcons = new Sprite[3];
    [SerializeField]
    Sprite[] jetIcons = new Sprite[3];
    [SerializeField]
    Sprite[] shieldIcons = new Sprite[3];
    [SerializeField]
    Sprite[] xRayIcons = new Sprite[3];
    [SerializeField]
    Sprite[] rocketLauncherIcons = new Sprite[3];

    [SerializeField]
    Slider playerHealthSlider;

    float mod1CooldownTimer, mod2CooldownTimer, mod3CooldownTimer, mod4CooldownTimer; //timers for cooldowns

    float modCooldownTime = 1f; //what is the cooldown time for a mod?

    [HideInInspector]
    public bool mod1Available = true; //Is mod1 available? - Down
    [HideInInspector]
    public bool mod2Available = true; //Is mod2 available? - Left
    [HideInInspector]
    public bool mod3Available = true; //Is mod3 available? - Right
    [HideInInspector]
    public bool mod4Available = true; //Is mod4 available? - Up

  
    //TODO: This shouldn't be set upon object instantiation, but rather checked against
    //a back end keeping track of which mods are set in which slots
    ModTypes slot1Type = ModTypes.NONE;
    ModTypes slot2Type = ModTypes.NONE;
    ModTypes slot3Type = ModTypes.NONE;
    ModTypes slot4Type = ModTypes.NONE;

    #region Singleton

    private static InterfaceManager _instance;

    public static InterfaceManager instance
    {
        get
        {
            return _instance;
        }
    }

    #endregion

    #region fatigueCounters

    /// <summary>
    /// For designers
    /// </summary>
    [SerializeField]
    int grenadeLauncherFatigueUses = 7;
    [SerializeField]
    int mashotGunFatigueUses = 15;
    [SerializeField]
    int swordFatigueUses = 4;
    [SerializeField]
    int jetFatigueUses = 7;
    [SerializeField]
    int shieldFatigueUses = 5;
    [SerializeField]
    int xRayFatigueUses = 11;
    [SerializeField]
    int rocketLauncherFatigueUses = 10;

    /// <summary>
    /// private references
    /// </summary>
    int mod1Counter = 999;
    int mod2Counter = 999;
    int mod3Counter = 999;
    int mod4Counter = 999;

    //for calculating percentage usage to show on sliders
    int masterMod1Counter = 999;
    int masterMod2Counter = 999;
    int masterMod3Counter = 999;
    int masterMod4Counter = 999;

    #endregion

    private void Awake()
    {

        //set up singleton
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        SetModTypeInSlot(ModSpot.Down, ModTypes.NONE);
        SetModTypeInSlot(ModSpot.Left, ModTypes.NONE);
        SetModTypeInSlot(ModSpot.Right, ModTypes.NONE);
        SetModTypeInSlot(ModSpot.Up, ModTypes.NONE);
        //Set initial state of counters
        UpdateCounterAndSprites(ModSpot.Down);
        UpdateCounterAndSprites(ModSpot.Left);
        UpdateCounterAndSprites(ModSpot.Right);
        UpdateCounterAndSprites(ModSpot.Up);
        mod1FatigueCounter.value = masterMod1Counter;
        mod2FatigueCounter.value = masterMod2Counter;
        mod3FatigueCounter.value = masterMod3Counter;
        mod4FatigueCounter.value = masterMod4Counter;

    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
        //TODO: This sorts of calls should go in the appropriate control manager
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    FireModOnInterface(ModSpot.Down);
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    FireModOnInterface(ModSpot.Left);
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    FireModOnInterface(ModSpot.Right);
        //}
        //if(Input.GetKeyDown(KeyCode.Alpha4))
        //{
        //    FireModOnInterface(ModSpot.Up);
        //}

    }

    private void FixedUpdate()
    {
        #region CooldownTimers
        if (!mod1Available)
        {
            mod1CooldownTimer -= Time.deltaTime;

            mod1CooldownSlider.value = 1 - (mod1CooldownTimer / modCooldownTime);
            if(mod1CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Down);
            }
        }

        if (!mod2Available)
        {
            mod2CooldownTimer -= Time.deltaTime;
            mod2CooldownSlider.value = 1 - (mod2CooldownTimer / modCooldownTime);
            if (mod2CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Left);
            }
        }

        if (!mod3Available)
        {
            mod3CooldownTimer -= Time.deltaTime;
            mod3CooldownSlider.value = 1 - (mod3CooldownTimer / modCooldownTime);
            if (mod3CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Right);
            }
        }

        if (!mod4Available)
        {
            mod4CooldownTimer -= Time.deltaTime;
            mod4CooldownSlider.value = 1 - (mod4CooldownTimer / modCooldownTime);
            if (mod4CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Up);
            }
        }
        #endregion
    }

    /// <summary>
    /// Activates the mod on the mod interface, starts the cooldown timers
    /// sets the appropriate graphics.
    /// </summary>
    /// <param name="i_toActivate">The modspot to reset</param>
   public void FireModOnInterface(ModSpot i_toActivate)
    {
        switch (i_toActivate)
        {
            case ModSpot.Down:
                if (mod1Counter <= 0 || !mod1Available)
                    return;
                //mod1Available = false;
                //mod1CooldownTimer = modCooldownTime;
                //mod1Counter--;
                //mod1AmmoSlider.value = mod1Counter / (float)masterMod1Counter;
                break;
            case ModSpot.Left:
                if (mod2Counter <= 0 || !mod2Available)
                    return;
                //mod2Available = false;
                //mod2CooldownTimer = modCooldownTime;
                //mod2Counter--;
                //mod2AmmoSlider.value = mod2Counter / (float)masterMod2Counter;
                break;
            case ModSpot.Right:
                if (mod3Counter <= 0 || !mod3Available)
                    return;
                //mod3Available = false;
                //mod3CooldownTimer = modCooldownTime;
                //mod3Counter--;
                //mod3AmmoSlider.value = mod3Counter / (float)masterMod3Counter;
                break;
            case ModSpot.Up:
                if (mod4Counter <= 0 || !mod4Available)
                    return;
                //mod4Available = false;
                //mod4CooldownTimer = modCooldownTime;
                //mod4Counter--;
                //mod4AmmoSlider.value = mod4Counter / (float)masterMod4Counter;
                break;

        }

        SetCooldownSprite(i_toActivate);


    }

 
    /// <summary>
    /// Resets the mod on the mod interface. Used in the cooldown timers. Not for
    /// outside use.
    /// </summary>
    /// <param name="i_toReset">The modspot to reset</param>
    void ResetMod(ModSpot i_toReset)
    {
        switch (i_toReset)
        {
            case ModSpot.Down:
                mod1Available = true;
                mod1CooldownSlider.value = 1f;
                break;
            case ModSpot.Left:
                mod2Available = true;
                mod2CooldownSlider.value = 1f;
                break;
            case ModSpot.Right:
                mod3Available = true;
                mod3CooldownSlider.value = 1f;
                break;
            case ModSpot.Up:
                mod4Available = true;
                mod4CooldownSlider.value = 1f;
                break;

        }

        SetInitialReadySlotGraphic(i_toReset);
        UpdateCounterAndSprites(i_toReset);
    }

    /// <summary>
    /// Set the health bar, for outside use. Note, no death checks performed here.
    /// </summary>
    /// <param name="i_deltaHealth">The delta change, positive or negative</param>
    public void SetHealth(float i_deltaHealth)
    {
        playerHealthSlider.value += i_deltaHealth;
    }

    /// <summary>
    /// Updates the uses text and sprites for the appropriate mods
    /// in the mod interface
    /// </summary>
    /// <param name="i_toUpdate">modspot to update</param>
    void UpdateCounterAndSprites(ModSpot i_toUpdate)
    {
        switch (i_toUpdate)
        {
            case ModSpot.Down:
                
                if (mod1Counter == 0 && slot1Type != ModTypes.NONE)
                {
                    SetUnavailableSprite(i_toUpdate);
                }
                break;
            case ModSpot.Left:
                if (mod2Counter == 0 && slot2Type != ModTypes.NONE)
                {
                    mod2FatigueCounter.value = 0;
                    SetUnavailableSprite(i_toUpdate);
                }
                break;
            case ModSpot.Right:
                if (mod3Counter == 0 && slot3Type != ModTypes.NONE)
                {
                    mod3FatigueCounter.value = 0;
                    SetUnavailableSprite(i_toUpdate);
                }
                break;
            case ModSpot.Up:
                if (mod4Counter == 0 && slot4Type != ModTypes.NONE)
                {
                    mod4FatigueCounter.value = 0;
                    SetUnavailableSprite(i_toUpdate);
                }
                break;
        }

    }


    public void SetModTypeInSlot(ModSpot i_toSet, ModTypes i_type)
    {
        switch (i_toSet)
        {
            case ModSpot.Down:
                slot1Type = i_type;
                break;
            case ModSpot.Left:
                slot2Type = i_type;
                break;
            case ModSpot.Right:
                slot3Type = i_type;
                break;
            case ModSpot.Up:
                slot4Type = i_type;
                break;
        }

        SetInitialReadySlotGraphic(i_toSet);
        SetModAmmoCounter(i_toSet);
    }

    void SetInitialReadySlotGraphic(ModSpot i_toSet)
    {
        ModTypes typeToSet = ModTypes.NONE;
        Sprite spriteToSet = null;

        switch (i_toSet)
        {
            case ModSpot.Down:
                typeToSet = slot1Type;
                break;
            case ModSpot.Left:
                typeToSet = slot2Type;
                break;
            case ModSpot.Right:
                typeToSet = slot3Type;
                break;
            case ModSpot.Up:
                typeToSet = slot4Type;
                break;
        }

        switch (typeToSet)
        {
            case ModTypes.GRENADE_LAUNCHER:
                spriteToSet = grenadeIcons[0];
                break;
            case ModTypes.MASHOT_GUN:
                spriteToSet = mashotgunIcons[0];
                break;
            case ModTypes.SWORD:
                spriteToSet = swordIcons[0];
                break;
            case ModTypes.JET_ENGINE:
                spriteToSet = jetIcons[0];
                break;
            case ModTypes.SHIELD:
                spriteToSet = shieldIcons[0];
                break;
            case ModTypes.X_RAY:
                spriteToSet = xRayIcons[0];
                break;
            case ModTypes.ROCKET_LAUNCHER:
                spriteToSet = rocketLauncherIcons[0];
                break;
            case ModTypes.NONE:
                spriteToSet = noSpriteImage;
                break;
        }

        switch (i_toSet)
        {
            case ModSpot.Down:
                mod1Image.sprite = spriteToSet;
                break;
            case ModSpot.Left:
                mod2Image.sprite = spriteToSet;
                break;
            case ModSpot.Right:
                mod3Image.sprite = spriteToSet;
                break;
            case ModSpot.Up:
                mod4Image.sprite = spriteToSet;
                break;
        }
    }

    void SetCooldownSprite(ModSpot i_toSet)
    {
        ModTypes typeToSet = ModTypes.NONE;
        Sprite spriteToSet = null;
        switch (i_toSet)
        {
            case ModSpot.Down:
                typeToSet = slot1Type;
                break;
            case ModSpot.Left:
                typeToSet = slot2Type;
                break;
            case ModSpot.Right:
                typeToSet = slot3Type;
                break;
            case ModSpot.Up:
                typeToSet = slot4Type;
                break;
        }

        switch (typeToSet)
        {
            case ModTypes.GRENADE_LAUNCHER:
                spriteToSet = grenadeIcons[1];
                break;
            case ModTypes.MASHOT_GUN:
                spriteToSet = mashotgunIcons[1];
                break;
            case ModTypes.SWORD:
                spriteToSet = swordIcons[1];
                break;
            case ModTypes.JET_ENGINE:
                spriteToSet = jetIcons[1];
                break;
            case ModTypes.SHIELD:
                spriteToSet = shieldIcons[1];
                break;
            case ModTypes.X_RAY:
                spriteToSet = xRayIcons[1];
                break;
            case ModTypes.ROCKET_LAUNCHER:
                spriteToSet = rocketLauncherIcons[1];
                break;
        }

        switch (i_toSet)
        {
            case ModSpot.Down:
                mod1Image.sprite = spriteToSet;
                break;
            case ModSpot.Left:
                mod2Image.sprite = spriteToSet;
                break;
            case ModSpot.Right:
                mod3Image.sprite = spriteToSet;
                break;
            case ModSpot.Up:
                mod4Image.sprite = spriteToSet;
                break;
        }
    }

    void SetUnavailableSprite(ModSpot i_toSet)
    {

        ModTypes typeToSet = ModTypes.NONE;
        Sprite spriteToSet = null;
        switch (i_toSet)
        {
            case ModSpot.Down:
                typeToSet = slot1Type;
                break;
            case ModSpot.Left:
                typeToSet = slot2Type;
                break;
            case ModSpot.Right:
                typeToSet = slot3Type;
                break;
            case ModSpot.Up:
                typeToSet = slot4Type;
                break;
        }

        switch (typeToSet)
        {
            case ModTypes.GRENADE_LAUNCHER:
                spriteToSet = grenadeIcons[2];
                break;
            case ModTypes.MASHOT_GUN:
                spriteToSet = mashotgunIcons[2];
                break;
            case ModTypes.SWORD:
                spriteToSet = swordIcons[2];
                break;
            case ModTypes.JET_ENGINE:
                spriteToSet = jetIcons[2];
                break;
            case ModTypes.SHIELD:
                spriteToSet = shieldIcons[2];
                break;
            case ModTypes.X_RAY:
                spriteToSet = xRayIcons[2];
                break;
            case ModTypes.ROCKET_LAUNCHER:
                spriteToSet = rocketLauncherIcons[2];
                break;
        }

        switch (i_toSet)
        {
            case ModSpot.Down:
                mod1Image.sprite = spriteToSet;
                break;
            case ModSpot.Left:
                mod2Image.sprite = spriteToSet;
                break;
            case ModSpot.Right:
                mod3Image.sprite = spriteToSet;
                break;
            case ModSpot.Up:
                mod4Image.sprite = spriteToSet;
                break;
        }
    }

    void SetModAmmoCounter(ModSpot i_toSet)
    {
        ModTypes typeToSet = ModTypes.NONE;
        int count = 0;

        //which type are we setting
        switch (i_toSet)
        {
            case ModSpot.Down:
                typeToSet = slot1Type;
                break;
            case ModSpot.Left:
                typeToSet = slot2Type;
                break;
            case ModSpot.Right:
                typeToSet = slot3Type;
                break;
            case ModSpot.Up:
                typeToSet = slot4Type;
                break;
        }

        //how much?
        switch (typeToSet)
        {
            case ModTypes.GRENADE_LAUNCHER:
                count = grenadeLauncherFatigueUses;
                break;
            case ModTypes.MASHOT_GUN:
                count =  mashotGunFatigueUses;
                break;
            case ModTypes.SWORD:
                count = swordFatigueUses;
                break;
            case ModTypes.JET_ENGINE:
                count = jetFatigueUses;
                break;
            case ModTypes.SHIELD:
                count = shieldFatigueUses;
                break;
            case ModTypes.X_RAY:
                count = xRayFatigueUses;
                break;
            case ModTypes.ROCKET_LAUNCHER:
                count = rocketLauncherFatigueUses;
                break;
            case ModTypes.NONE:
                count = 0;
                break;

        }

        //set counter
        switch (i_toSet)
        {
            case ModSpot.Down:
                mod1Counter = count;
                masterMod1Counter = count;
                break;
            case ModSpot.Left:
                mod2Counter = count;
                masterMod2Counter = count;
                break;
            case ModSpot.Right:
                mod3Counter = count;
                masterMod3Counter = count;
                break;
            case ModSpot.Up:
                mod4Counter = count;
                masterMod4Counter = count;
                break;
        }





    }

    public void SetFatigue(ModSpot i_spot, float i_newFatigue)
    {
        switch (i_spot)
        {
            case ModSpot.Down:
                mod1FatigueCounter.value = i_newFatigue;
                break;
            case ModSpot.Left:
                mod2FatigueCounter.value = i_newFatigue;
                break;
            case ModSpot.Right:
                mod3FatigueCounter.value = i_newFatigue;
                break;
            case ModSpot.Up:
                mod4FatigueCounter.value = i_newFatigue;

                break;
        }
    }
}

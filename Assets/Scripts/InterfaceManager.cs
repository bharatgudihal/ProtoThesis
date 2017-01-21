using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   
public class InterfaceManager : MonoBehaviour {

    [SerializeField]
    Slider mod1Slider, mod2Slider, mod3Slider, mod4Slider;

    [SerializeField]
    Button mod1Button, mod2Button, mod3Button, mod4Button;

    [SerializeField]
    Image mod1Image, mod2Image, mod3Image, mod4Image;

    [SerializeField]
    Sprite activatedSprite;

    [SerializeField]
    Sprite defaultSprite;

    [SerializeField]
    Sprite usedSprite;

    [SerializeField]
    Text mod1UsesText, mod2UsesText, mod3UsesText, mod4UsesText;

    [SerializeField]
    Slider playerHealthSlider;

    float mod1CooldownTimer, mod2CooldownTimer, mod3CooldownTimer, mod4CooldownTimer;

    float modCooldownTime = 1f;

    bool mod1Available = false;
    bool mod2Available = false;
    bool mod3Available = false;
    bool mod4Available = false;

    [SerializeField]
    int mod1Counter = 3;
    [SerializeField]
    int mod2Counter = 3;
    [SerializeField]
    int mod3Counter = 3;
    [SerializeField]
    int mod4Counter = 3;

    private void Awake()
    {
        UpdateCounter(ModSpot.Down);
        UpdateCounter(ModSpot.Left);
        UpdateCounter(ModSpot.Right);
        UpdateCounter(ModSpot.Up);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Alpha1) && !mod1Available)
        {
            ActivateMod(ModSpot.Down);
        }
        if(Input.GetKeyDown(KeyCode.Alpha2) && !mod2Available)
        {
            ActivateMod(ModSpot.Left);
        }
        if(Input.GetKeyDown(KeyCode.Alpha3) && !mod3Available)
        {
            ActivateMod(ModSpot.Right);
        }
        if(Input.GetKeyDown(KeyCode.Alpha4) && !mod4Available)
        {
            ActivateMod(ModSpot.Up);
        }

    }

    private void FixedUpdate()
    {
        #region timers
        if (mod1Available)
        {
            mod1CooldownTimer -= Time.deltaTime;

            mod1Slider.value = mod1CooldownTimer / modCooldownTime;
            if(mod1CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Down);
            }
        }

        if (mod2Available)
        {
            mod2CooldownTimer -= Time.deltaTime;
            mod2Slider.value = mod2CooldownTimer / modCooldownTime;
            if (mod2CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Left);
            }
        }

        if (mod3Available)
        {
            mod3CooldownTimer -= Time.deltaTime;
            mod3Slider.value = mod3CooldownTimer / modCooldownTime;
            if (mod3CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Right);
            }
        }

        if (mod4Available)
        {
            mod4CooldownTimer -= Time.deltaTime;
            mod4Slider.value = mod4CooldownTimer / modCooldownTime;
            if (mod4CooldownTimer <= 0)
            {
                ResetMod(ModSpot.Up);
            }
        }
        #endregion
    }

    void ActivateMod(ModSpot i_toActivate)
    {
        switch (i_toActivate)
        {
            case ModSpot.Down:
                if (mod1Counter <= 0)
                    return;
                mod1Available = true;
                mod1CooldownTimer = modCooldownTime;
                mod1Image.sprite = activatedSprite;
                mod1Counter--;
                break;
            case ModSpot.Left:
                if (mod2Counter <= 0)
                    return;
                mod2Available = true;
                mod2CooldownTimer = modCooldownTime;
                mod2Image.sprite = activatedSprite;
                mod2Counter--;
                break;
            case ModSpot.Right:
                if (mod3Counter <= 0)
                    return;
                mod3Available = true;
                mod3CooldownTimer = modCooldownTime;
                mod3Image.sprite = activatedSprite;
                mod3Counter--;
                break;
            case ModSpot.Up:
                if (mod4Counter <= 0)
                    return;
                mod4Available = true;
                mod4CooldownTimer = modCooldownTime;
                mod4Image.sprite = activatedSprite;
                mod4Counter--;
                break;

        }

        
    }

    void ResetMod(ModSpot i_toReset)
    {
        switch (i_toReset)
        {
            case ModSpot.Down:
                mod1Available = false;
                mod1Image.sprite = defaultSprite;
                mod1Slider.value = 1f;
                break;
            case ModSpot.Left:
                mod2Available = false;
                mod2Image.sprite = defaultSprite;
                mod2Slider.value = 1f;
                break;
            case ModSpot.Right:
                mod3Available = false;
                mod3Image.sprite = defaultSprite;
                mod3Slider.value = 1f;
                break;
            case ModSpot.Up:
                mod4Available = false;
                mod4Image.sprite = defaultSprite;
                mod4Slider.value = 1f;
                break;

        }

        UpdateCounter(i_toReset);
    }

    void UpdateCounter(ModSpot i_toUpdate)
    {
        switch (i_toUpdate)
        {
            case ModSpot.Down:
                mod1UsesText.text = mod1Counter.ToString();
                if(mod1Counter == 0)
                {
                    mod1Image.sprite = usedSprite;
                }
                break;
            case ModSpot.Left:
                mod2UsesText.text = mod2Counter.ToString();
                if (mod2Counter == 0)
                {
                    mod2Image.sprite = usedSprite;
                }
                break;
            case ModSpot.Right:
                mod3UsesText.text = mod3Counter.ToString();
                if (mod3Counter == 0)
                {
                    mod3Image.sprite = usedSprite;
                }
                break;
            case ModSpot.Up:
                mod4UsesText.text = mod4Counter.ToString();
                if (mod4Counter == 0)
                {
                    mod4Image.sprite = usedSprite;
                }
                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    public Sprite[] Sound, Vibrate;
    public Button SoundButton,VibrateButton;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            VibrateOn();
        }
        else
        {
            VibrateOff();
        }

        if (PlayerPrefs.GetInt("Sound") == 0)
        {
            SoundOn();
        }
        else
        {
            SoundOff();
        }
    }

   public void  SetSound()
    {
        if (SoundButton.image.sprite==Sound[1])
        {
            PlayerPrefs.SetInt("Sound", 0);
            SoundOn();
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            SoundOff();
        }
    }
  public  void SetVibrate()
    {
        if (VibrateButton.image.sprite==Vibrate[1])
        {
            PlayerPrefs.SetInt("Vibrate", 0);
            VibrateOn();
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            VibrateOff();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     void SoundOn()
    {
        SoundButton.image.sprite = Sound[0];
        GlobalValues.Sound = 0;
    }
     void SoundOff()
    {
        SoundButton.image.sprite = Sound[1];
        GlobalValues.Sound = 1;
    }
     void VibrateOn()
    {
        VibrateButton.image.sprite = Vibrate[0];
        GlobalValues.Vibrate = 0;
    }
     void VibrateOff()
    {
        VibrateButton.image.sprite = Vibrate[1];
        GlobalValues.Vibrate = 1;
    }
}

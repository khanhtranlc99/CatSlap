using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalValues : MonoBehaviour
{
    // Start is called before the first frame update
    public static int MinPower;
    public static int MaxPower,Sound,Vibrate;
    public static int Power, Coins;
    public static int Level=1;
    public static int PlayerHealth,EnemyHealth;
    public static float PowerPercentage;
    public static GlobalValues current;
    public static bool isBonus = false;
    public static int TurnLeft = 3;
    public static bool OneSlap = false;

    void Awake()
    {
      // PlayerPrefs.SetInt("Level", 50);
            OneSlap = false;
        if ((float)Level % 4 == 0)
        {
            TurnLeft = 3;
            isBonus = true;
        }
        else
        {
            isBonus = false;
        }
        if (!PlayerPrefs.HasKey("Retry"))
        {
            PlayerPrefs.SetString("Retry", "Null");
           
        }
        if (!PlayerPrefs.HasKey("RetryScene"))
        {
            PlayerPrefs.SetString("RetryScene", "Null");
        }
        // PlayerPrefs.SetInt("Coins", 1000);
        current = this;
        EnemyHealth=  GetEnemyHealth();
        PlayerHealth = GetPlayerHealth();
        Level = LevelNo();
        Coins = GetCoins();
        MinPower = GetMinPower();
        MaxPower = GetMaxPower();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCoins(int coin)
    {
        PlayerPrefs.SetInt("Coins", coin);
    }

    public int GetEnemyHealth()
    {
       int EnemyHealth= 75 + (PlayerPrefs.GetInt("Level", 1)*25)+2;
        if ((float)Level%4==0)
        {
            EnemyHealth -= 62;
        }
        return EnemyHealth;
    }  
    
    public int GetPlayerHealth()
    {
      int  PlayerHealth= PlayerPrefs.GetInt("Health", 100);
        return PlayerHealth;
    }

    public int LevelNo()
    {
     int   Level= PlayerPrefs.GetInt("Level", 1);
        return Level;
    }  
    public int GetCoins()
    {
     int   Coins= PlayerPrefs.GetInt("Coins", 0);
        return Coins;
    }
    
    public int GetMinPower()
    {
     int   minpower= PlayerPrefs.GetInt("MinPower", 17);
        return minpower;
    }
    public int GetMaxPower()
    {
     int   maxpower= PlayerPrefs.GetInt("MaxPower", 35);
        return maxpower;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text PlayerHealth, EnemyHealth, CoinsText, LevelNo,BonusText,VsText, IncreaseHealthText, IncreasePowerText, IncHealthText, IncPowerText;
    public Text CompleteCoinText, FailCoinText, CompleteRewardText, FailRewardText;
    public GameObject CompletePanel, FailPanel,PowerIncButton,HealthIncButton,SettingPanel,SlapPanel;
    public Slider PlayerHealthSlider, EnemyHealthSlider,PlayerAnimSlider,EnemyAnimSlider;
    public Image EnemyIcon;
    int healthvalue, powervalue;
    public Text PlayerTextAnim, EnemyTextAnim;
    bool OneTime = false;
    public ParticleSystem WeakParticlePlayer, StrongParticlePlayer, particleEnemy;
    public Sprite SlapDisable;
    public Image[] SlapIcons;

    

    
    void Start()
    {
        
        PlayerHealthSlider.minValue = 0;
        PlayerHealthSlider.maxValue = GlobalValues.PlayerHealth;
        PlayerHealthSlider.value = GlobalValues.PlayerHealth;
        EnemyHealthSlider.minValue = 0;
        EnemyHealthSlider.maxValue = GlobalValues.EnemyHealth;
        EnemyHealthSlider.value = GlobalValues.EnemyHealth;    
        PlayerAnimSlider.minValue = 0;
        PlayerAnimSlider.maxValue = GlobalValues.PlayerHealth;
        PlayerAnimSlider.value = GlobalValues.PlayerHealth;
        EnemyAnimSlider.minValue = 0;
        EnemyAnimSlider.maxValue = GlobalValues.EnemyHealth;
        EnemyAnimSlider.value = GlobalValues.EnemyHealth;

        CoinsText.text = "" + GlobalValues.Coins;
        PlayerHealth.text = "" + GlobalValues.PlayerHealth;
        EnemyHealth.text = "" + GlobalValues.EnemyHealth;
        /*if ((float)GlobalValues.Level % 4 == 0)
        {
            BonusText.gameObject.SetActive(true);
            LevelNo.gameObject.SetActive(false);
            VsText.gameObject.SetActive(false);
        }
        else
        {*/
        LevelNo.text = "LEVEL" + GlobalValues.Level;
      //  }
        IncreaseHealthText.text = "HEALTH(" + PlayerPrefs.GetInt("IncHealth", 1) + ")";
        IncreasePowerText.text = "POWER(" + PlayerPrefs.GetInt("IncPower", 1) + ")";
        healthvalue = PlayerPrefs.GetInt("IncHealth", 1) * 50;
        powervalue = PlayerPrefs.GetInt("IncPower", 1) * 50;
        IncHealthText.text = "" + healthvalue;
        IncPowerText.text = "" + powervalue;

        //Debug.Log(FindObjectOfType<Enemy>().gameObject.name);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SetEnemySprite()
    {
       
        /*if ((float)GlobalValues.Level % 4 == 0)
        {
        EnemyIcon.sprite = Resources.Load<Sprite>("profilepicture/" + FindObjectOfType<Bonus>().gameObject.name);
        }
        else
        {*/
        EnemyIcon.sprite = Resources.Load<Sprite>("profilepicture/" + FindObjectOfType<Enemy>().gameObject.name);  
       // }
    }

    public void SlapsLeft()
    {
        SlapIcons[GlobalValues.TurnLeft].sprite = SlapDisable;
     
    }
    public void HideButtons()
    {
        if (!OneTime)
        {
            OneTime = true;
           /* if((float)GlobalValues.Level % 4 == 0)
            SlapPanel.SetActive(true);*/
            PowerIncButton.GetComponent<Animation>().Play("GoToDownAnim");
            HealthIncButton.GetComponent<Animation>().Play("GoToDownAnim");
        }
    }
    public void ReduceEnemyHealth()
    {
        if (GlobalValues.EnemyHealth - GlobalValues.Power >= 1)
        {
           GlobalValues.EnemyHealth -= GlobalValues.Power;
           EnemyHealth.text = "" + GlobalValues.EnemyHealth;
           /* if (GlobalValues.TurnLeft <= 0 && (float)GlobalValues.Level % 4 == 0)
            {
                FailPanel.SetActive(true);
            }*/
            //  EnemyHealthSlider.value = GlobalValues.EnemyHealth;
        }
        else
        {
            // EnemyHealth.text = "0";
            //  EnemyHealthSlider.value = 0;
            GlobalValues.EnemyHealth = 0;
           GlobalValues.Coins = GlobalValues.current.GetCoins();
           CompleteCoinText.text = GlobalValues.Coins+"";
           CompleteRewardText.text =  (GlobalValues.Level * 100) + "";
           PlayerPrefs.SetInt("Coins", GlobalValues.Coins + (GlobalValues.Level * 100));
           StartCoroutine(OnPanel(CompletePanel));
        }
        if (!GlobalValues.isBonus)
        {
        ParticleSystem p = Instantiate(WeakParticlePlayer);
        //p.transform.position =   FindObjectOfType<Enemy>().transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).transform.position;
        p.Play();
        }
        EnemyTextAnim.text = "-" + GlobalValues.Power;
        EnemyTextAnim.GetComponent<Animation>().Play();
        StartCoroutine(SliderAnim(EnemyHealthSlider, GlobalValues.EnemyHealth,EnemyAnimSlider,EnemyHealth, 1));
    }

    public void ReducePlayerHealth()
    {
        if (GlobalValues.PlayerHealth - GlobalValues.Power >= 1)
        {
            GlobalValues.PlayerHealth -= GlobalValues.Power;
            PlayerHealth.text = "" + GlobalValues.PlayerHealth;
        //  PlayerHealthSlider.value = GlobalValues.PlayerHealth;
        }
        else
        {
         // PlayerHealth.text = "0";
         // PlayerHealthSlider.value = 0;
            GlobalValues.Coins = GlobalValues.current.GetCoins();
            FailCoinText.text = GlobalValues.Coins + "";
            FailRewardText.text =((GlobalValues.Level * 100)/2) + "";
            PlayerPrefs.SetInt("Coins", GlobalValues.Coins + ((GlobalValues.Level * 100)/2));
            StartCoroutine(OnPanel(FailPanel));
        }
        ParticleSystem p = Instantiate(particleEnemy);
        //p.transform.position =   FindObjectOfType<Player>().transform.GetChild(0).GetChild(2).GetChild(0).GetChild(0).GetChild(1).transform.position;
        p.Play();
        PlayerTextAnim.text = "-" + GlobalValues.Power;
        PlayerTextAnim.GetComponent<Animation>().Play();
        StartCoroutine(SliderAnim(PlayerHealthSlider, GlobalValues.PlayerHealth,PlayerAnimSlider,PlayerHealth, 1));
    }

    IEnumerator OnPanel(GameObject panel)
    {
        yield return new WaitForSeconds(3f);
        panel.SetActive(true);
    }

    IEnumerator SliderAnim(Slider slider, int health,Slider AnimSlider,Text healthtext,float DiscreaseValue)
    {
      //  Debug.Log("VALUE  " + (slider.value - health));
        yield return new WaitForSeconds(0.001f);
        if ((slider.value - health)>20)
        {
            DiscreaseValue = 10;
        }
        else
        {
            DiscreaseValue = 1;
        }
        slider.value -=DiscreaseValue ;
        if(slider.value>=0)
        healthtext.text = "" + slider.value;
        if (slider.value!=health)
        {
            StartCoroutine(SliderAnim(slider, health, AnimSlider,healthtext,DiscreaseValue));
        }
        yield return new WaitForSeconds(0.004f);
        AnimSlider.value -= DiscreaseValue;

    }

    public void IncreaseHealth()
    {
        Debug.Log(GlobalValues.Coins+"  "+healthvalue);
        if (GlobalValues.Coins>=healthvalue)
        {
            PlayerPrefs.SetInt("Health", GlobalValues.PlayerHealth + 25);
            PlayerPrefs.SetInt("IncHealth", PlayerPrefs.GetInt("IncHealth",1) + 1);
            GlobalValues.PlayerHealth = GlobalValues.current.GetPlayerHealth();
            PlayerHealth.text = "" + GlobalValues.PlayerHealth;
            PlayerHealthSlider.maxValue = GlobalValues.PlayerHealth;
            PlayerAnimSlider.maxValue = GlobalValues.PlayerHealth;
            PlayerHealthSlider.value = GlobalValues.PlayerHealth;
            PlayerAnimSlider.value = GlobalValues.PlayerHealth;
            IncreaseHealthText.text = "HEALTH(" + PlayerPrefs.GetInt("IncHealth") + ")";
            PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins")-healthvalue);
            GlobalValues.Coins = PlayerPrefs.GetInt("Coins");
            CoinsText.text = "" + GlobalValues.Coins;
            healthvalue = PlayerPrefs.GetInt("IncHealth", 1) * 50;
            IncHealthText.text = "" + healthvalue;

        }
    }
    public void IncreasePower()
    {
        if (GlobalValues.Coins>=powervalue)
        { 
            PlayerPrefs.SetInt("MaxPower", GlobalValues.MaxPower + 20);
            PlayerPrefs.SetInt("IncPower", PlayerPrefs.GetInt("IncPower",1)+1);
            GlobalValues.MaxPower = GlobalValues.current.GetMaxPower();
            FindObjectOfType<PowerBar>().PowerText.text = "" + GlobalValues.MaxPower;
            IncreasePowerText.text = "POWER(" + PlayerPrefs.GetInt("IncPower") + ")";
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - powervalue);
            GlobalValues.Coins = PlayerPrefs.GetInt("Coins");
            CoinsText.text = "" + GlobalValues.Coins;
            powervalue = PlayerPrefs.GetInt("IncPower", 1) * 50;
            IncPowerText.text="" + powervalue;
        }
    }

    public void Get5x()
    {
        
        Advertisements.Instance.ShowRewardedVideo(CompleteMethod);

    }

    public void NoThanks()
    {
        Advertisements.Instance.ShowInterstitial(InterstitialClosed);
        
        PlayerPrefs.SetInt("Level",GlobalValues.Level+1);
        GlobalValues.Level = GlobalValues.current.LevelNo();
        GameObject.Find("LoadCanvas").transform.GetChild(0).gameObject.SetActive(true);
        DontDestroyOnLoad(GameObject.Find("LoadCanvas"));
        SceneManager.LoadScene(0);
        CompletePanel.SetActive(false);
        Destroy(FindObjectOfType<DontDestroy>().gameObject);
        Destroy(FindObjectOfType<Player>().gameObject);
        if (!GlobalValues.isBonus)
        {
        Destroy(FindObjectOfType<Enemy>().gameObject);
        }
        Destroy(FindObjectOfType<AIController>().gameObject);
        Destroy(FindObjectOfType<PlayerController>().gameObject);
    } 
    
    public void FailNoThanks()
    {
        if (!GlobalValues.isBonus)
        {
            GameObject.Find("LoadCanvas").transform.GetChild(0).gameObject.SetActive(true);
            DontDestroyOnLoad(GameObject.Find("LoadCanvas"));
            SceneManager.LoadScene(0);
            CompletePanel.SetActive(false);
            PlayerPrefs.SetString("Retry", FindObjectOfType<Enemy>().gameObject.name);
            PlayerPrefs.SetString("RetryScene", SceneManager.GetActiveScene().name);
            Destroy(FindObjectOfType<DontDestroy>().gameObject);
            Destroy(FindObjectOfType<Player>().gameObject);
            Destroy(FindObjectOfType<Enemy>().gameObject);
            Destroy(FindObjectOfType<AIController>().gameObject);
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        }
        else
        {
            GlobalValues.Coins = GlobalValues.current.GetCoins();
            FailCoinText.text = GlobalValues.Coins + "";
            FailRewardText.text = "00";
            NoThanks();
        }
        
        Advertisements.Instance.ShowInterstitial(InterstitialClosed);
    }

    public void PlusOne()
    {
        
        Advertisements.Instance.ShowRewardedVideo(PlusOneMethod);
        

    }

    public GameObject Restartbutton, onebutton, nothanksbutton;
    public void LosePanel()
    {
        FailPanel.SetActive(true);
        GlobalValues.OneSlap = false;
        Restartbutton.SetActive(true);
        onebutton.SetActive(false);
        nothanksbutton.SetActive(false);

        //FindObjectOfType<Enemy>().GetComponent<Animator>().enabled=false;
        //FindObjectOfType<Enemy>().GetComponent<Animator>().enabled=true;
        //FindObjectOfType<Enemy>().GetComponent<Animator>().Rebind();
    }

    public void SettingsOnAnimations()
    {
        SettingPanel.GetComponent<Animation>().Play("PanelFromLeftToMiddle");
        StartCoroutine(waitForAnim(1,SettingPanel.transform.GetChild(0).gameObject, "PopupDisplayToggleAnim"));
    }

    public void SettingsAnimations()
    {
        SettingPanel.transform.GetChild(0).gameObject.GetComponent<Animation>().Play("PopupDisplayToggleAnimR");
        StartCoroutine(waitForAnim(.25f, SettingPanel, "PanelFromMiddleToLeft"));
    }

    IEnumerator waitForAnim(float time,GameObject go,string name)
    {
        yield return new WaitForSeconds(time);
        go.GetComponent<Animation>().Play(name);
    }

    private void InterstitialClosed(string advertiser) 
    { 
        Debug.Log("Interstitial closed from: " + advertiser + " -> Resume Game "); 
    }

    private void CompleteMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed); 
        if (completed == true)
        {
            PlayerPrefs.SetInt("Coins", GlobalValues.Coins + ((GlobalValues.Level * 100) * 5));
            GlobalValues.Coins = GlobalValues.current.GetCoins();
            PlayerPrefs.SetInt("Level", GlobalValues.Level + 1);
            GlobalValues.Level = GlobalValues.current.LevelNo();
            GameObject.Find("LoadCanvas").transform.GetChild(0).gameObject.SetActive(true);
            DontDestroyOnLoad(GameObject.Find("LoadCanvas"));
            SceneManager.LoadScene(0);
            CompletePanel.SetActive(false);
            Destroy(FindObjectOfType<DontDestroy>().gameObject);
            Destroy(FindObjectOfType<Player>().gameObject);
            if (!GlobalValues.isBonus)
            {
                Destroy(FindObjectOfType<Enemy>().gameObject);
            }
            Destroy(FindObjectOfType<AIController>().gameObject);
            Destroy(FindObjectOfType<PlayerController>().gameObject);
        }        
        else      
        {
            StartCoroutine(OnPanel(CompletePanel));
        }  
    }

    private void PlusOneMethod(bool completed, string advertiser)
    {
        Debug.Log("Closed rewarded from: " + advertiser + " -> Completed " + completed);
        if (completed == true)
        {
            GlobalValues.OneSlap = true;
            FindObjectOfType<Enemy>().GetComponent<Animator>().enabled = true;
            FindObjectOfType<Enemy>().GetComponent<Animator>().Rebind();
            FindObjectOfType<Player>().GetComponent<Animator>().enabled = true;
            FindObjectOfType<Player>().GetComponent<Animator>().Rebind();
            FindObjectOfType<Player>().GetComponent<Player>().enabled = true;
            FailPanel.SetActive(false);
            Audience[] audience = FindObjectsOfType<Audience>();
            for (int i = 0; i < audience.Length; i++)
            {
                audience[i].GetComponent<Animator>().Rebind();
                audience[i].GetComponent<Audience>().enabled = false;
                audience[i].GetComponent<Audience>().enabled = true;
            }
        }
        else
        {
            StartCoroutine(OnPanel(FailPanel));
        }
    }


}

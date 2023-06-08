using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject enemy,bonus;
    GameController gamecontroller;
    bool Onetime = false;
    public int Health;
    CanvasManager canvasmanager;
    public Transform HairsParent,FaceParent;
    List<GameObject> hair;
    List<GameObject> playerface,tooth;
    bool diedAnimationPlayed = false;
    void Start()
    {
        gamecontroller = FindObjectOfType<GameController>();
        if (!GlobalValues.isBonus)
        {
        InvokeRepeating("Provoke", 2, Random.Range(4, 6));
        }
        canvasmanager = FindObjectOfType<CanvasManager>();
        hair = new List<GameObject>();
        playerface = new List<GameObject>();
        tooth = new List<GameObject>();
        foreach (Transform item in HairsParent)
        {
            if (NameContains("slapMAINhair", item))
            {
                hair.Add(item.gameObject);
            }
            //Debug.Log(hair.Count);
        }  
        foreach (Transform item in FaceParent)
        {
            if (NameContains("slapMAINhead", item))
            {
                playerface.Add(item.gameObject);
            }
            //Debug.Log(hair.Count);
        } 
        foreach (Transform item in HairsParent)
        {
            if (NameContains("toothMain", item))
            {
                tooth.Add(item.gameObject);
            }
            //Debug.Log(hair.Count);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OneTrue()
    {
        Onetime = false;
        gamecontroller.SetPositionsAndRotations();
    }
    public void SlapHit()
    {
        if (Onetime)
        {
            Debug.Log("Player");
            return;
        }
        Onetime = true;

        if (GlobalValues.isBonus)
        {
            GlobalValues.TurnLeft -= 1;
         
          
            if (!bonus)
            {
                bonus = FindObjectOfType<Bonus>().gameObject;
            }
            Invoke("OneTrue",1.5f);
           
            GetComponent<Animator>().ResetTrigger("StartSlap");
            GameObject g = FindObjectOfType<PowerBar>().gameObject;
            g.SetActive(false);
            g.SetActive(true);
            
            if (GlobalValues.EnemyHealth - GlobalValues.Power <= 0)
            {
                //gamecontroller.TapButton.SetActive(false);
                float number2 = Random.Range(0, 21);
                number2 /= 100;
                GetComponent<Animator>().SetBool("IsOpponentDead", true);
                GetComponent<Animator>().SetFloat("VictoryNumber", (float)System.Math.Round(number2, 1));
                Audience[] audience = FindObjectsOfType<Audience>();
                FindObjectOfType<SoundManager>().Win();
                for (int i = 0; i < audience.Length; i++)
                {
                    float number = Random.Range(1, 50);
                    number /= 100;
                    audience[i].GetComponent<Animator>().SetBool("ShouldCheer", false);
                    audience[i].GetComponent<Animator>().SetFloat("VictoryNumber", number);
                    audience[i].GetComponent<Animator>().SetTrigger("Victory");
                }
            }
            else
            {
                
                if (GlobalValues.PowerPercentage >= 0.85)
                {
                    float number2 = Random.Range(0, 51);
                    number2 /= 100;
                    GetComponent<Animator>().SetFloat("HappyAfterSlapNumber", (float)System.Math.Round(number2, 1));
                    FindObjectOfType<SoundManager>().Cheer();
                    Audience[] audience = FindObjectsOfType<Audience>();
                    for (int i = 0; i < audience.Length; i++)
                    {
                        float number = Random.Range(1, 50);
                        number /= 100;
                        audience[i].GetComponent<Animator>().SetTrigger("Applause");
                        audience[i].GetComponent<Animator>().SetFloat("ApplauseNumber", number);
                    }
                }
                else
                {
                    float number2 = Random.Range(0, 91);
                    number2 /= 100;
                    GetComponent<Animator>().SetFloat("SadAfterSlapNumber", (float)System.Math.Round(number2, 1));
                }
            }
            canvasmanager.ReduceEnemyHealth();
            
            
            canvasmanager.SlapsLeft();
            bonus.GetComponent<Bonus>().Slap();
            if (GlobalValues.TurnLeft <= 0)
            {
                GetComponent<Animator>().ResetTrigger("StartSlap");
            }

        }
        else
        {
     

            if (!enemy)
        {
            enemy = FindObjectOfType<Enemy>().gameObject;
        }
        float number1 = Random.Range(0, 41);
        number1 /= 100;
         enemy.GetComponent<Animator>().ResetTrigger("Provoke");
         enemy.GetComponent<Animator>().SetFloat("GetSlappedNumber", (float)System.Math.Round(number1, 1));
            if ((GlobalValues.EnemyHealth - GlobalValues.Power) <= 0 && diedAnimationPlayed == false)
            {
                var enemyAni = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Animator>();
                enemyAni.StopPlayback();
                //player.SetBool("Defeat", true);
                enemyAni.Play("Flying");
                //enemy.GetComponent<Player>().GetComponent<Animation>().Play("Flying");
                diedAnimationPlayed = true;

            }
            if (GlobalValues.EnemyHealth-GlobalValues.Power<=0)
        {
           //enemy.GetComponent<Animator>().enabled = false;
           // enemy.GetComponent<Enemy>().enabled = false;
            //AddForceAtAngle(6000, 0);
            // enemy.GetComponent<Animator>().SetBool("Defeat", true);
            float number2 = Random.Range(0, 21);
            number2 /= 100;
            GetComponent<Animator>().SetFloat("VictoryNumber", (float)System.Math.Round(number2, 1));
            GetComponent<Animator>().SetBool("IsOpponentDead", true);
            Audience[] audience = FindObjectsOfType<Audience>();
            FindObjectOfType<SoundManager>().Win();
            for (int i = 0; i < audience.Length; i++)
            {
                float number = Random.Range(1, 50);
                number /= 100;
                audience[i].GetComponent<Animator>().SetBool("ShouldCheer", false);
                audience[i].GetComponent<Animator>().SetFloat("VictoryNumber", number);
                audience[i].GetComponent<Animator>().SetTrigger("Victory");
            }
        }
        else
        {
                if (GlobalValues.OneSlap)
                {
                    FindObjectOfType<SoundManager>().Lose();
                    Audience[] audience = FindObjectsOfType<Audience>();
                    for (int i = 0; i < audience.Length; i++)
                    {
                        float number = Random.Range(1, 50);
                        number /= 100;
                        audience[i].GetComponent<Animator>().SetBool("ShouldCheer", false);
                        audience[i].GetComponent<Animator>().SetFloat("DefeatNumber", number);
                        audience[i].GetComponent<Animator>().SetTrigger("Defeat");
                    }
                    float number2 = Random.Range(0, 21);
                    number2 /= 100;
                   enemy.GetComponent<Animator>().SetFloat("VictoryNumber", (float)System.Math.Round(number2, 1));
                   enemy.GetComponent<Animator>().SetBool("IsOpponentDead", true);
                  // enemy.GetComponent<Animator>().SetBool("Victory", true);
                    canvasmanager.LosePanel();
                }
                else
                {
        GetComponent<Animator>().ResetTrigger("StartSlap");
        enemy.GetComponent<Animator>().SetBool("StartGettingSlapped",true);
        enemy.GetComponent<Animator>().SetFloat("SlapPower",GlobalValues.PowerPercentage);
            if (GlobalValues.PowerPercentage>=0.85)
            {
                float number2 = Random.Range(0, 51);
                number2 /= 100;
                GetComponent<Animator>().SetFloat("HappyAfterSlapNumber", (float)System.Math.Round(number2, 1));
                FindObjectOfType<SoundManager>().Cheer();
                Audience[] audience = FindObjectsOfType<Audience>();
                for (int i = 0; i < audience.Length; i++)
                {
                    float number = Random.Range(1, 50);
                    number /= 100;
                    audience[i].GetComponent<Animator>().SetTrigger("Applause");
                    audience[i].GetComponent<Animator>().SetFloat("ApplauseNumber", number);
                }
            }
            else
            {
                float number2 = Random.Range(0, 91);
                number2 /= 100;
                GetComponent<Animator>().SetFloat("SadAfterSlapNumber", (float)System.Math.Round(number2, 1));
            }
                }
        }
        StartCoroutine(WaitForAnimation());
        enemy.GetComponent<Enemy>().changePlayerFaceAndHairs();
        canvasmanager.ReduceEnemyHealth();
        }
        if (GlobalValues.Vibrate==0)
        {
            Handheld.Vibrate();
        }
        FindObjectOfType<SoundManager>().Slap();
       
    }

    public void AddForceAtAngle(float force, float angle)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

       enemy.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(ycomponent, 0, xcomponent);
    }

    bool NameContains(string start, Transform transform)
    {
        if (transform.name.StartsWith(start, comparisonType: System.StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        return false;
    }

    IEnumerator WaitForAnimation()
    {
        //  GetComponent<Animator>().SetFloat("SlapNumber", 0);
        Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitingForGetSlap"))
        {
        enemy.GetComponent<Animator>().SetFloat("SlapPower",0);
        Onetime = false;
        if (GameController.CurrentTurn=="Player")
        {
            GameController.CurrentTurn = "Enemy";
        }
        else
        {
            GameController.CurrentTurn = "Player";
        }
        Debug.Log("Call After Slap"+ GameController.CurrentTurn);
        gamecontroller.AfterSlap();
        }
        else
        {
            StartCoroutine(WaitForAnimation());
        }
    }

    void Provoke()
    {
        if (!GlobalValues.isBonus)
        {
        if (!enemy)
        {
            enemy = FindObjectOfType<Enemy>().gameObject;
        }

        if (enemy.GetComponent<Animator>().GetFloat("SlapPower") > 0)
            return;

        gamecontroller.SetPositionsAndRotations();
        float number = Random.Range(0, 31);
        number /= 100;
        // Debug.Log(number);
        GetComponent<Animator>().SetFloat("ProvocationNumber", (float)System.Math.Round(number, 1));
        GetComponent<Animator>().SetTrigger("Provoke");
        GetComponent<Animator>().SetBool("WaitForSlap", false);
        }

    }

   
 public void changePlayerFaceAndHairs()
    {

        Debug.Log((float)GlobalValues.PlayerHealth / (float)GlobalValues.current.GetPlayerHealth());
        if ((float)GlobalValues.PlayerHealth/ (float)GlobalValues.current.GetPlayerHealth()<=0.75 && (float)GlobalValues.PlayerHealth / (float)GlobalValues.current.GetPlayerHealth() > 0.50)
        {
            /*hair[0].SetActive(false);
            hair[1].SetActive(true); */ 


            Debug.Log("IF");
        }
        else if ((float)GlobalValues.PlayerHealth / (float)GlobalValues.current.GetPlayerHealth() <= 0.50 && (float)GlobalValues.PlayerHealth / (float)GlobalValues.current.GetPlayerHealth() > 0.25)
        {
          /*  playerface[0].SetActive(false);
            playerface[1].SetActive(true);*/
            Debug.Log("Else IF");
        }
        else if((float)GlobalValues.PlayerHealth / (float)GlobalValues.current.GetPlayerHealth() <= 0.25)
        {
            hair[0].SetActive(false);
            playerface[0].SetActive(false);
            hair[1].SetActive(false);
            hair[2].SetActive(true);
            hair[3].SetActive(true);
            playerface[1].SetActive(false);
            playerface[2].SetActive(true);
            for (int i = 0; i < tooth.Count; i++)
            {
                tooth[i].gameObject.SetActive(true);
            }
            Debug.Log("ELSE");
        }
    }
}

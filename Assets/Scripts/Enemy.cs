using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject enemy;
    GameController gamecontroller;
    bool Onetime = false;
    public int Health;
    CanvasManager canvasmanager;
    bool diedAnimationPlayed = false;
    void Start()
    {
        gamecontroller = FindObjectOfType<GameController>();
     InvokeRepeating("Provoke",2, Random.Range(4,6));
        canvasmanager = FindObjectOfType<CanvasManager>();
    }

    bool NameContains(string start, Transform transform)
    {
        if (transform.name.StartsWith(start, comparisonType: System.StringComparison.CurrentCultureIgnoreCase))
        {
            return true;
        }

        return false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SlapHit()
    {
        if (Onetime)
        {
            return;
        }
        Onetime = true;

        if (!enemy)
        {
            enemy = FindObjectOfType<Player>().gameObject;
        }
        //if()
        float number1 = Random.Range(0, 41);
        number1 /= 100;
        enemy.GetComponent<Animator>().ResetTrigger("Provoke");
        enemy.GetComponent<Animator>().SetFloat("GetSlappedNumber", (float)System.Math.Round(number1, 1));
        if ((GlobalValues.PlayerHealth - GlobalValues.Power) <= 0 && diedAnimationPlayed==false)
        {
            var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            player.StopPlayback();
            //player.SetBool("Defeat", true);
            player.Play("Flying");
            //enemy.GetComponent<Player>().GetComponent<Animation>().Play("Flying");
            diedAnimationPlayed = true;

        }

            if (GlobalValues.PlayerHealth - GlobalValues.Power <= 0)
        {
           
            //enemy.GetComponent<Animator>().enabled = false;
            
            //enemy.GetComponent<Player>().enabled = false;
            //AddForceAtAngle(6000, 180);
            //enemy.GetComponent<Animator>().SetBool("Defeat", true);
            float number2 = Random.Range(0, 21);
            number2 /= 100;

            GetComponent<Animator>().SetFloat("VictoryNumber", (float)System.Math.Round(number2, 1));
            GetComponent<Animator>().SetBool("IsOpponentDead", true);
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
        }
        else
        {
        GetComponent<Animator>().ResetTrigger("StartSlap");
        enemy.GetComponent<Animator>().SetBool("StartGettingSlapped", true);
        enemy.GetComponent<Animator>().SetFloat("SlapPower", GlobalValues.PowerPercentage);
            if (GlobalValues.PowerPercentage >= 0.85)
            {
                float number2 = Random.Range(0, 51);
                number2 /= 100;
                GetComponent<Animator>().SetFloat("HappyAfterSlapNumber", (float)System.Math.Round(number2, 1));
                Audience[] audience = FindObjectsOfType<Audience>();
                for (int i = 0; i < audience.Length; i++)
                {
                    float number = Random.Range(0, 50);
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
        if (GlobalValues.Vibrate == 0)
        {
            Handheld.Vibrate();
        }
        FindObjectOfType<SoundManager>().Slap();
        StartCoroutine(WaitForAnimation());
        canvasmanager.ReducePlayerHealth();
        enemy.GetComponent<Player>().changePlayerFaceAndHairs();
        // Debug.Log("Enemy");
    }

    public void AddForceAtAngle(float force, float angle)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        enemy.transform.GetChild(0).GetComponent<Rigidbody>().AddForce(ycomponent, 0, xcomponent);
    }

    IEnumerator WaitForAnimation()
    {
        Debug.Log(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.1f);
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitingForGetSlap"))
        {
            enemy.GetComponent<Animator>().SetFloat("SlapPower", 0);
            Onetime = false;
            if (GameController.CurrentTurn == "Player")
            {
                GameController.CurrentTurn = "Enemy";
            }
            else
            {
                GameController.CurrentTurn = "Player";
            }
            gamecontroller.AfterSlap();
        }
        else
        {
            StartCoroutine(WaitForAnimation());
        }

    }

    public void Slap()
    {
        if (!enemy)
        {
            enemy = FindObjectOfType<Player>().gameObject;
        }
        Debug.Log("EnemySlap");
        GlobalValues.Power = Random.Range(17 + (GlobalValues.Level * 3), 35 + (GlobalValues.Level * 7));
        GlobalValues.PowerPercentage = (float)GlobalValues.Power / (float)GlobalValues.MaxPower;
        Debug.Log(GlobalValues.Power);
        enemy.GetComponent<Animator>().ResetTrigger("Provoke");
        enemy.GetComponent<Animator>().SetBool("WaitForSlap", true);
        float number = Random.Range(0, 121);
        number /= 100;
        Debug.Log(System.Math.Round(number, 1));
        GetComponent<Animator>().SetFloat("SlapNumber", (float)System.Math.Round(number, 1));
        GetComponent<Animator>().SetTrigger("StartSlap");
        GetComponent<Animator>().SetFloat("SlapPower", GlobalValues.PowerPercentage);
    }

    void Provoke()
    {
        if (!enemy)
        {
            enemy = FindObjectOfType<Player>().gameObject;
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
    public void changePlayerFaceAndHairs()
    {

        Debug.Log((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth());

        if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0.60 && (float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() > 0.25)
        {
           // transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            //transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            Debug.Log("IF");
        }
  
    }
}

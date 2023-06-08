using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject StartText,PowerBar,TapButton;
    GameObject PowerBarInstantiated, player, enemy,PlayerSpawnpoint,EnemySpawnPoint,bonus;
    public static string CurrentTurn="Player";
    bool turn = true;
    void Start()
    {
        CurrentTurn = "Player";
        PowerBarInstantiated = Instantiate(PowerBar, transform.parent, true);
        player = FindObjectOfType<Player>().gameObject;
/*
        if ((float)GlobalValues.Level % 4 == 0)
        {*/
           // InvokeRepeating("SetPositionsAndRotations", 4, 0.5f);
      //  }
       
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void Slap()
    {
        //stop power Bar
        PowerBarInstantiated.GetComponent<PowerBar>().StopBar();
        
        if (GlobalValues.isBonus)
        {
            if (!bonus)
            {
                bonus = FindObjectOfType<Bonus>().gameObject;
            }
           
            player.GetComponent<Animator>().SetBool("IsBonusLevel",true);

        }
        else
        {
        if (!enemy)
        {
            enemy = FindObjectOfType<Enemy>().gameObject;
        }
        // Debug.Log("Slap");
        //ready enemy for slap
        player.GetComponent<Animator>().ResetTrigger("Provoke");
        enemy.GetComponent<Animator>().ResetTrigger("Provoke");
        enemy.GetComponent<Animator>().SetBool("WaitForSlap", true);
       
        TapButton.SetActive(false);
        InvokeRepeating("SetPositionsAndRotations", 0, 1);
        }

        //player slap
        player.GetComponent<Animator>().SetTrigger("StartSlap");
        player.GetComponent<Animator>().SetFloat("SlapPower",GlobalValues.PowerPercentage);
        float number = Random.Range(0, 121);
        number /= 100;
        Debug.Log(System.Math.Round(number, 1));
        player.GetComponent<Animator>().SetFloat("SlapNumber", (float)System.Math.Round(number, 1));

        StartText.SetActive(false);
        FindObjectOfType<CanvasManager>().HideButtons();
    }

    public void AfterSlap()
    {
        if (!enemy)
        {
            enemy = FindObjectOfType<Enemy>().gameObject;
        }

        Camera.main.GetComponent<Animator>().SetBool("Turn", turn= !turn);

        //Set Turns
        if (CurrentTurn == "Enemy")
        {
            Debug.Log("AfterSlapEnemy");
            enemy.GetComponent<Animator>().SetBool("StartGettingSlapped", false);
            enemy.GetComponent<Animator>().SetBool("WaitForSlap", false);
            StartCoroutine(WaitForAISlap());
        }
        else
        {
            StartCoroutine(TurnButtonOn());
            Debug.Log("AfterSlapPlayer");
            player.GetComponent<Animator>().SetBool("StartGettingSlapped", false);
            player.GetComponent<Animator>().SetBool("WaitForSlap", false);
        }
        player.GetComponent<Animator>().SetFloat("SlapNumber", 0);
        enemy.GetComponent<Animator>().SetFloat("SlapNumber", 0);
        player.GetComponent<Animator>().SetFloat("SlapPower", 0);
        enemy.GetComponent<Animator>().SetFloat("SlapPower", 0);
        enemy.GetComponent<Animator>().SetFloat("GetSlappedNumber", 0);
        player.GetComponent<Animator>().SetFloat("GetSlappedNumber", 0);
        player.GetComponent<Animator>().SetFloat("HappyAfterSlapNumber", 0);
        enemy.GetComponent<Animator>().SetFloat("HappyAfterSlapNumber", 0);
        player.GetComponent<Animator>().SetFloat("SadAfterSlapNumber", 0);
        enemy.GetComponent<Animator>().SetFloat("SadAfterSlapNumber", 0);
        //Set Default Positions and Rotations
        SetPositionsAndRotations();
        
    }

    IEnumerator TurnButtonOn()
    {
        yield return new WaitForSeconds(1f);
        TapButton.SetActive(true);
    }

    IEnumerator WaitForAISlap()
    {
        yield return new WaitForSeconds(2f);
        enemy.GetComponent<Enemy>().Slap();
    }

    public void SetPositionsAndRotations()
    {
        if (!GlobalValues.isBonus)
        {
            if (!enemy)
            {
                if (FindObjectOfType<Enemy>())
                    enemy = FindObjectOfType<Enemy>().gameObject;
            }
            if (!EnemySpawnPoint)
            {
                EnemySpawnPoint = GameObject.Find("EnemySpawnPoint");
            }
            if (enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitingForGetSlap") || enemy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ReadyForSlap"))
            {
                Debug.Log("SetEnemy");
                enemy.transform.position = EnemySpawnPoint.transform.position;
                enemy.transform.rotation = EnemySpawnPoint.transform.rotation;
            }
        }

        if (!PlayerSpawnpoint)
        {
            PlayerSpawnpoint = GameObject.Find("PlayerSpawnPoint");
        }

        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("WaitingForGetSlap") || player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ReadyForSlap"))
        {
            Debug.Log("SetPlayer");
            player.transform.position = PlayerSpawnpoint.transform.position;
            player.transform.rotation = PlayerSpawnpoint.transform.rotation;
        }
        
        
    }

}

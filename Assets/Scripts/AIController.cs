using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIController : MonoBehaviour
{

    GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTransform()
    {
        //if ((float)GlobalValues.Level%4==0)
        //{
        //    Debug.Log("Bonusss");
        //    FindObjectOfType<Manager>().BonusRound();
        //    FindObjectOfType<CanvasManager>().SetEnemySprite();
        //}
        //else
        //{
            GlobalValues.isBonus = false;
            Instantiate();
            enemy.transform.parent = null;
            GameObject SpawnPoint = GameObject.Find("EnemySpawnPoint");
            enemy.transform.position = SpawnPoint.transform.position;
            enemy.transform.rotation = SpawnPoint.transform.rotation;
        //}

    }

    void Instantiate()
    {
        if (PlayerPrefs.GetString("Retry")!="Null")
        {
            enemy = Instantiate(Resources.Load("enemies/characters/" + PlayerPrefs.GetString("Retry")) as GameObject, transform, false);
            enemy.name = PlayerPrefs.GetString("Retry");
            enemy.GetComponent<Animator>().SetBool("WaitForSlap", true);
            FindObjectOfType<CanvasManager>().SetEnemySprite();
            PlayerPrefs.SetString("Retry","Null");
        }
        else
        {
            int number = Random.Range(1,5);
            // Debug.Log("enemies/characters/" + SceneManager.GetActiveScene().name.Substring(0, 3) + number);
            string name = SceneManager.GetActiveScene().name.Substring(0, 3) + number;
            enemy = Instantiate(Resources.Load("enemies/characters/" +name) as GameObject, transform, false);
            enemy.name = name;
            enemy.GetComponent<Animator>().SetBool("WaitForSlap", true);
            FindObjectOfType<CanvasManager>().SetEnemySprite();
        }
       
    }
}

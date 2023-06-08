using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    string[] names = {"Brick","Fish","Pinata","SandBag", "WaterMelon" };
    // Start is called before the first frame update
    GameObject player,enemy,bonus;
    void Start()
    { 
        //set player position rotation
        player = FindObjectOfType<PlayerController>().gameObject;
        player.transform.parent = null;
        player.GetComponent<PlayerController>().SetTransform();
        //set enemy position rotation
        enemy = FindObjectOfType<AIController>().gameObject;
        enemy.transform.parent = null;
        enemy.GetComponent<AIController>().SetTransform();
        if (GameObject.FindGameObjectWithTag("Loading"))
        {
            GameObject.FindGameObjectWithTag("Loading").SetActive(false);
        }
       // bonus.transform.rotation = GameObject.FindGameObjectWithTag("Table").gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BonusRound()
    {
        GlobalValues.isBonus = true;
        string name = names[Random.Range(0, names.Length)];
        bonus = Instantiate(Resources.Load("enemies/bonus/" + name) as GameObject);
        bonus.name = name;
        bonus.transform.position = GameObject.FindGameObjectWithTag("Table").gameObject.transform.position;
        GameObject.FindGameObjectWithTag("Table").gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] SmallObjects;
    public GameObject Full;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slap()
    {
       
        if (transform.name != "SandBag")
        {
                Full.SetActive(false);
            Debug.Log((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth());
            if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0.90 && (float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() > 0.60)
            {
                for (int i = 0; i < 2; i++)
                {
                    SmallObjects[i].AddComponent<Rigidbody>();
                    AddForceAtAngleSmall(200, -90, SmallObjects[i]);
                }
            }
             if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0.60 && (float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() > 0.30)
            {
                for (int i = 0; i < 6; i++)
                {
                    SmallObjects[i].AddComponent<Rigidbody>();
                    AddForceAtAngleSmall(200, -90, SmallObjects[i]);
                }
            }
             if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0)
            {
                for (int i = 0; i < SmallObjects.Length; i++)
                {
                    SmallObjects[i].AddComponent<Rigidbody>();
                    AddForceAtAngleSmall(200, -90, SmallObjects[i]);
                }
            }
        }
        else
        {
            if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0.90 && (float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() > 0.60)
            {
                Full.SetActive(false);
                SmallObjects[0].SetActive(true);
                SmallObjects[0].GetComponent<ParticleSystem>().Play();
            }
             if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0.60 && (float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() > 0.30)
            {
                SmallObjects[1].SetActive(true);
                SmallObjects[1].GetComponent<ParticleSystem>().Play();
            }
            if ((float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() <= 0.30 && (float)GlobalValues.EnemyHealth / (float)GlobalValues.current.GetEnemyHealth() > 0.0)
            {
                SmallObjects[2].SetActive(true);
                SmallObjects[2].GetComponent<ParticleSystem>().Play();
            }
        }
        transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = false;
        AddForceAtAngle(800, 0);
        Invoke("StopForce", 1.37f);
    }

    public void AddForceAtAngle(float force, float angle)
    {
        Debug.Log("profilepicture/" + FindObjectOfType<Bonus>().gameObject.name);
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        transform.GetChild(0).GetComponent<Rigidbody>().AddForce(ycomponent, 0, xcomponent);
    }  
    public void AddForceAtAngleSmall(float force, float angle,GameObject go)
    {
        float xcomponent = Mathf.Cos(angle * Mathf.PI / 180) * force;
        float ycomponent = Mathf.Sin(angle * Mathf.PI / 180) * force;

        go.GetComponent<Rigidbody>().AddForce(ycomponent, 0, xcomponent);
    }

    void StopForce()
    {
        transform.GetChild(0).GetComponent<Rigidbody>().isKinematic = true;
    }
}

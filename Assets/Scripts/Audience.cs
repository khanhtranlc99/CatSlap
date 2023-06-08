using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : MonoBehaviour
{
    // Start is called before the first frame update
    public float number;
    void Start()
    {
        number = Random.Range(1, 6);
        number /= 10;
        GetComponent<Animator>().SetTrigger("ShouldCheer");
        GetComponent<Animator>().SetFloat("CheeringNumber",number);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

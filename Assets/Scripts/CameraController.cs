using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject PowerBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPowerPower()
    {
        if (!PowerBar)
        {
            PowerBar = FindObjectOfType<PowerBar>().gameObject;
        }
        PowerBar.GetComponentInChildren<Text>().text = ""+GlobalValues.MaxPower;
        PowerBar.SetActive(!PowerBar.activeSelf);
    }
}

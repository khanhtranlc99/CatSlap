using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    // Start is called before the first frame update
    public Animation _animation;
    public Text PowerText;
    void Start()
    {
        _animation["PowerBarAnim3D"].normalizedSpeed += ((float)PlayerPrefs.GetInt("Level", 1)/20);
        Debug.Log(_animation["PowerBarAnim3D"].normalizedSpeed);
        _animation.Play();
        PowerText.text = GlobalValues.MaxPower+"";
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _animation.Play();
        }
    }

    public void StopBar()
    {
        Vector3 Angle = transform.GetChild(0).transform.eulerAngles;
        if (Angle.z>=50)
        {
            Angle.z -= 360;
        }
        if (Mathf.Abs(Angle.z)>40)
        {
            Angle.z = 40;
        }

        float Percentage = (Mathf.Abs(Angle.z) / 40) * 100;
        int PowerDiff = GlobalValues.MaxPower - GlobalValues.MinPower;
        GlobalValues.Power = GlobalValues.MinPower + (PowerDiff-((PowerDiff*(int)Percentage)/100));
        PowerText.text = "" + GlobalValues.Power;
        GlobalValues.PowerPercentage = (float)GlobalValues.Power / (float)GlobalValues.MaxPower;
        Debug.Log(GlobalValues.Power+" "+Percentage +" "+GlobalValues.PowerPercentage);
        _animation.Stop();

    }
}

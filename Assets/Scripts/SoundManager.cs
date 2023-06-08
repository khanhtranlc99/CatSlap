using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] Slaps, Cheers, Loses, Wins;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slap()
    {
        if (GlobalValues.Sound==0)
        {
        GetComponent<AudioSource>().clip = Slaps[Random.Range(0, Slaps.Length)];
        GetComponent<AudioSource>().Play();
        }
    }  
    public void Cheer()
    {
        if (GlobalValues.Sound==0)
        {
        transform.GetChild(0).GetComponent<AudioSource>().clip = Cheers[Random.Range(0, Cheers.Length)];
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    } 
    public void Win()
    {
        if (GlobalValues.Sound==0)
        {
        transform.GetChild(0).GetComponent<AudioSource>().clip = Wins[Random.Range(0, Wins.Length)];
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }  
    public void Lose()
    {
        if (GlobalValues.Sound==0)
        {
        transform.GetChild(0).GetComponent<AudioSource>().clip = Loses[Random.Range(0, Loses.Length)];
        transform.GetChild(0).GetComponent<AudioSource>().Play();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Advertisements.Instance.Initialize();
        Advertisements.Instance.ShowBanner(BannerPosition.BOTTOM);
        DontDestroyOnLoad(this);
        if (PlayerPrefs.GetString("RetryScene") !="Null")
        {
        SceneManager.LoadScene(PlayerPrefs.GetString("RetryScene"));
            PlayerPrefs.SetString("RetryScene", "Null");
        }
        else
        {
        int number = Random.Range(1,3);
        SceneManager.LoadScene(number);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_IOS
using Unity.Advertisement.IosSupport;
#endif

public class Load : MonoBehaviour
{
    // Start is called before the first frame update
    DontDestroy des;
    void Awake()
    {

#if UNITY_IOS
        if (ATTrackingStatusBinding.GetAuthorizationTrackingStatus() ==
        ATTrackingStatusBinding.AuthorizationTrackingStatus.NOT_DETERMINED)
        {
            ATTrackingStatusBinding.RequestAuthorizationTracking();
        }
#endif

        if (GameObject.Find("LoadCanvas"))
        {
            Destroy(GameObject.Find("LoadCanvas").gameObject);
        }
        if (FindObjectOfType<DontDestroy>())
            des = FindObjectOfType<DontDestroy>();
        else
        {
            DontDestroyOnLoad(des);
            GameController.CurrentTurn = "Player";
        }
    }

    // Update is called once per frame
    void Update()
    {
   
    }
}

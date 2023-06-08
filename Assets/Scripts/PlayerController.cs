using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
       player = Instantiate(Resources.Load("player/Player") as GameObject,transform,false);
       player.name = "Player";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTransform()
    {
        player.transform.parent = null;
        GameObject SpawnPoint = GameObject.Find("PlayerSpawnPoint");
        player.transform.position = SpawnPoint.transform.position;
        player.transform.rotation = SpawnPoint.transform.rotation;
    }
}

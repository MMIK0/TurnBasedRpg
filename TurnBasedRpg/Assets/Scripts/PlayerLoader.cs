using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLoader : MonoBehaviour
{
      public GameObject player;


    void Start()
    {
        if(PlayerController.instance == null) 
        {
            Instantiate(player);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

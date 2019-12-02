using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialLoader : MonoBehaviour
{
    public GameObject UIScreen;
    public GameObject player;
    public GameObject gameMan;
    public GameObject battleMan;
    void Start()
    {
        if (UIFade.instance == null)
        {
            UIFade.instance = Instantiate(UIScreen).GetComponent<UIFade>();
        }
        if (PlayerController.instance == null)
        {
            PlayerController clone = Instantiate(player).GetComponent<PlayerController>();
            PlayerController.instance = clone;
        }
        if (GameManager.instance == null)
        {
            Instantiate(gameMan);
        }
        if(BattleManager.instance == null)
        {
            Instantiate(battleMan);
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

}
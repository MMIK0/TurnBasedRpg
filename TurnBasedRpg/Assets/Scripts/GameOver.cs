using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{

    public string mainMenuScene;
    public string loadGameScene;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitToMainMenu()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(BattleManager.instance.gameObject);

        SceneManager.LoadScene(mainMenuScene);
    }
    public void LoadLastSave()
    {
        Destroy(GameManager.instance.gameObject);
        Destroy(GameMenu.instance.gameObject);
        Destroy(PlayerController.instance.gameObject);
        //Destroy(BattleManager.instance.gameObject);

        SceneManager.LoadScene(loadGameScene);
    }

}

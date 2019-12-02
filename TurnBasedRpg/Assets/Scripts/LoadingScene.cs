using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadingScene : MonoBehaviour
{

    public float waitToLoad;
    public Text theText;
    private bool shouldFade;
    public float fadeSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shouldFade = true;

        if (shouldFade)
        {
            theText.color = new Color(Mathf.MoveTowards(theText.color.r, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theText.color.g, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theText.color.b, 1f, fadeSpeed * Time.deltaTime), Mathf.MoveTowards(theText.color.a, 0f, fadeSpeed * Time.deltaTime));
            
        }
            if (waitToLoad > 0)
        {
            waitToLoad -= Time.deltaTime;
            if (waitToLoad <= 0)
            {
                SceneManager.LoadScene(PlayerPrefs.GetString("Current_Scene"));

                GameManager.instance.LoadData();
                QuestManager.instance.LoadQuestData();
            }
        }
    }
}

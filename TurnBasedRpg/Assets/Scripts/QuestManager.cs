using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public string[] questMarkerName;
    public bool[] questMarkersComplete;

    public static QuestManager instance;




    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        questMarkersComplete = new bool[questMarkerName.Length];
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveQuestData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadQuestData();
        }
    }

    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questMarkerName.Length; i++)
        {
            if(questMarkerName[i] == questToFind)
            {
                return i;
            }
        }
        Debug.LogError("Quest" + questToFind + " does not exist");
        return 0;
    }

    public bool CheckIfComplete(string questToCheck)
    {
        if(GetQuestNumber(questToCheck) != 0)
        {
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }
        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();
    }

    public void MarkQuestInComplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();
    }

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();

        if(questObjects.Length > 0)
        {
            for(int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckCompletion();
            }
        }
    }
    public void SaveQuestData()
    {
        for(int i = 0; i < questMarkerName.Length; i++)
        {
            if (questMarkersComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerName[i], 1);
            }else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerName[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for(int i = 0; i < questMarkerName.Length; i++)
        {
            int valueToSet = 0;
            if (PlayerPrefs.HasKey("QuestMarker_" + questMarkerName[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerName[i]);
            }

            if(valueToSet == 0)
            {
                questMarkersComplete[i] = false;
            }else
            {
                questMarkersComplete[i] = true;
            }
        }
    }
}

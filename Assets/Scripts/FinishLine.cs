using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
//using UnityEngine.JsonUtility;
//using System.Text.Json;

public class FinishLine : MonoBehaviour
{
    //false if file already written
    //otherwise the leaderboard file will be written twice in succession
    //idk why
    bool written;

    public GameObject finishScreen;
    public GameObject inputScreen;
    public GameObject pauseMenu;

    public InputField inputField;

    string inputName;

    public class HighScores
    {
        public float[] scoreList;
        public string[] nameList;
    }


    void Start() {
        written = false;
    }

    void OnTriggerExit(Collider other)
    {
        //pause game and bring up completion menu
        Time.timeScale = 0;
        inputScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        pauseMenu.GetComponent<PauseMenu>().isComplete = true;

    }

    public void confirmName()
    {
        inputName = inputField.text;
        submitScores();
        finishScreen.SetActive(true);
        inputScreen.SetActive(false);
    }

    void submitScores()
    {
        float finishTime = GameObject.FindWithTag("Timer").GetComponent<Timer>().getTime();
        if(!File.Exists(Application.persistentDataPath + "/leaderboard.json")) {
            using(StreamWriter sw = File.CreateText(Application.persistentDataPath + "/leaderboard.json"))
            {
                HighScores scores = new HighScores();
                scores.scoreList = new float[] {finishTime, 60f, 70f, 80f, 90f};
                scores.nameList = new string[] {inputName, "ZUN", "CID", "PAC", "BIG"};
                Array.Sort(scores.scoreList, scores.nameList);
                string scoresJson = JsonUtility.ToJson(scores);
                sw.WriteLine(scoresJson);
                written = true;
            }
        }

        else
        {
            string jsonString = File.ReadAllText(Application.persistentDataPath + "/leaderboard.json");
            HighScores scores = JsonUtility.FromJson<HighScores>(jsonString);
            if(finishTime < scores.scoreList[4] && !written) 
            {
                scores.scoreList[4] = finishTime; 
                scores.nameList[4] = inputName;
                Array.Sort(scores.scoreList, scores.nameList);
                File.WriteAllText(Application.persistentDataPath + "/leaderboard.json", JsonUtility.ToJson(scores));
                written = true;
            }
        }
    }
}

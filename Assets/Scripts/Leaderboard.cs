using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Leaderboard : MonoBehaviour
{
    public class HighScores
    {
        public float[] scoreList;
        public string[] nameList;
    }

    float[] scoreList;
    public Text leaderboardText;

    public GameObject leaderboardScreen;
    public GameObject titleMain;

    void Start()
    {
        HighScores scores;
        if(!File.Exists(Application.persistentDataPath + "/leaderboard.json")) 
        {
            using(StreamWriter sw = File.CreateText(Application.persistentDataPath + "/leaderboard.json"))
            {
                scores = new HighScores();
                scores.scoreList = new float[] {60f, 70f, 80f, 90f, 100f};
                scores.nameList = new string[] {"ZUN", "CID", "PAC", "BIG", "KID"};
                Array.Sort(scores.scoreList);
                string scoresJson = JsonUtility.ToJson(scores);
                sw.WriteLine(scoresJson);
            }
        }
        else
        {
            string jsonString = File.ReadAllText(Application.persistentDataPath + "/leaderboard.json");
            scores = JsonUtility.FromJson<HighScores>(jsonString);
        }

        scoreList = scores.scoreList;

        string outText = "";

        for (int i = 0; i < 5; ++i)
        {
            int placement = i+1;
            float score = scores.scoreList[i];
            string name = scores.nameList[i];
            outText += placement.ToString() + ". " + name + " " + score.ToString("F2") + "\n";
            ++placement;
        }

        leaderboardText.text += outText;
    }

    public void backToMain()
    {
        titleMain.SetActive(true);
        leaderboardScreen.SetActive(false);
    }    
}

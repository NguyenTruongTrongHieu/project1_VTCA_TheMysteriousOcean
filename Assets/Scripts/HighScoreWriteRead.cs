using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class HighScoreWriteRead : MonoBehaviour
{
    public static HighScoreWriteRead HighScoreInstance;
    string path;

    public List<HighScorePlayer> highScorePlayers = new List<HighScorePlayer>();
    public int highestScore;

    private void Awake()
    {

        if (HighScoreInstance == null)
        {
            HighScoreInstance = this;
            DontDestroyOnLoad(HighScoreInstance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath + "/HighScoreData.txt";

        LoadData();
        highestScore = highScorePlayers.Count == 0 ? 0 : highScorePlayers[highScorePlayers.Count - 1].Score;
    }

    public void SaveData(HighScorePlayer player) {
        try {
            StreamWriter writer = new StreamWriter(path, true);

            string line = player.ToString();
            writer.WriteLine(line);

            writer.Close();

            highScorePlayers.Add(player);
            highestScore = player.Score;
        }
        catch (Exception ex) {
            throw ex;
        }
    }

    public void LoadData()
    {
        try 
        { 
            StreamReader reader = new StreamReader(path);
            string line = reader.ReadLine();

            while (line != null) {
                string[] arr = line.Split('|');

                if (arr.Length == 2) {
                    HighScorePlayer player = new HighScorePlayer();
                    player.NamePlayer = arr[0];
                    player.Score = int.Parse(arr[1]);
                    highScorePlayers.Add(player);
                }

                line = reader.ReadLine();
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

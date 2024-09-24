using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class Level : MonoBehaviour
{
    public static Level instance;
    public Text level;
    public Text NextLevelPanel;

    public GameObject WinLosePanel;
    public Text WinOrLose;

    public uint numDestructable = 0;
    bool StartNextLevel = false;
    float NextLevelTimer = 5;

    public string[] levels = { "PlayScreenLv1", "PlayScreenLv2" };
    public int CurrentLevel = 1;

    public int score = 0;
    public int PreviousLevelScore = 0;
    public int EndScore;
    [SerializeField] Text EndScoreText;
    [SerializeField] Text HighScoreText;
    Text ScoreText;

    private void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
            ScoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
        else {
            if (Level.instance.WinOrLose.text != "Victory!") {
                Destroy(gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        NextLevelPanel.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentLevel <= levels.Length)
        {
            level.text = "Level " + Level.instance.CurrentLevel.ToString();
        }

        if (StartNextLevel)
        {
            if (NextLevelTimer <= 0)
            {
                NextLevelPanel.enabled = false;
                CurrentLevel++;
                if (CurrentLevel <= levels.Length)
                {
                    PreviousLevelScore = score;

                    string SceneName = levels[CurrentLevel - 1];
                    SceneManager.LoadSceneAsync(SceneName);
                }
                else
                {
                    WinOrLose.text = "Win!";

                    foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>())
                    {
                        Destroy(b.gameObject);
                    }

                    ShowEndScore();
                    Time.timeScale = 0f;
                    WinLosePanel.SetActive(true);
                }

                NextLevelTimer = 3;
                StartNextLevel = false;
            }
            else
            {
                if (CurrentLevel < levels.Length)
                {
                    NextLevelPanel.enabled = true;
                }
                NextLevelTimer -= Time.deltaTime;
            }
        }

    }

    public void ResetLevel ()
    {
        foreach (Bullet b in GameObject.FindObjectsOfType<Bullet>())
        {
            Destroy(b.gameObject);
        }

        numDestructable = 0;
        ShowEndScore();
        score = 0;
        AddScore(PreviousLevelScore);

        WinOrLose.text = "Game Over!";
        Time.timeScale = 0f;
        WinLosePanel.SetActive(true);
    }

    public void AddScore(int amountToAdd) { 
        score += amountToAdd;
        ScoreText.text = score.ToString();
    } 

    public void ShowEndScore()
    {
        EndScore = score;
        EndScoreText.text = "Score: " + EndScore.ToString();
        HighScoreText.text = "HighScore: " + HighScoreWriteRead.HighScoreInstance.highestScore.ToString();
    }

    public void AddDestructable() {
        numDestructable++;
    }

    public void RemoveDestructable() {
        numDestructable--;

        if (numDestructable <= 0)
        {
            StartNextLevel = true;
        }
    }
}

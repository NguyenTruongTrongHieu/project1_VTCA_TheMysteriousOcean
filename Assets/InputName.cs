using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputName : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] GameObject saveHighestScore;

    // Start is called before the first frame update
    void Start()
    {
        saveHighestScore.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Level.instance.EndScore > HighScoreWriteRead.HighScoreInstance.highestScore) { 
            saveHighestScore.SetActive(true);
        }
    }

    public void input() {
        string input = inputField.text;

        HighScorePlayer player = new HighScorePlayer();
        player.NamePlayer = input;
        player.Score = Level.instance.EndScore;
        HighScoreWriteRead.HighScoreInstance.SaveData(player);
    }
}

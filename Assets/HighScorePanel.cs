using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    [SerializeField] List <HighScoreElement> highScoreElement = new List<HighScoreElement>();
    int lengthOfElement;

    // Update is called once per frame
    void Update()
    {
        lengthOfElement = 3;

        if (HighScoreWriteRead.HighScoreInstance.highScorePlayers.Count < 3)
        {
            lengthOfElement = HighScoreWriteRead.HighScoreInstance.highScorePlayers.Count;
        }

        for(int i = 0; i< lengthOfElement; i++) {
            highScoreElement[i].PlayerNameText.text = HighScoreWriteRead.HighScoreInstance.highScorePlayers[HighScoreWriteRead.HighScoreInstance.highScorePlayers.Count - (i+1)].NamePlayer;
            highScoreElement[i].ScoreText.text = HighScoreWriteRead.HighScoreInstance.highScorePlayers[HighScoreWriteRead.HighScoreInstance.highScorePlayers.Count - (i + 1)].Score.ToString();
        }
    }
}

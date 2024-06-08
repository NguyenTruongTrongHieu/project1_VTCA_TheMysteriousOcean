using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScorePlayer : MonoBehaviour
{
    public string NamePlayer;
    public int Score;

    public override string ToString()
    {
        return NamePlayer + "|" + Score.ToString();
    }
}

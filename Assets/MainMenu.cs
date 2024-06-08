using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class MainMenu : MonoBehaviour
{
    public void PlayGame() {
        SceneManager.LoadScene(1);
    }

    public void QuitGame(){
        Application.Quit();
    }

    public void Restart() {
        if (Level.instance.WinOrLose.text == "Win!")
        {
            Destroy(Level.instance.gameObject);

            SceneManager.LoadScene(1);
        }
        else
        {
            string SceneName = Level.instance.levels[Level.instance.CurrentLevel - 1];
            SceneManager.LoadScene(SceneName);
        }

        Time.timeScale = 1.0f;
    }
}

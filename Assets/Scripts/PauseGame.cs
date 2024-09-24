using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    bool pause = false;
    public GameObject PausePanel;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) {
            if (pause)
            {
                Resume();
            }
            else
            {
                pauseGame();   
            }
        }
    }
    public void pauseGame()
    {
        if (Level.instance.WinLosePanel.active == false)
        {
            Time.timeScale = 0f;
            PausePanel.SetActive(true);
            pause = true;
        }
        
    }

    public void Resume() {
        Time.timeScale = 1f;
        PausePanel.SetActive(false);
        pause = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour {

    Pise pise;

    MazeManager mazeManager;

    public GameObject shopHelpMassage;
    public Text textTitle;
    public Text textMesage;


    public void ShowShopMessage(string title,string mes)
    {
        shopHelpMassage.SetActive(true);
        textTitle.text = title;
        textMesage.text = mes;
        if (title.Equals("REMONT"))
        {
            textTitle.color = new Color(135, 68, 200, 255);
        }
        else
            if (title.Equals("BOOMBA"))
            {
                textTitle.color = new Color(135, 68, 0, 255);
            }
        else
            if (title.Equals("CTEROUD"))
            {
                textTitle.color = new Color(26, 123, 20, 255);
            }
        else
            textTitle.color = new Color(0, 124, 245, 255);
        ////    textTitle.color = new Color(0, 124, 245, 255);

    }
    public void CloseShopMessage()
    {
        shopHelpMassage.SetActive(false);
    }
    private void Awake()
    {
        Time.timeScale = 1;

        pise = FindObjectOfType<Pise>();
        mazeManager = FindObjectOfType<MazeManager>();

    }

    public void Continue()
    {
        pise.ChengePise();
    }
    public void toMainMenu()
    {
        SceneManager.LoadScene("MianMenu");

    }
    public void Replay()
    {
        pise.ChengePise();
        SceneManager.LoadScene("Maze");
    }
    public void Exit()
    {
        ///Application.Quit();
    }
    public void NexGame()
    {
        mazeManager.SetSave();
        SceneManager.LoadScene("Maze");
        Time.timeScale = 1;
    }
    bool isMute = true;
    
}

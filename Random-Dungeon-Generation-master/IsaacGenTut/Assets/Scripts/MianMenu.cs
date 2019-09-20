using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MianMenu : MonoBehaviour {

    public Button button;
    public SceneFader sceneFader;
    
	void Start () {
        Time.timeScale = 1;
        if (User.USER_INFO.mazeLvl == 0)
            button.interactable = false;
	}
    public void newGame()
    {
        User user = new User()
        {
            login = User.USER_INFO.login,
            pass = User.USER_INFO.pass,
            lvl = 0,
            lvlxp = 0,
            frize = false,
            lifeStill = false,
            shiltCount = 0,
            bombCount = 0,
            slimeCount = 0,
            mazeLvl = 0
        };
        string path = Path.Combine(Application.dataPath, User.USER_INFO.login + ".json");

        File.WriteAllText(path, JsonUtility.ToJson(user));
        User.USER_INFO = user;
        sceneFader.FadeTo("Maze");

    }
   

    public void Continiu()
    {
        sceneFader.FadeTo("Maze");

    }
    public void toLogIn()
    {
        sceneFader.FadeTo("SignIn");
    }

    void Update () {
		
	}
}

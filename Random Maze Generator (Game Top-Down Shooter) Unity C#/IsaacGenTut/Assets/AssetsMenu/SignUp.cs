using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SignUp : MonoBehaviour {

   

    public SceneFader sceneFader;
    public InputField login;
    public InputField pas1;
    public InputField pas2;

    public Text error;
    public Text error1;

   
    public void toSignUp()
    {

        try
        {
            string pathReating = Path.Combine(Application.dataPath, "Reating" + ".txt");
            if (File.Exists(Path.Combine(Application.dataPath, login.text + ".json")))
            {
                error.text = "Логин занят";
                return;
            }
            if (login.text == "")
            {
                error.text = "Login is invalid";
            }
            else
            if (pas1.text == pas2.text && pas1.text != "" && pas2.text != "")
            {
                //User user = new User(login.text, pas1.text, 1);
                User user = new User()
                {
                    login = login.text,
                    pass = pas1.text,
                    lvl = 0,
                    lvlxp = 0,
                    frize = false,
                    lifeStill = false,
                    mazeLvl = 0
                };

                string path = Path.Combine(Application.dataPath, login.text + ".json");
                File.WriteAllText(path, JsonUtility.ToJson(user));
                File.AppendAllText(pathReating, login.text + Environment.NewLine);

                
                sceneFader.FadeTo("SignIN");
            }
            else
            {
                pas1.text = "";
                pas2.text = "";
                error1.text = "Passwords do not match";
            }
        }
        catch { error.text = "Login is invalid"; }

    }

    public void Bakc()
    {
        sceneFader.FadeTo("SignIN");

    }
}
[Serializable]
public class User
{
    public string login;
    public string pass;
    public int lvl;
    public int lvlxp;

    public bool frize;
    public bool lifeStill;

    public int shiltCount;
    public int bombCount;
    public int slimeCount;

    public int mazeLvl;

    public static User USER_INFO;

    
}

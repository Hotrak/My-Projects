using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LogIN : MonoBehaviour {

    

    public InputField login;
    public InputField pas1;
    public Text error;
    public static string Login;

    public SceneFader sceneFader;

    public void Start()
    {
        colculateReating();
    }

    public void Quit()
    {
        Debug.Log("Exciting...");
        Application.Quit();
    }

    public GameObject reating;
    public Text[] namesText;
    public Text[] mazeLvlsText;
    public Text[] userLvlsText;

    private bool ReatingIsOpen;
    public void showReatings()
    {
        ReatingIsOpen = !ReatingIsOpen;
        if (!ReatingIsOpen)
        {
            reating.SetActive(false);

        }
        else
        {
            reating.SetActive(true);
            //colculateReating();
        }


    }
    public void colculateReating()
    {
        string pathReating = Path.Combine(Application.dataPath, "Reating" + ".txt");

        string[] usersName = File.ReadAllLines(pathReating);
        Debug.Log(usersName[0]);

        User[] users = new User[usersName.Length];


        for (int i = 0; i < usersName.Length; i++)
        {
            path = Path.Combine(Application.dataPath, usersName[i] + ".json");
            users[i] = JsonUtility.FromJson<User>(File.ReadAllText(path));
        }

        users = Sort(users);

        for (int i = 0; i < users.Length; i++)
        {
            if (i < 5)
            {
                namesText[i].text = users[i].login;
                mazeLvlsText[i].text = users[i].mazeLvl + "";
                userLvlsText[i].text = users[i].lvl + "";
            }
        }
    }
    public User[] Sort(User[] users)
    {
        User temp;
        for (int i = 0; i < users.Length - 1; i++)
        {
            for (int j = i + 1; j < users.Length; j++)
            {
                if (users[i].lvl < users[j].lvl)
                {
                    temp = users[i];
                    users[i] = users[j];
                    users[j] = temp;
                }
            }
        }
        return users;

    }

    public static string path;
    string pathReating;

    public void LogIn()
    {
        path = Path.Combine(Application.dataPath, login.text + ".json");
        pathReating = Path.Combine(Application.dataPath, "Reating" + ".json");

        if (File.Exists(path))
        {

            User user = JsonUtility.FromJson<User>(File.ReadAllText(path));
            if (user.pass.ToString() == pas1.text)
            {
                PlayerPrefs.SetInt("levelReached", user.lvl);

                User.USER_INFO = new User()
                {
                    login = user.login,
                    pass = user.pass,
                    lvl = user.lvl,
                    lvlxp = user.lvlxp,
                    frize = user.frize,
                    lifeStill = user.lifeStill,
                    shiltCount = user.shiltCount,
                    bombCount = user.bombCount,
                    slimeCount = user.slimeCount,
                    mazeLvl = user.mazeLvl
                };

                sceneFader.FadeTo("MianMenu");

            }
            else
            {
                error.text = "Не верно введён логин или пароль";
            }
        }
        else
        {
            error.text = "Не верно введён логин или пароль";
        }
    }

    public void toSignUp()
    {
        sceneFader.FadeTo("SignUp");

    }
    public void Exit()
    {
        Application.Quit();
    }
}

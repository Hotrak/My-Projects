using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Save : MonoBehaviour {

    public void GetSave()
    {
        //string path = Path.Combine(Application.dataPath, login.text + ".json");
        //User user = JsonUtility.FromJson<User>(File.ReadAllText(path));

    }
    public static void SetSave()
    {
        string path = Path.Combine(Application.dataPath, User.USER_INFO.login + ".json");

        File.WriteAllText(path, JsonUtility.ToJson(User.USER_INFO));
    }
}

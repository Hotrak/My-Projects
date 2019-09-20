using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MazeManager : MonoBehaviour {

    //Мшт
    public GameObject vinMenu;
    public Text statTrakText;
    public Text timerText;

    public int Width = 3;
    public int Height = 3;

    public int startPointX = 0;
    public int startPointY = 0;

    public int mazeLvl;

    public float enamyDamage;
    public float enamyHels;

    public int closeEnamyCountMin = 3;
    public int closeEnamyCountMax = 1;

    public int shotbleEnamyCountMin = -1;
    public int shotbleEnamyCountMax = 0;

    public bool boss;

    PlayerControls player;
    Inventar inventar;
    MazeSpawner mazeSpawner;
    float timer ;
    public int statTrak;
    private void Awake()
    {

        Debug.Log("LOAD");
        LoadSave();
        mazeLvl = 0;

        //playerLvl = 3;
        //playerLvlXp = 80;
        mazeSpawner = FindObjectOfType<MazeSpawner>();
        player = FindObjectOfType<PlayerControls>();
        inventar = FindObjectOfType<Inventar>();
        try
        {
            mazeLvl = User.USER_INFO.mazeLvl;

            player.lvl = User.USER_INFO.lvl;
            player.lvlXp = User.USER_INFO.lvlxp;

            player.isFrize = User.USER_INFO.frize;
            player.isLifeStill = User.USER_INFO.lifeStill;
            player.LvlUp(0);
            player.hels = player.maxHels;

            inventar.shiltCount = User.USER_INFO.shiltCount;
            inventar.boombCount = User.USER_INFO.bombCount;
            inventar.slizeCount = User.USER_INFO.slimeCount;
        } catch (Exception e)
        {
            Debug.Log("Ошибка рпри загрузке 1");

        }



        ColculatwMaze();
        statTrak = 0;
        timer = 0;
    }
    bool isSawn;
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (!isSawn)
        {
            startPointX = UnityEngine.Random.Range(0, Width - 1);
            startPointY = UnityEngine.Random.Range(0, Height - 1);
            mazeSpawner.SpawnMaze();
            isSawn = true;
        }

    }

    public void ColculatwMaze()
    {
        closeEnamyCountMin = closeEnamyCountMin + int.Parse(Mathf.Floor(mazeLvl * 1f).ToString());
        closeEnamyCountMax = closeEnamyCountMax + int.Parse(Mathf.Floor(mazeLvl * 1f).ToString());

        shotbleEnamyCountMin = shotbleEnamyCountMin + int.Parse(Mathf.Floor(mazeLvl * 0.5f).ToString());
        shotbleEnamyCountMax = shotbleEnamyCountMax + int.Parse(Mathf.Floor(mazeLvl * 0.75f).ToString());

        enamyDamage = mazeLvl * 2;
        enamyHels = mazeLvl * 40;

        //playerDamage = playerLvl * 5;
        //playerHels = playerLvl * 10;

        for (int i =  0; i< mazeLvl;i++ )
        {
            if (i % 2 == 0)
                Width++;
            else
                Height++;
        }


    }
    public AudioClip winClip;
    public AudioClip loseClip;
    public GameObject audioPlayer;
    internal void VinGame()
    {

        Camera.main.GetComponent<AudioSource>().mute = true;
        audioPlayer.GetComponent<AudioSource>().PlayOneShot(winClip);
        vinMenu.SetActive(true);

        TimeSpan t = TimeSpan.FromSeconds(timer);

        timerText.text = string.Format("{0:D2}:{1:D2}",
                t.Minutes,
                t.Seconds
                );
        statTrakText.text = statTrak+"";
    }

    public User GetUser()
    {
        User user = new User()
        {
            login = User.USER_INFO.login,
            pass = User.USER_INFO.pass,
            lvl = player.lvl,
            lvlxp = player.lvlXp,
            frize = player.isFrize,
            lifeStill = player.isLifeStill,
            shiltCount = inventar.shiltCount,
            bombCount = inventar.boombCount,
            slimeCount = inventar.slizeCount,
            mazeLvl = ++mazeLvl
        };
        return user;
    }
    public void SetSave()
    {
        string path = Path.Combine(Application.dataPath, User.USER_INFO.login + ".json");

        File.WriteAllText(path, JsonUtility.ToJson(GetUser()));
    }
    public void playLuseClip()
    {
        Camera.main.GetComponent<AudioSource>().mute = true;

        audioPlayer.GetComponent<AudioSource>().PlayOneShot(loseClip);

    }
    public void LoadSave()
    {
        try
        {
            string path = Path.Combine(Application.dataPath, User.USER_INFO.login + ".json");
            User.USER_INFO = JsonUtility.FromJson<User>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Debug.Log("Ошибка рпри загрузке 2");
        }

    }

   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pise : MonoBehaviour {

    public bool isPise;
    public GameObject menuObj;
    public bool isGameOver;
    PlayerControls player;

	void Start () {
        player = FindObjectOfType<PlayerControls>();
	}
	
	void Update () {

        if(!isGameOver)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ChengePise();


            }
	}
    public void GameOver()
    {
        isGameOver = true;
        onPise(false);
    }
    public void ChengePise()
    {
        isPise = !isPise;
        onPise(isPise);
        menuObj.SetActive(isPise);
        player.enabled = !isPise;
    }
    public void onPise(bool isPise)
    {
        Time.timeScale = isPise ? 0 : 1;
    }
}

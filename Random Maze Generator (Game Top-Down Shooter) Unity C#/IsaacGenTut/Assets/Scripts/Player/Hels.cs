using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hels : MonoBehaviour {

    PlayerControls player;
    Image imageHels;
    public Image xp;
    public Text lvl; 
	void Start () {
        player = FindObjectOfType<PlayerControls>();
        imageHels = GetComponent<Image>();

    }
	
	// Update is called once per frame
	void Update () {

        imageHels.fillAmount = player.hels / player.maxHels;

        xp.fillAmount = float.Parse(player.lvlXp.ToString())  / 100;
        lvl.text = player.lvl+ "";

    }
}

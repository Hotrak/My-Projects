using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

    public float damage;
    public float dopDamage;
    public PlayerControls player;

    public float NextTimeToFier;
    public float FierRate;

    public bool isFrize;
    public bool isLifeSill;
    protected Pise pise;
    MazeManager mazeManager;

    void Awake() {
        player = FindObjectOfType<PlayerControls>();
        pise = FindObjectOfType<Pise>();
        mazeManager = FindObjectOfType<MazeManager>();

    }

    protected void CheckFrize()
    {
        isFrize = player.isFrize;
        isLifeSill = player.isLifeStill;
        dopDamage = player.dopDamage;
    }

}

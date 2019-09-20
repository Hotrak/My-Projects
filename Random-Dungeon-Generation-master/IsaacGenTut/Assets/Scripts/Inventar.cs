using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventar : MonoBehaviour {

    public GameObject shilt;
    public GameObject boomb;
    public GameObject slize;

    public Text shiltTextCount;
    public Text bombTextCount;
    public Text slizeTextCount;

    public int shiltCount;
    public int boombCount;
    public int slizeCount;

    public float shiltTimeCount;
    private float time;
    private bool shiltIsActive;
    public Image shiltImage;

    public void ActivShilt()
    {
        shiltIsActive = true;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        shilt.SetActive(shiltCount != 0);
        boomb.SetActive(boombCount != 0);
        slize.SetActive(slizeCount != 0);

        if (shiltCount != 0)
        {
            shiltTextCount.text = shiltCount + "";
        }

        if (boombCount != 0)
        {
            bombTextCount.text = boombCount + "";
        }

        if (slizeCount != 0)
        {
            slizeTextCount.text = slizeCount + "";
        }

        if (shiltIsActive)
        {
            time += Time.deltaTime;
            shiltImage.fillAmount = time / 20;
            if (time > shiltTimeCount)
            {
                shiltIsActive = false;
                FindObjectOfType<PlayerControls>().RestSprite();
                time = 0;
            }
        }

    }
}

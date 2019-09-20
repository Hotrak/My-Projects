using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHels : MonoBehaviour {

    public GameObject helsObg;
    public Image helsImage;
    private Enemy boss;

    public Enemy Boss {
        get
        {
            return boss;
        }

        set
        {
            boss = value;
            helsObg.SetActive(true);
        }
    }

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (boss != null)
        {
            helsImage.fillAmount = boss.Hels / boss.maxHels;
        }
        else
            helsObg.SetActive(false);

    }
}

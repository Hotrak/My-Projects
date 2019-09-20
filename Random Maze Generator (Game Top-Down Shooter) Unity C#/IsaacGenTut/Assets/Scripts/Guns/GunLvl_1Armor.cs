using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLvl_1Armor : Gun {

    public Transform StartPoinGun1;
    public Transform StartPoinGun2;

    public Animator animation1;
    public Animator animation2;


    public GameObject Effect1;
    public GameObject Effect2;

    public Bullet bullet;
    bool shooting = true;
    bool isReload;

    bool isGun1;

    void Start () {
		
	}

    
    // Update is called once per frame
    void Update() {

        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFier)
        {
            NextTimeToFier = Time.time + 1f / FierRate;

            isGun1 = !isGun1;
            if (isGun1)
            {
                if (shooting)
                {
                    Shoot(StartPoinGun1);
                    animation1.SetTrigger("Shoot");
                    Effect1.SetActive(true);
                    Effect2.SetActive(false);

                }
                else

                if (Input.GetButtonDown("Fire1"))
                {

                    //GetComponent<AudioSource>().PlayOneShot(Empte);

                }
            }else
            {
                if (shooting)
                {
                    Shoot(StartPoinGun2);
                    Effect1.SetActive(false);
                    Effect2.SetActive(true);
                    animation2.SetTrigger("Shoot");
                    player.PlayAudioClep();
                }
                else

                   if (Input.GetButtonDown("Fire1"))
                    {

                    //GetComponent<AudioSource>().PlayOneShot(Empte);

                    }

            }
        }
        if (Input.GetButtonUp("Fire1"))
        {
            Effect1.SetActive(false);
            Effect2.SetActive(false);
        }

    }
    void Shoot(Transform StartPoin)
    {
      
        CheckFrize();

        Bullet newBullet = Instantiate(bullet, StartPoin.position, FindObjectOfType<PlayerControls>().transform.rotation) as Bullet;
        newBullet.damage = damage + dopDamage;
        newBullet.frize = isFrize;
        newBullet.isLifeStill = isLifeSill;


        newBullet.target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLvl_2 : Gun {



    public Transform startPoint;
    public Transform effectPoint;
    public GameObject Effect;

    public Bullet bullet;

    //float NextTimeToFier;
    //float FierRate = 5;




    void Update()
    {


        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFier)
        {

            NextTimeToFier = Time.time + 1f / FierRate;

            Shoot();
            GameObject go =  Instantiate(Effect,effectPoint.position, Effect.transform.rotation);
            Destroy(go,1);
            Effect.SetActive(true);
            player.PlayAudioClep();
            //player.PlayAudioClep();



        }
        if (Input.GetButtonUp("Fire1"))
        {
            //Effect.SetActive(false);
        }

    }

    void Reloading()
    {

    }

    void Shoot()
    {
        CheckFrize();

        Bullet newBullet = Instantiate(bullet, startPoint.position, player.transform.rotation) as Bullet;
        newBullet.damage = damage + dopDamage;

        newBullet.frize = isFrize;
        newBullet.isLifeStill = isLifeSill;
        newBullet.isSplash = true;

        newBullet.target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }
}

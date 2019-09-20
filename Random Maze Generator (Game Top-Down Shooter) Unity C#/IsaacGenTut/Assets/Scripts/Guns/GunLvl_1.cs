using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLvl_1 : Gun {

   

    public Transform startPoint;
    public GameObject Effect;

    public Bullet bullet;

    //float NextTimeToFier;
    //float FierRate = 5;
    


   
    void Update () {

       
        if (Input.GetButton("Fire1") && Time.time >= NextTimeToFier)
        {

            NextTimeToFier = Time.time + 1f / FierRate;

            Shoot();
            Effect.SetActive(true);
            player.PlayAudioClep();



        }
        if (Input.GetButtonUp("Fire1"))
        {
            Effect.SetActive(false);
        }
        if (player.isHaveArmor)
        {
            Effect.SetActive(false);
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

        newBullet.target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
    }
}

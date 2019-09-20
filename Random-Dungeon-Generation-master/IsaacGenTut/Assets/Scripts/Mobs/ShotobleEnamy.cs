using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShotobleEnamy : Enemy {

    // Use this for initialization
    public Animator animation1;
    public Animator animation2;

    public EnemyBullet bullet;
    public Transform startPoint1;
    public Transform startPoint2;

    bool isGun1;

    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {

        //Vector2 difff =player.transform.position;
        Vector2 diff = player.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);


        if (Time.time >= NextTimeToFier)
        {
            NextTimeToFier = Time.time + 1f / FierRate;

            isGun1 = !isGun1;
            if (isGun1)
            {
                Shoot(startPoint1);
                animation1.SetTrigger("Shoot");
                //Effect1.SetActive(true);
                //Effect2.SetActive(false);
            }
            else
            {
                Shoot(startPoint2);
               ///Effect1.SetActive(false);
                //Effect2.SetActive(true);
                animation2.SetTrigger("Shoot");

            }
        }
       

        //Shoot();
    }


    private void Shoot(Transform startPoint)
    {

        EnemyBullet newBullet = Instantiate(bullet, startPoint.position, transform.rotation) as EnemyBullet;
        newBullet.damage = 10;
        newBullet.target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        
    }
    public override void GetGift()
    {
        player.LvlUp(lvlXp);
    }
    public override void MinusTask()
    {
        player.task.Minus(this);
    }


}

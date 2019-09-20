using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseEnemy : Enemy {

    public float dis;

    bool isFind;

    float chengeAttackTime;
    bool isAttack1;



    private void Start()
    {
        maxSpeed = speed;
    }
    void Update () {

        dis = Vector3.Distance(player.transform.position, transform.position);
        if (dis < 1.5f)
        {
            isFind = true;
            chengeAttackTime += Time.deltaTime;
            if (chengeAttackTime > 2)
            {
                chengeAttackTime = 0;
                isAttack1 = !isAttack1;
                if (isAttack1)
                    animator.SetInteger("state", 1);
                else
                    animator.SetInteger("state", 2);
            }


            
            Attack();

        }
        else
        {
            isFind = false;
            animator.SetInteger("state",3);
        }
        rigidbody.velocity = transform.forward * speed * 10;

    }
    
    private void Attack()
    {
        if (Time.time >= NextTimeToFier)
        {
            NextTimeToFier = Time.time + 1f / FierRate;
            player.RessiveDamage(damage/10);
  
        }
    }
    private void FixedUpdate()
    {
        //var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);


        //transform.LookAt(player.transform);

        //Vector2 dir = player.transform.position - transform.position;
        //transform.Translate(dir.normalized * Time.deltaTime * speed);

        //transform.Translate(Vector2.up * speed * Time.deltaTime);

        if(!isFind)
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed*Time.deltaTime);

        Vector2 diff = player.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
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

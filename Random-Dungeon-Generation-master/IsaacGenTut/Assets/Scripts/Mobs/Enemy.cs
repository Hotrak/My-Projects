using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


    public int lvlXp;
    public float Hels;
    public float maxHels;
    public int speed;
    public float damage;
    public int maxSpeed;
    public bool isFrize;
    public bool isLifeStill;
    
    public Animator animator;
    public PlayerControls player;
    public Rigidbody2D rigidbody;
    protected MazeManager mazeManager;

    public float NextTimeToFier;
    public float FierRate = 5;
    public float maxFierRate;

    public GameObject slime;

    public virtual void Awake()
    {
        slime = Resources.Load("Slize") as GameObject;
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerControls>();
        rigidbody = GetComponent<Rigidbody2D>();
        mazeManager = FindObjectOfType<MazeManager>();
        maxFierRate = FierRate;
        damage += mazeManager.enamyDamage;
        Hels += mazeManager.enamyHels;
        maxHels = Hels;
    }
    public virtual void ReceveDamage(float damage)
    {
        ReceveDamageEffect();
        for (int i = 0; i < damage;i++)
        {
            if (Hels > 0)
            {
                Hels--;
            }
            else
            {
                Die();
                break;
            }
        }

        //animator.SetInteger("state", 3);

    }
    private void ReceveDamageEffect()
    {
        if(!isFrize)
            animator.SetTrigger("setDamage");
        else
            animator.SetTrigger("setFrize");

    }
    public  void Die()
    {
        if (isLifeStill)
        {
            player.addHels(5);
            Debug.Log("SET_HELS");
        }else
            Debug.Log("SET_HELS_ERROR");
        GetGift();
        MinusTask();
        mazeManager.statTrak++;
        int rand = UnityEngine.Random.Range(0,4);
        if (rand == 0)
            Instantiate(slime,transform.position,Quaternion.identity);
        Destroy(gameObject);

    }
    public virtual void MinusTask()
    {
        

    }

    public virtual void GetGift()
    {
        Debug.Log("CloseEnemyDenide1");
    }   

    public void Frize()
    {
        isFrize = true;
        speed = maxSpeed - 5;
        FierRate = maxFierRate - 2;
        //speed -= 4;
        Invoke("offFrize", 1.5f);
    }
    private void offFrize()
    {
        isFrize = false;
        speed = maxSpeed;
        FierRate = maxFierRate;

        //speed +=  4;
    }
}

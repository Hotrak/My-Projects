using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomb : MonoBehaviour {

    public float radius;
    public float speed;
    public float damage;
    public GameObject boomEffect;
    CircleCollider2D collider;

    float NextTimeToFier;
    float FierRate = 5;

    float time = 0;
    void Start () {
        collider = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {


        time += Time.deltaTime;

        if (time > speed)
        {
            collider.radius = radius;
            GameObject Go =  Instantiate(boomEffect,transform.position, boomEffect.transform.rotation);
            Destroy(Go,1f);
            Destroy(gameObject,0.1f);
        }
       

    }

    bool isDamage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
            enemy.ReceveDamage(damage);
        else
        {
            Box box = collision.GetComponent<Box>();
            if (box != null)
                box.DestroyGameObgect();


        }



    }


}

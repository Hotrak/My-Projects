using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{

    public int lvl;
    public int lvlXp;

    public float speed;
    public Transform foot;
    private Vector2 direction;
    public Animator animation;
    
    public float maxHels;
    public float hels;

    public GameObject DamageEffect;
    public GameObject armorEffect;

    private Rigidbody2D componentRigidbody;

    public bool isHaveArmor;
    public GameObject armor;
    public GameObject spriteLvl1;
    public GameObject spriteLvl2;
    public GameObject spriteLvl3;
    public GameObject spriteNow;

    public MazeManager mazeManager;
    public GameObject boomb;

    private Inventar inventar;

    public bool isFrize;
    public bool isLifeStill;

    public Task task;

    public AudioClip audioShoot;

    public float dopDamage;
    float dopHels;

    private void Start()
    {
        componentRigidbody = GetComponent<Rigidbody2D>();
        inventar = FindObjectOfType<Inventar>();
        mazeManager = FindObjectOfType<MazeManager>();
     
        
        //hels = maxHels;

        LvlUp(0);
    }

    float horizontal;
    float vertical;

    private Transform movePoint;
    public void AftomatickMove(Transform transform)
    {
        aftomatickMove = transform;
        movePoint = transform;
    }
    public bool aftomatickMove;

    private void Update()
    {
        if (!aftomatickMove)
            Move();
        else
            Move(movePoint);
        

        if (Input.GetKeyDown(KeyCode.Space) && inventar.boombCount != 0)
        {
            CreateBoomb();
            inventar.boombCount--;
        }

        if (Input.GetKeyDown(KeyCode.Q)&& inventar.shiltCount!=0 && !isHaveArmor)
        {
            ActivArmor();
            inventar.shiltCount--;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            Debug.Log("dfdf");
        }

        //horizontal = Input.GetAxisRaw("Horizontal");
        //vertical = Input.GetAxisRaw("Vertical");



    }
    public void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        direction = moveInput.normalized * speed;

        //foot.rotation = Quaternion.LookRotation(moveInput);
        if (moveInput == new Vector2(0, 0))
            animation.SetBool("Wolck", false);
        else
            animation.SetBool("Wolck", true);
    }

    float time;
    public void Move(Transform transform)
    {
        transform.position = Vector3.Lerp(transform.position, transform.position, speed * Time.deltaTime);

        time += Time.deltaTime;

        if (time >= 0.3f)
        {
            time = 0;
            aftomatickMove = false;
            Camera.main.GetComponent<CameraController>().CreateDors();
        }
    }
    public void PlayAudioClep()
    {
        GetComponent<AudioSource>().PlayOneShot(audioShoot);

    }
    public void addHels(float count)
    {
        for (int i = 0; i < count; i++)
            if (hels < maxHels)
                hels++;
    }

    private void FixedUpdate()
    {
        //var mosePositio = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //Quaternion rot = Quaternion.LookRotation(transform.position-mosePositio,Vector3.forward);

        //transform.rotation = rot;
        //transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        //componentRigidbody.angularVelocity = 0;

        //float input = Input.GetAxis("Vertical");
        //componentRigidbody.AddForce(transform.up * speed * input);

        componentRigidbody.MovePosition(componentRigidbody.position + direction * Time.deltaTime);

        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        componentRigidbody.MoveRotation(angle - 90);

    
        
          //componentRigidbody.velocity = new Vector2(horizontal * speed, vertical * speed);
    }

    void CreateBoomb()
    {
        Instantiate(boomb,transform.position, boomb.transform.rotation);
    }
   
    public GameObject ArmorEffect
    {
        get
        {
            return armorEffect;
        }

        set
        {
            armorEffect = value;
        }
    }


    void OnCollisionEnter2D(Collision2D col)
    {
        //gameObject.GetComponent<Rigidbody2D>().AddForce(transform.right * 1 * poweForce, ForceMode2D.Impulse);
    }

    public void RessiveDamage(float damage)
    {
        for (int i = 0; i< damage;i++)
        {
            hels--;
        }
        
        GameObject Go =  Instantiate(DamageEffect,transform.position, DamageEffect.transform.rotation);
        Destroy(Go,1);
        if (hels <= 0)
            Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Shilt")
        {
            inventar.shiltCount++;
           
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "boomb")
        {
            inventar.boombCount++;

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "slime")
        {
            inventar.slizeCount++;

            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Finish")
        {
            mazeManager.VinGame();

            Destroy(collision.gameObject);
        }

    
    }

    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "BombShop")
    //    {

    //    }
    //    if (collision.gameObject.tag == "ShildShop")
    //    {

    //    }
    //    if (collision.gameObject.tag == "FrizeShop")
    //    {

    //    }
    //    if (collision.gameObject.tag == "LifeStillShop")
    //    {

    //    }
    //}

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "BombShop")
    //    {

    //    }
    //    if (collision.gameObject.tag == "ShildShop")
    //    {

    //    }
    //    if (collision.gameObject.tag == "FrizeShop")
    //    {

    //    }
    //    if (collision.gameObject.tag == "LifeStillShop")
    //    {

    //    }
    //}
    public GameObject DieObj;
    public AudioClip luseClip;
    bool isDie;
    public void Die()
    {
        if (!isDie)
        {
            DieObj.SetActive(true);
            //FindObjectOfType<Pise>().GameOver();
            //Camera.main.GetComponent<AudioSource>().mute = true;
            //GetComponent<AudioSource>().PlayOneShot(luseClip);
            mazeManager.playLuseClip();
            gameObject.SetActive(false);
            isDie = true;
        }
        

        

    }
    private void ActivArmor()
    {
        ChengeSprite(armor);
        armor.GetComponent<GunLvl_1Armor>().isFrize = isFrize;
        addHels(maxHels);
        //armor.SetActive(true);
        //spriteNow.SetActive(false);
        isHaveArmor = true;
        inventar.ActivShilt();

        GameObject Go = Instantiate(armorEffect, new Vector2(0, 0), DamageEffect.transform.rotation);
        Go.transform.SetParent(transform, false);
        Destroy(Go, 1);
    }
   
    public void LvlUp(int xp)
    {
        
        for (int i = 0; i < xp;i++)
        {
            lvlXp++;
            if (lvlXp>=100)
            {
                lvlXp = 0;
                lvl++;
            }
        }
        if (lvl < 10)
        {
            if (!isHaveArmor)
                ChengeSprite(spriteLvl1);
        }
        if (lvl >= 10 && lvl < 20)
        {
            if (!isHaveArmor)
                ChengeSprite(spriteLvl2);

            GameObject Go = Instantiate(armorEffect, new Vector2(0, 0), DamageEffect.transform.rotation);
            Go.transform.SetParent(transform, false);
            Destroy(Go, 1);
        }
        if (lvl >= 20)
        {
            if (!isHaveArmor)
                ChengeSprite(spriteLvl3);

            GameObject Go = Instantiate(armorEffect, new Vector2(0, 0), DamageEffect.transform.rotation);
            Go.transform.SetParent(transform, false);
            Destroy(Go, 1);
        }
        dopDamage = lvl * 2;
        maxHels -= dopHels;
        dopHels = lvl * 10;
        maxHels += dopHels;
        //addHels(10);

    }

    
    public void RestSprite()
    {
        isHaveArmor = false;
        LvlUp(0);
    }
    public void ChengeSprite(GameObject sprite)
    {
        spriteLvl1.SetActive(false);
        spriteLvl2.SetActive(false);
        spriteLvl3.SetActive(false);

        armor.SetActive(false);

        spriteNow = sprite;
        spriteNow.SetActive(true);
    }
    //if (Input.GetKey(KeyCode.D)) {
    //    transform.Translate(Vector2.right * speed *Time.deltaTime);
    //}
    //if (Input.GetKey(KeyCode.A))
    //{
    //    transform.Translate(-Vector2.right * speed * Time.deltaTime);
    //}
    //if (Input.GetKey(KeyCode.W))
    //{
    //    transform.Translate(Vector2.up * speed * Time.deltaTime);
    //}
    //if (Input.GetKey(KeyCode.S))
    //{
    //    transform.Translate(-Vector2.up * speed * Time.deltaTime);
    //}



}
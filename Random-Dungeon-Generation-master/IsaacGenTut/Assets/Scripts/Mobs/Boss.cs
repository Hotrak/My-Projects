using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy {

    public Transform startPoint1;
    public Transform startPoint2;
    public Transform startPoint3;

    public GameObject teleportEffectStart;
    public GameObject teleportEffectEnd;

    public int state;
    public Cell cell;

    public EnemyBullet bullet;

    public GameObject mobSpawner;

    public float timeShoot = 5;
    public int bulletSpeed;
    float time;

    bool stay;

    public GameObject finishObj;

    public override void Awake()
    {
        slime = Resources.Load("Slize") as GameObject;
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerControls>();
        rigidbody = GetComponent<Rigidbody2D>();
        mazeManager = FindObjectOfType<MazeManager>();
        maxFierRate = FierRate;
        damage += mazeManager.enamyDamage*2;
        Hels += mazeManager.enamyHels*1.5f;
        maxHels = Hels;
        //base.Awake();
    }
    private void Start()
    {
        FindObjectOfType<BossHels>().Boss = this;
    }

    void Update() {

        Vector2 diff = player.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        if (!stay)
        {
            if (state == 0)
            {
                TeleportCalc();
                state = 1;
                //stay = true;
                //StartCoroutine(ChengeState(0));

            }
            else
        if (state == 1)
            {
                if (Time.time >= NextTimeToFier)
                {
                    NextTimeToFier = Time.time + 1f / FierRate;
                    ThryDerectionShoot();

                }

                time += Time.deltaTime;
                if (time > timeShoot)
                {
                    stay = true;
                    StartCoroutine(ChengeState(0));

                    time = 0;
                }
            }
            else
        if (state == 2)
            {
                TeleportCalc(9, 4);
                stay = true;
                StartCoroutine(ChengeState(2));

            }
        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    //ThryDerectionShoot();
        //    //TeleportCalc();
        //    TeleportCalc(9,4);
        //}

    }

    IEnumerator ChengeState(int time)
    {
        yield return new WaitForSeconds(time);

        while (true)
        {
            int state = Random.Range(0, 3);
            if (this.state != state)
            {
                this.state = state;
                break;
            }
        }
        stay = false;

    }

    private void ThryDerectionShoot()
    {
        Shoot(startPoint1);
        Shoot(startPoint2);
        Shoot(startPoint3);
    }

    private void Shoot(Transform startPoint)//Transform startPoint
    {

        EnemyBullet newBullet = Instantiate(bullet, transform.position, startPoint.rotation) as EnemyBullet;
        newBullet.damage = damage;
        //newBullet.speed = bulletSpeed;
        newBullet.target = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);

    }

    private void TeleportCalc()
    {

        //cell = Camera.main.GetComponent<CameraController>().cells[24];
        GameObject Go = Instantiate(teleportEffectStart, transform.position, teleportEffectStart.transform.rotation);
        Destroy(Go, 1f);
        while (true)
        {
            int x = Random.Range(0, 18);
            int y = Random.Range(0, 8);

            if (y != 0 && x != 0 && y != 7 && x != 17)
            {
                animator.SetInteger("state", 5);
                StartCoroutine(Teleport(0.5f, new Vector2(cell.sellX[x], cell.sellY[y])));
                break;
            }
        }

    }

    private void TeleportCalc(int x, int y)
    {

        //cell = Camera.main.GetComponent<CameraController>().cells[24];
        GameObject Go = Instantiate(teleportEffectStart, transform.position, teleportEffectStart.transform.rotation);
        Destroy(Go, 1f);
        animator.SetInteger("state", 5);

        StartCoroutine(Teleport(0.5f, new Vector2(cell.sellX[x], cell.sellY[y]), true));

    }

    IEnumerator Teleport(float time, Vector2 position, bool spown = false)
    {
        yield return new WaitForSeconds(time);

        transform.position = position;
        animator.SetInteger("state", 0);
        if (spown)
        {
            mobSpawner.GetComponent<MobSpavner>().closeEnamy = true;
            mobSpawner.GetComponent<MobSpavner>().shotbleEnamy = false;
            Instantiate(mobSpawner, new Vector2(position.x + 3, position.y), transform.rotation);
            Instantiate(mobSpawner, new Vector2(position.x - 3, position.y), transform.rotation);
            player.task.cloaseEnamyCount += 2;
        }

    }

    
    public override void MinusTask()
    {
        player.task.Minus(this);
    }
}

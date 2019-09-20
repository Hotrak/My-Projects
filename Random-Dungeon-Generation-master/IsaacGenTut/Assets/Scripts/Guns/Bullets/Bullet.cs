using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Vector2 target;
    int speed = 20;
    Rigidbody2D rigidbody2D;
    public float damage;
    public GameObject effect;
    public GameObject effectWol;
    public GameObject effectWolSprite3;
    public bool isSplash;

    public bool frize;
    public bool isLifeStill;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        Invoke("DestroyGameObject",2f);
    }

    private void DestroyGameObject(GameObject effect)
    {
       
        GameObject Go = Instantiate(effect, transform.position, effect.transform.rotation);
        Destroy(Go,1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update () {

        transform.Translate(Vector2.up * speed * Time.deltaTime);

        //var dir = target;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //rigidbody2D.MoveRotation(angle - 90);
        //transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        CheckGraund();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Enemy unit = collision.GetComponent<Enemy>();
        if (unit != null)
        {

           

            if (isSplash)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 3);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.tag == "Enemy")
                    {
                        Enemy enemy = collider.GetComponent<Enemy>();
                        enemy.ReceveDamage(damage);
                        Debug.Log("SPASH_DAMAGE");
                    }
                }

            }
            else
            {
               
                unit.ReceveDamage(damage);
               
            }
            if (frize)
                unit.Frize();
            unit.isLifeStill = isLifeStill;
            DestroyGameObject(effect);

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        //if(collision.gameObject.tag!="Player")
        //    DestroyGameObject();
    }

    private void CheckGraund()
    {
        Collider2D[] collider1 = Physics2D.OverlapCircleAll(transform.position, 0.02f);


        if (collider1.Length > 1 && collider1.All(x => !x.GetComponent<PlayerControls>()) && collider1.All(x => !x.GetComponent<Enemy>()) && collider1.All(x => !x.GetComponent<EnemyBullet>()))
        {
            if (isSplash)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);
                foreach (Collider2D collider in colliders)
                {
                    if (collider.tag == "Enemy")
                    {
                        Enemy enemy = collider.GetComponent<Enemy>();
                        enemy.ReceveDamage(damage);
                        Debug.Log("SPASH_DAMAGE");
                    }
                }

            }
            GameObject enterEffect;
            if (!isSplash)
                enterEffect = effectWol;
            else
                enterEffect = effectWolSprite3;
            DestroyGameObject(enterEffect);

        }

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    public Vector2 target;
    public int speed = 10;
    Rigidbody2D rigidbody2D;
    public float damage;
    public GameObject effect;

    private void DestroyGameObject(GameObject effect)
    {
        if (effect != null)
        {
            GameObject Go = Instantiate(effect, transform.position, effect.transform.rotation);
            Destroy(Go, 1);
            
        }
        Destroy(gameObject);

    }

    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        //Invoke("DestroyGameObject", 2f);
    }
	
	void Update () {
        transform.Translate(Vector2.up * speed * Time.deltaTime);


        //var dir = target;
        //var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //rigidbody2D.MoveRotation(angle - 90);
        CheckGraund();
    }

    private void CheckGraund()
    {
        Collider2D[] collider1 = Physics2D.OverlapCircleAll(transform.position, 0.02f);


        if (collider1.Length > 1 && collider1.All(x => !x.GetComponent<PlayerControls>()) && collider1.All(x => !x.GetComponent<Enemy>()) && collider1.All(x => !x.GetComponent<Bullet>()))
        {
            DestroyGameObject(effect);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerControls player = collision.GetComponent<PlayerControls>();

        if(player != null)
        {
            player.RessiveDamage(10);
            Destroy(gameObject);
        }
    }
}

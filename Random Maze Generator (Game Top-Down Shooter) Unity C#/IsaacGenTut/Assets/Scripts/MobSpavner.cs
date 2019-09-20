using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpavner : MonoBehaviour {

    public Transform[] points;
    public GameObject[] enemy;
    public float timeBefo;

    public GameObject effect;

    public GameObject CloseEnamy;
    public GameObject ShootbleEnamy;
    public bool closeEnamy;
    public bool shotbleEnamy;
	void Start () {
		
	}

    float time;
	void Update () {
        time += Time.deltaTime;

        if (time > timeBefo)
        {
            Spawn();
        }

	}

    void Spawn()
    {
        if (closeEnamy)
        {
            Create(CloseEnamy, transform);
        }else
        if(shotbleEnamy)
            Create(ShootbleEnamy, points[1]);
        else
            for (int i = 0; i < enemy.Length; i++)
            {

                Create(enemy[i], points[i]);

            }

        Destroy(gameObject);
    }
    void Create(GameObject gameObject, Transform transform)
    {
        Instantiate(gameObject, transform.position, gameObject.transform.rotation);
        GameObject Go = Instantiate(effect, transform.position, effect.transform.rotation);
        Destroy(Go, 1);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public GameObject bomb;
    public GameObject boomBomb;
    public GameObject shilt;
    public GameObject slime;
    public GameObject Effect;

    public void DestroyGameObgect()
    {

        int rand = Random.Range(0, 15);
        GameObject gifted = null ;
        if (rand == 1)
            gifted = boomBomb;
        else if (rand == 2)
            gifted = boomBomb;
        else if (rand == 3)
            gifted = boomBomb;
        else if (rand == 9)
            gifted = shilt;
        else if (rand == 11)
            gifted = bomb;
        else if (rand == 6)
            gifted = slime;

        if(gifted!= null)
            Instantiate(gifted, transform.position, gifted.transform.rotation);
        Instantiate(Effect, transform.position, Effect.transform.rotation);

        Destroy(gameObject);
    }

}

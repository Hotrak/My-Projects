using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task {


    public int cloaseEnamyCount = 0;
    public int shotbleEnamyCount = 0;
    public int bossEnamyCount = 0;
    public bool isBoss;


    public bool CheckValue()
    {
        if(cloaseEnamyCount <= 0 && shotbleEnamyCount <= 0 && bossEnamyCount <= 0)
            return true;

        return false;
    }
    public void Minus(Enemy enemy)
    {
        if (enemy is CloseEnemy)
            cloaseEnamyCount--;
        if (enemy is ShotobleEnamy)
            shotbleEnamyCount--;
        if (enemy is Boss)
            bossEnamyCount--;
    }


}

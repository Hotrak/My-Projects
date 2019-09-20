using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSpavner : MonoBehaviour {

    public int shiltCount;
    public int bombCount;
    public int slimeCount;

    public int boxCount;

    public int closeEnamyCount;
    public int shotableEnamyCount;

    public bool boss;



    public Items[,] GenerateCell()
    {
        Items[,] matrix = new Items[8, 18];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 18; j++)
            {
                matrix[i, j] = Items.empty;
            }
        }
        matrix = GetBox(matrix);
        matrix = GetMobs(matrix);


        return matrix;

    }

    private Items[,] GetBox(Items[,] matrix)
    {
        int count = 0;


        if (boxCount > 1)
        {
            while (true)
            {
                int x = Random.Range(0, 8);
                int y = Random.Range(0, 18);
                //Debug.Log("X: " + x + " Y:" + y + " Lengs0 :" + matrix.GetLength(0) + " Lengs1 :" + matrix.GetLength(1));

                if (matrix[x, y] != Items.box)
                    if (ChecValue(x, y))
                    {
                        if (CheckBorder(x, y))
                        {
                            count++;
                            matrix[x, y] = Items.box;
                            if (count >= boxCount)
                                break;
                        }
                        else
                        {
                            if (count != boxCount)
                            {
                                if (y == 1 || y == 16)
                                {
                                    if (y > 9)
                                    {
                                        matrix[x, y - 1] = Items.box;
                                        matrix[x, y] = Items.box;

                                    }
                                    else
                                    {
                                        matrix[x, y + 1] = Items.box;
                                        matrix[x, y] = Items.box;
                                    }
                                }
                                else
                                {
                                    if (x > 3)
                                    {
                                        matrix[x - 1, y] = Items.box;
                                        matrix[x, y] = Items.box;

                                    }
                                    else
                                    {
                                        matrix[x + 1, y] = Items.box;
                                        matrix[x, y] = Items.box;
                                    }
                                }


                                count += 2;
                                if (count >= boxCount)
                                    break;

                            }
                        }

                    }

            }
        }
        else
        {
            int x = Random.Range(0, 8);
            int y = Random.Range(0, 18);
            if (matrix[x, y] != Items.box)
                if (ChecValue(x, y))
                {
                    matrix[x, y] = Items.box;
                }
        }
        return matrix;


    }
    private Items[,] GetMobs(Items[,] matrix)
    {
        if (closeEnamyCount > 0)
        {
            int count = 0;
            while (true)
            {
                int x = Random.Range(0, 8);
                int y = Random.Range(0, 18);
                //Debug.Log("X: " + x + " Y:" + y + " Lengs0 :" + matrix.GetLength(0) + " Lengs1 :" + matrix.GetLength(1));
                
                if (matrix[x, y] == Items.empty)
                {
                    if (CheckLastBorder(x, y))
                    {
                        count++;
                        matrix[x, y] = Items.closeEnemy;
                    }
                }
                if (count >= closeEnamyCount)
                {
                    break;
                }

            }
        }

        if (shotableEnamyCount > 0)
        {
            int count = 0;
            while (true)
            {
                int x = Random.Range(0, 8);
                int y = Random.Range(0, 18);
                //Debug.Log("X: " + x + " Y:" + y + " Lengs0 :" + matrix.GetLength(0) + " Lengs1 :" + matrix.GetLength(1));

                if (matrix[x, y] == Items.empty)
                {
                    if (CheckLastBorder(x, y))
                    {
                        count++;
                        matrix[x, y] = Items.shotbleEnemy;
                    }
                }
                if (count >= shotableEnamyCount)
                {
                    break;
                }

            }
        }

        return matrix;
    }
    private bool ChecValue(int x, int y)
    {
        if ((x == 0 && y == 10) || (x == 0 && y == 11) || (x == 0 && y == 12) || (x == 0 && y == 13))
            return false;
        else
        if ((x == 1 && y == 10) || (x == 1 && y == 11) || (x == 1 && y == 12) || (x == 1 && y == 13))
            return false;
        else
        if ((x == 5 && y == 17) || (x == 4 && y == 17) || (x == 3 && y == 17) || (x == 2 && y == 17))
            return false;
        else
        if ((x == 5 && y == 16) || (x == 4 && y == 16) || (x == 3 && y == 16) || (x == 2 && y == 16))
            return false;


        else
        if ((x == 7 && y == 10) || (x == 7 && y == 11) || (x == 7 && y == 12) || (x == 7 && y == 13))
            return false;
        else
        if ((x == 6 && y == 10) || (x == 6 && y == 11) || (x == 6 && y == 12) || (x == 6 && y == 13))
            return false;

        else
        if ((x == 2 && y == 1) || (x == 3 && y == 1) || (x == 4 && y == 1) || (x == 5 && y == 1))
            return false;
        else
        if ((x == 2 && y == 0) || (x == 3 && y == 0) || (x == 4 && y == 0) || (x == 5 && y == 0))
            return false;

        return true;
    }
    private bool CheckBorder(int x,int y)
    {
        if ((x == 1 && y >= 1) || (x == 16 && y >= 1))
            return false;
        if ((x >= 1 && y == 6) || (x >= 1 && y == 1))
            return false;

        return true;
    }
    private bool CheckLastBorder(int x, int y)
    {

        if (x ==0 || y==0 || x==7 || y ==17 )
            return false;

        return true;
    }
}

public enum Items
{
    empty,bomm,box,shilt,slime,closeEnemy,shotbleEnemy
}

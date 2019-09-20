using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpawnNote : MonoBehaviour
{


    public Node[,] notes;
    // Use this for initialization
    void Start()
    {
        compliteNotes = new List<Node>();
        //templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();

        //GenereteMaze();

        //Invoke("Destroy",5.0f);


    }

    bool isCreate;
    // Update is called once per frame
    void Update()
    {
        if (!isCreate)
        {

            //CreateMaze();
            //isCreate = true;

            //for (int l = 0; l < hag; l++)
            //{
            //    Debug.Log(notes[l, 0].left + " " + notes[l, 0].top + " " + notes[l, 0].bottom + " " + notes[l, 0].right + " | " +
            //        notes[l, 1].left + " " + notes[l, 1].top + " " + notes[l, 1].bottom + " " + notes[l, 1].right + " | " +
            //        notes[l, 2].left + " " + notes[l, 2].top + " " + notes[l, 2].bottom + " " + notes[l, 2].right);
            //}
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    void GenereteMaze()
    {
        notes = new Node[hag, hag];
        for (int ih = 0; ih < hag; ih++)
            for (int jh = 0; jh < hag; jh++)
            {
                notes[ih, jh] = new Node();
            }
        int i =0 ;
        int j = 0;
        for (int c = 0; c < hag * hag; c++)
        {
            for (int r = 0; r < 15; r++)
            {
                if (notes[i, j].GetSize() == 4)
                {
                    Debug.Log("Тупик");
                    for (int x = compliteNotes.Count - 1; x > 0; x--)
                    {
                        if (compliteNotes[x].GetSize() != 4)
                        {
                            notes[i, j].block = true;
                            i = compliteNotes[i].i;
                            j = compliteNotes[i].j;

                            break;

                        }

                    }
                    break;

                }
                int rand = Random.Range(1, 5);
                int result = Check(i, j, rand);
                if (result == 1)
                {
                    if (rand == 1)
                    {
                        notes[i, j].left = 1;
                        notes[i, j].i = i;
                        notes[i, j].j = j;
                        compliteNotes.Add(notes[i, j]);
                        j -= 1;
                        notes[i, j].right = 1;

                    }
                    if (rand == 2)
                    {
                        notes[i, j].top = 1;
                        notes[i, j].i = i;
                        notes[i, j].j = j;
                        compliteNotes.Add(notes[i, j]);

                        i -= 1;
                        notes[i, j].bottom = 1;

                    }
                    if (rand == 3)
                    {
                        notes[i, j].right = 1;
                        notes[i, j].i = i;
                        notes[i, j].j = j;
                        compliteNotes.Add(notes[i, j]);
                        j += 1;
                        notes[i, j].left = 1;

                    }
                    if (rand == 4)
                    {
                        notes[i, j].bottom = 1;
                        notes[i, j].i = i;
                        notes[i, j].j = j;
                        compliteNotes.Add(notes[i, j]);
                        i += 1;
                        notes[i, j].top = 1;

                    }
                    //for (int ror = 1; ror < 5; ror++)
                    //{
                    //    CheckLast(i,j, ror);

                    //}
                    Debug.Log("C: " + c + "X : " + i + " Y : " + j + " R : " + rand);
                    //Debug.Log("Тупик");
                    break;
                }
                //if (notes[i,j].size ==4)
                //{
                //    Debug.Log("Тупик");
                //    for (int x = compliteNotes.Count-1; x>0 ;x--)
                //    {
                //        if (compliteNotes[i].GetSize()!=4)
                //        {
                //            i = compliteNotes[i].i;
                //            j = compliteNotes[i].j;
                //            break;

                //        }
                //    }

                //}

            }
        }


    }

    int hag = 5;
    // 1 - да
    // 0 - нет
    // 2 - Нечего
    int Check(int i, int j, int rand)
    {

        if (i == 0)
        {
            if (rand == 2)
            {
                notes[i, j].top = 2;
                return 0;
            }
        }

        if (i == hag - 1)
        {
            if (rand == 4)
            {
                notes[i, j].bottom = 2;
                return 0;
            }
        }

        if (j == 0)
        {
            if (rand == 1)
            {
                notes[i, j].left = 2;
                return 0;

            }
        }

        if (j == hag - 1)
        {
            if (rand == 3)
            {
                notes[i, j].right = 2;
                return 0;
            }

        }


        // 2
        if (rand == 1 && notes[i, j - 1].right == 1)
        {

            notes[i, j].left = 1;
            return 0;
        }
        else
        if (rand == 2 && notes[i - 1, j].bottom == 1)
        {

            notes[i, j].top = 1;
            return 0;
        }
        else
        if (rand == 3 && notes[i, j + 1].left == 1)
        {

            notes[i, j].right = 1;
            return 0;
        }
        else
        if (rand == 4 && notes[i + 1, j].top == 1)
        {

            notes[i, j].bottom = 1;
            return 0;
        }
        else
            return 1;
    }
    int CheckLast(int i, int j, int rand)
    {


        if (i == 0)
        {
            if (rand == 2)
            {
                notes[i, j].top = 2;
                return 0;
            }
        }

        if (i == 4)
        {
            if (rand == 4)
            {
                notes[i, j].bottom = 2;
                return 0;
            }
        }

        if (j == 0)
        {
            if (rand == 1)
            {
                notes[i, j].left = 2;
                return 0;

            }
        }

        if (j == 4)
        {
            if (rand == 3)
            {
                notes[i, j].right = 2;
                return 0;
            }

        }

        // 2
        if (rand == 1 && notes[i, j - 1].right == 1)
        {
            notes[i, j].left = 2;

            return 1;
        }
        else
        if (rand == 2 && notes[i - 1, j].bottom == 1)
        {
            notes[i, j].top = 2;
            return 1;
        }
        else
        if (rand == 3 && notes[i, j + 1].left == 1)
        {
            notes[i, j].right = 2;
            return 1;
        }
        else
        if (rand == 4 && notes[i + 1, j].top == 1)
        {
            notes[i, j].bottom = 2;
            return 1;
        }

        return 3;
    }

    List<Node> compliteNotes;
    private RoomTemplates templates;
    void CreateMaze()
    {
        Node[,] NoteTr = new Node[hag, hag];

        for (int i = 0; i < hag; i++)
        {
            for (int j = 0; j < hag; j++)
            {
                NoteTr[i, j] = notes[j, i];
            }
        }

        Node[,] NotePer = new Node[hag, hag];

        for (int i = 0; i < hag; i++)
        {
            int h = 0;
            for (int j = hag - 1; j >= 0; j--)
            {
                NotePer[i, h] = NoteTr[i, j];
                h++;
            }
        }

        Vector2 vec = new Vector2();

        for (int i = 0; i < hag; i++)
            for (int j = 0; j < hag; j++)
            {
                vec.x = i * 10;
                vec.y = j * 10;


                if (NotePer[i, j].block == true)
                    Instantiate(templates.block, vec, templates.b.transform.rotation);


                if (NotePer[i, j].right == 1 && NotePer[i, j].bottom == 1 && NotePer[i, j].left == 1)
                    Instantiate(templates.rbl, vec, templates.rbl.transform.rotation);
                else
               if (NotePer[i, j].right == 1 && NotePer[i, j].top == 1 && NotePer[i, j].left == 1)
                    Instantiate(templates.rtl, vec, templates.rtl.transform.rotation);
                else
                if (NotePer[i, j].left == 1 && NotePer[i, j].top == 1 && NotePer[i, j].bottom == 1)
                {
                    //templates.ltb.name =  ""+ NotePer[i, j].i+ " "+ NotePer[i, j].j + " "+templates.ltb.name;
                    Instantiate(templates.ltb, vec, templates.ltb.transform.rotation);

                }
                else

               if (NotePer[i, j].left == 1 && NotePer[i, j].bottom == 1)
                    Instantiate(templates.lb, vec, templates.lb.transform.rotation);
                else
               if (NotePer[i, j].top == 1 && NotePer[i, j].bottom == 1)
                    Instantiate(templates.tb, vec, templates.tb.transform.rotation);
                else
               if (NotePer[i, j].right == 1 && NotePer[i, j].bottom == 1)
                    Instantiate(templates.rb, vec, templates.rb.transform.rotation);
                else
               if (NotePer[i, j].left == 1 && NotePer[i, j].bottom == 1)
                    Instantiate(templates.lb, vec, templates.lb.transform.rotation);
                else
               if (NotePer[i, j].top == 1 && NotePer[i, j].left == 1)
                    Instantiate(templates.tl, vec, templates.tl.transform.rotation);
                else
               if (NotePer[i, j].top == 1 && NotePer[i, j].right == 1)
                    Instantiate(templates.tr, vec, templates.tr.transform.rotation);
                else
               if (NotePer[i, j].left == 1 && NotePer[i, j].right == 1)
                    Instantiate(templates.lr, vec, templates.lr.transform.rotation);
                else
               if (NotePer[i, j].bottom == 1)
                {
                    Instantiate(templates.b, vec, templates.b.transform.rotation);

                }
                else
               if (NotePer[i, j].top == 1)
                {
                    Instantiate(templates.t, vec, templates.t.transform.rotation);

                }
                else
               if (NotePer[i, j].right == 1)
                {
                    Instantiate(templates.r, vec, templates.r.transform.rotation);

                }
                else
               if (NotePer[i, j].left == 1)
                {
                    Instantiate(templates.l, vec, templates.l.transform.rotation);
                }

            }
        for (int i = 0; i < hag; i++)
        {
            for (int j = 0; j < hag; j++)
                if (NotePer[i, j].i == 0 && NotePer[i, j].j == 0)
                    Instantiate(templates.start, vec, templates.l.transform.rotation);
        }

    }

}

public class Node
{
    public int size = 4;

    public int i;
    public int j;

    public int bottom;
    public int right;
    public int left;
    public int top;

    public bool block;

    public int GetSize()
    {
        int size = 0;
        if (bottom != 0)
            size++;
        if (right != 0)
            size++;
        if (left != 0)
            size++;
        if (top != 0)
            size++;
        return size;
    }
}
// 0 - не изветно
// 1 - заполненный проход
// 2 - стена


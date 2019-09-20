using System.Collections;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public GameObject leftDor;
    public GameObject bottomDor;

    public GameObject WallLeft;
    public GameObject WallBottom;

    public GameObject WallLeftFull;
    public GameObject WallBottomFull;

    public GameObject LiftAngel;
    public GameObject BottomAngel;

    public Transform CameraPoint;

    public GameObject Layer1;
    public GameObject Layer2;
    public GameObject Layer3;
    public GameObject Layer4;

    private GameObject box0;
    private GameObject box1;
    private GameObject box2;
    private GameObject box3;
    public GameObject mobSpavner;

    public GameObject boss;

   

    public int id;
    public float[]sellX;
    public float[] sellY;
    public Items[,] items;
    int count = 0;

    public bool Exit;
    public bool isStart;
    public bool Shop;

    public int x;
    public int y;

    public GameObject shopObg;
    public GameObject lernObj;

    private void Start()
    {
        box0 = Resources.Load("SloyBox0") as GameObject;
        box1 = Resources.Load("SloyBox1") as GameObject;
        box2 = Resources.Load("SloyBox2") as GameObject;
        box3 = Resources.Load("SloyBox3") as GameObject;

        Layer1 = Resources.Load("Layer1") as GameObject;
        Layer2 = Resources.Load("Layer2") as GameObject;
        Layer3 = Resources.Load("Layer3") as GameObject;
        Layer4 = Resources.Load("Layer4") as GameObject;

    }
    public void Awake()
    {
        shopObg = Resources.Load("Shop") as GameObject;
    }

    public void ChengeStateDors(bool state = false)
    {
        if(leftDor!=null)
            leftDor.SetActive(state);
        if (bottomDor != null)
            bottomDor.SetActive(state);
    }
    
    CellSpavner cellSpavner;
    public IEnumerator CreateLayer()
    {
        CellSpavner cellSpavner = new CellSpavner();
        MazeManager mazeManager = FindObjectOfType<MazeManager>();
        cellSpavner.boxCount = 5;

        cellSpavner.closeEnamyCount = Random.Range(mazeManager.closeEnamyCountMin, mazeManager.closeEnamyCountMax);
        cellSpavner.shotableEnamyCount = Random.Range(mazeManager.shotbleEnamyCountMin, mazeManager.shotbleEnamyCountMax);

        Task task = new Task();
        task.bossEnamyCount = 0;
        task.cloaseEnamyCount = cellSpavner.closeEnamyCount;
        task.shotbleEnamyCount = cellSpavner.shotableEnamyCount;

        FindObjectOfType<PlayerControls>().task = task;

        items = cellSpavner.GenerateCell();

        float x = 0;
        float y = 0;

        float coafX = 2.832f;
        float coafY = 3.112f;
        
        x = transform.position.x;
        y = transform.position.y;

        for (int i = 0;i<8;i++)
        {
            for (int j = 0; j < 18; j++)
            {
                GameObject Go = Instantiate(GetLayer(), new Vector2(coafX + x, coafY+ y), Quaternion.identity);
                Go.transform.SetParent(gameObject.transform);

                if (items[i, j] == Items.box)
                {
                    int rand = Random.Range(0,4);
                    GameObject box = null;
                    if (rand == 0)
                        box = box0;
                    else if (rand == 1)
                        box = box1;
                    else if (rand == 2)
                        box = box2;
                    else if (rand == 3)
                        box = box3;

                    GameObject Go2 = Instantiate(box, new Vector2(coafX + x, coafY + y), Quaternion.identity);
                    Go2.GetComponent<SpriteRenderer>().sortingOrder -= count;
                    count++;
                    Go2.transform.SetParent(gameObject.transform);
                }
                else
                if (items[i, j] == Items.closeEnemy)
                {
                    mobSpavner.GetComponent<MobSpavner>().closeEnamy = true;
                    mobSpavner.GetComponent<MobSpavner>().shotbleEnamy = false;
                    GameObject Go2 = Instantiate(mobSpavner, new Vector2(coafX + x, coafY + y), Quaternion.identity);
                }
                else
                if (items[i, j] == Items.shotbleEnemy)
                {
                    mobSpavner.GetComponent<MobSpavner>().shotbleEnamy = true;
                    mobSpavner.GetComponent<MobSpavner>().closeEnamy = false;
                    GameObject Go2 = Instantiate(mobSpavner, new Vector2(coafX + x, coafY + y), Quaternion.identity);
                }

                coafX += 1.25f;

            }
            coafX = 2.832f;
            coafY += 1.284f;
        }

        yield break;
    }
    public GameObject GetLayer()
    {
        if (Layer1 ==null)
        {
            Layer1 = Resources.Load("Layer1") as GameObject;
            Layer2 = Resources.Load("Layer2") as GameObject;
            Layer3 = Resources.Load("Layer3") as GameObject;
            Layer4 = Resources.Load("Layer4") as GameObject;
        }
        int rand = Random.Range(0, 9);
        GameObject Lauar = null;
        if (rand == 0)
            Lauar = Layer1;
        else if (rand == 1)
            Lauar = Layer2;
        else if (rand == 2)
            Lauar = Layer3;
        else if (rand == 3)
            Lauar = Layer1;
        else if (rand == 4)
            Lauar = Layer1;
        else if (rand == 5)
            Lauar = Layer1;
        else if (rand == 6)
            Lauar = Layer1;
        else if (rand == 7)
            Lauar = Layer1;
        else if (rand == 8)
            Lauar = Layer1;
        return Lauar;
    }
    public IEnumerator CreateEmptyLayer()
    {

        float x = 0;
        float y = 0;

        float coafX = 2.832f;
        float coafY = 3.112f;

        sellX = new float[18];
        sellY = new float[8];

        x = transform.position.x;
        y = transform.position.y;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 18; j++)
            {

                GameObject Go = Instantiate(GetLayer(), new Vector2(coafX + x, coafY + y), Quaternion.identity);
                Go.transform.SetParent(gameObject.transform);

                sellX[j] = coafX + x;

                coafX += 1.25f;


            }

            sellY[i] = coafY + y;

            coafX = 2.832f;
            coafY += 1.284f;
        }

        if (Exit)
        {


            Task task = new Task();
            task.isBoss = true;

            if (FindObjectOfType<MazeManager>().mazeLvl != 0)
            {
                task.bossEnamyCount = 1;
                Instantiate(boss, new Vector2(sellX[9], sellY[4]), boss.transform.rotation);
            } 
            FindObjectOfType<PlayerControls>().task = task;


        }
        else if (isStart)
        {
            
            FindObjectOfType<CameraController>().DeleteDors(id);
            ChengeStateDors();
            GameObject Go = Instantiate(lernObj, new Vector2(5.48f + x, 7.12f + y), Quaternion.identity);


        }
        else if (Shop)
        {
            Debug.Log("SHOP IS CREATED");
            GameObject Go = Instantiate(shopObg, new Vector2(15.92f + x, 5.54f + y), Quaternion.identity);
            ChengeStateDors();
            //Go.transform.SetParent(gameObject.transform);
        }
        

        yield break;
    }

    //public void WallLeftDelete( bool isDalete)
    //{
    //    WallLeft.SetActive(isDalete);
    //    LiftAngel.SetActive(!isDalete);

    //}

    //public void WallBottomDelete(bool isDalete)
    //{
    //    WallBottom.SetActive(isDalete);
    //    BottomAngel.SetActive(!isDalete);
    //}
}
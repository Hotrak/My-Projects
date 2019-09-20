using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField]
    public float speed = 10F;
    [SerializeField]
    private Transform target;

    public Cell[] cells;
    bool[] isCreateLayer;
    bool[] isDoneLayer;
    // Use this for initialization

    public float dist;
    PlayerControls player;
    void Start() {
        //if (!target) target = FindObjectOfType<PlayerControls>().transform;

        player = FindObjectOfType<PlayerControls>();
    }
    bool isFind;

    public void FindStart()
    {
        foreach (Cell cell in cells)
        {
            if (cell.isStart)
            {
                Vector3 position = cell.CameraPoint.transform.position;
                position.z = 0;
                player.transform.position = position;
                break;
            }
        }
    }

    int nowMin = -1;
    int min;
    void Update() {

        if (!isFind)
        {
            isFind = true;
            FindCells();
            FindStart();
        }

        //Vector3 position = target.position; position.z = -10.0F;
        //position.y = position.y + 0.7f;
        min = ColculeteDistans();
        if (min != nowMin)
        {
            if (cells[min].isStart || cells[min].Shop)
            {
                DeleteDors(cells[min].id);
                cells[min].ChengeStateDors();
                isDoneLayer[min] = true;
            }else
            player.AftomatickMove(cells[min].CameraPoint);
        }
        nowMin = min;
        transform.position = Vector3.Lerp(transform.position, cells[min].CameraPoint.position, speed * Time.deltaTime);

        //dist = Vector3.Distance(player.transform.position, transform.position);
        if (player.task != null)
        {

            if (player.task.CheckValue())
            {
                if (!isDoneLayer[min])
                {
                    DeleteDors(cells[min].id);
                    cells[min].ChengeStateDors();
                    isDoneLayer[min] = true;
                    if (player.task.isBoss)
                    {
                        Instantiate(finishObj, new Vector2(cells[min].sellX[8], cells[min].sellY[4]), finishObj.transform.rotation);
                    }
                }
                
                
            }
        }
    }
    public GameObject finishObj;

    void FindCells()
    {
        cells = FindObjectsOfType<Cell>();
        isCreateLayer = new bool[cells.Length];
        isDoneLayer = new bool[cells.Length];
    }
    float[] dists;
    int ColculeteDistans()
    {
        dists = new float[cells.Length];
        int min = 0;
        for (int i =0;i<cells.Length;i++)
        {
            dists[i] = Vector3.Distance(player.transform.position, cells[i].CameraPoint.position);
            if (dists[min] > dists[i])
            {
                min = i;
            }
        }
        if (cells[min].WallBottomFull && cells[min].WallLeftFull&& !isCreateLayer[min])
        {
            isCreateLayer[min] = true;

            if (cells[min].Exit)
            {
                StartCoroutine(cells[min].CreateEmptyLayer());
                Boss boss = FindObjectOfType<Boss>();
                if (boss!=null)
                    boss.cell = cells[min];
                return min;
            }

            if (!cells[min].isStart&& !cells[min].Shop)
            {
                StartCoroutine(cells[min].CreateLayer());
                return min;
            }
            else
            {
                StartCoroutine(cells[min].CreateEmptyLayer());
            }

            
            //
        }

        return min;
    }
    public void CreateDors()
    {
        if (!isDoneLayer[min])
        {
            DeleteDors(cells[min].id, true);
            cells[min].ChengeStateDors(true);
            Debug.Log("CREATE IN "+ cells[min].id);

        }else
            Debug.Log("NO_CREATE IN " + cells[min].id);
    }
    public void DeleteDors(int id,bool state = false)
    {
        int Height = FindObjectOfType<MazeManager>().Height;
        for (int i = 0; i < cells.Length; i++)
        {
            if (cells[i].id == id + 1)
                cells[i].leftDor.SetActive(state);
            else
                if (cells[i].id == id + Height)
                cells[i].bottomDor.SetActive(state);

        }

    }
   
}

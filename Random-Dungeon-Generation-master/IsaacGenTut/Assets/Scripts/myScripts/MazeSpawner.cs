using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeSpawner : MonoBehaviour
{
    public Cell activeCellPrefab;
    public Cell CellPrefab1;
    public Cell CellPrefab2;
    public Cell CellPrefab3;
    public Cell CellPrefab4;
    MazeGeneratorCell[,] maze;
    MazeManager mazeManager;

    private void Awake()
    {

        //CellPrefab1= Resources.Load("Node1") as Cell;
        //CellPrefab2= Resources.Load("Node1") as Cell;
        int rand = Random.Range(0,4);
        if (rand == 0)
            activeCellPrefab = CellPrefab1;
        else if (rand == 1)
            activeCellPrefab = CellPrefab2;
        else if (rand == 2)
            activeCellPrefab = CellPrefab3;
        else if (rand == 3)
            activeCellPrefab = CellPrefab4;

    }
    public void SpawnMaze()
    {
        mazeManager = FindObjectOfType<MazeManager>();
        MazeGenerator generator = new MazeGenerator();
        generator.Width = mazeManager.Width;
        generator.Height = mazeManager.Height;

        generator.StartPointX = mazeManager.startPointX;
        generator.StartPointY = mazeManager.startPointY;

        maze = generator.GenerateMaze();

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                //Cell c = Instantiate(CellPrefab, new Vector2(x*10*2 * 1.36f, y*10*2), Quaternion.identity);
                Maze mazeObj = new Maze(x, y);
                StartCoroutine(Spawn(mazeObj));

            }
        }
    }
    int id = 0;
    public IEnumerator Spawn(Maze mazeObj)
    {

        int x = mazeObj.i;
        int y = mazeObj.j;

        //Cell c = Instantiate(CellPrefab, new Vector2(x * 10 * 2 * 1.6f, y * 10 * 2 / 1.315f), Quaternion.identity);
        Cell c = Instantiate(activeCellPrefab, new Vector2(x * 10 * 2 * 1.37f, y * 10 * 2 / 1.315f), Quaternion.identity);
        c.id = id;
        id++;

        c.x = x;
        c.y = y;
        c.Exit = maze[x, y].Exit;
        c.isStart = maze[x, y].Start;
        c.Shop = maze[x, y].Shop;

        c.WallLeftFull.SetActive(maze[x, y].WallLeftFull);
        c.WallBottomFull.SetActive(maze[x, y].WallBottomFull);

        if (c.LiftAngel != null)
            c.LiftAngel.SetActive(!maze[x, y].WallLeft);
        if (c.BottomAngel != null)
            c.BottomAngel.SetActive(!maze[x, y].WallBottom);

        c.WallLeft.SetActive(maze[x, y].WallLeft);
        c.WallBottom.SetActive(maze[x, y].WallBottom);

       
        yield break;
    }
  
}

public class Maze
{

    public int i;
    public int j;

    public Maze(  int i, int j)
    {
        this.i = i;
        this.j = j;
    }
}
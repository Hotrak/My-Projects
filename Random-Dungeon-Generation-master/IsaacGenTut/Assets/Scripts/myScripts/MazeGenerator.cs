using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell
{
    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool WallLeftFull = true;
    public bool WallBottomFull = true;

    public bool Visited = false;
    public int DistanceFromStart;

    public bool Exit;

    public bool Start;
    public bool Shop;

}
public class MazeGenerator  
{
    public int Width = 5;
    public int Height = 5;

    public int StartPointX;
    public int StartPointY;

    public MazeGeneratorCell[,] GenerateMaze()
    {
        MazeGeneratorCell[,] maze = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                maze[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            maze[x, Height - 1].WallLeftFull = false;
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            maze[Width - 1, y].WallBottomFull = false;
        }

        RemoveWallsWithBacktracker(maze);

        PlaceMazeExit(maze);
        PlaceMazeShop(maze);
        return maze;
    }

    private void PlaceMazeShop(MazeGeneratorCell[,] maze)
    {

        while (true)
        {
            System.Random random = new System.Random();
            int x = new System.Random().Next(0,Width-1);
            int y = new System.Random().Next(0,Height-1);

            if (!maze[x, y].Exit && !maze[x, y].Start)
            {
                maze[x, y].Shop = true;
                break;
            }

        }

    }

    private void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell current = maze[StartPointX, StartPointY];
        current.Start = true;
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                current = chosen;
                chosen.DistanceFromStart = stack.Count;
            }
            else
            {
                current = stack.Pop();
            }
        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }

    private void PlaceMazeExit(MazeGeneratorCell[,] maze)
    {
        MazeGeneratorCell furthest = maze[StartPointX, StartPointY];

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Height - 2];
            if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
        }

        for (int y = 0; y < maze.GetLength(1); y++)
        {
            if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 2, y];
            if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
        }

        if (furthest.X == 0)
        {
            //furthest.WallLeft = false;
            furthest.Exit = true;
            Debug.Log("hi 1");
        }
        else if (furthest.Y == 0)
        {
            //furthest.WallBottom = false;
            furthest.Exit = true;
            Debug.Log("hi 2");

        }
        else if (furthest.X == Width - 2)
        {
            //maze[furthest.X + 1, furthest.Y].WallLeft = false;
            maze[furthest.X, furthest.Y].Exit = true;
            Debug.Log("hi 3");

        }
        else if (furthest.Y == Height - 2)
        {
            //maze[furthest.X, furthest.Y + 1].WallBottom = false;
            maze[furthest.X, furthest.Y].Exit = true;
            Debug.Log("hi 4");

        }
    }

}

//if (furthest.X == 0) furthest.WallLeft = false;
//        else if (furthest.Y == 0) furthest.WallBottom = false;
//        else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
//        else if (furthest.Y == Height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;
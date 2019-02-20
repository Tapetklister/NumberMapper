using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public TileFactory tileFactory;
    public int xSize, ySize;
    public Vector2 tileValueRange;

    public Canvas boardCanvas;

    [HideInInspector] public Vector2 boardWorldSize;
    [HideInInspector] public Vector3 tileSize;

    Vector2[,] tilePositions;
    Tile[,] tiles;

    Vector3 origo;

    public int leftPadding, topPadding, rightPadding, bottomPadding;

    List<Tile> path = new List<Tile>();

    Tile startTile;

    public delegate void EndReached();
    public static event EndReached OnEndReached;

    private void Awake()
    {
        SetSize();
        //Generate(xSize, ySize);
        //UpdatePath();
    }

    private void OnEnable()
    {
        Awake();
    }

    private void SetSize()
    {
        origo = new Vector3(leftPadding - rightPadding, topPadding - bottomPadding, 0.0f);
        boardWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width - (leftPadding + rightPadding), Screen.height - (topPadding + bottomPadding))) * 2;
        tileSize = new Vector3(boardWorldSize.x / xSize, boardWorldSize.y / ySize, 1.0f);
    }

    public void Generate(int xSize, int ySize)
    {
        if (xSize <= 0.0f || ySize <= 0.0f)
        {
            throw new Exception("Grid's X and Y size can't be less than 1");
        }

        if (boardWorldSize == null)
        {
            throw new Exception("Board world size not set in Grid.cs");
        }

        if (boardWorldSize.x <= 0 || boardWorldSize.y <= 0)
        {
            throw new Exception("Screen size's X and Y values must be higher than 0");
        }

        tilePositions = new Vector2[xSize, ySize];
        tiles = new Tile[xSize, ySize];
        int startX = UnityEngine.Random.Range(0, xSize);
        int endX = UnityEngine.Random.Range(0, xSize);

        for (int i = 0, x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++, i++)
            {
                var type = ETileType.Base;

                if (x == startX && y == 0)
                {
                    type = ETileType.Start;
                }
                else if (x == endX && y == ySize - 1)
                {
                    type = ETileType.End;
                }

                tilePositions[x,y] = GenerateTilePosition(x, y, tileSize, boardWorldSize);
                tiles[x,y] = tileFactory.CreateAndInitializeTile(tilePositions[x, y], tileSize, transform, (int)UnityEngine.Random.Range(tileValueRange.x, tileValueRange.y), x, y, type);
                tiles[x, y].clickedEvent += UpdatePath;
                
                if (tiles[x,y].type == ETileType.Start)
                {
                    startTile = tiles[x, y];
                    path.Add(tiles[x, y]);
                }
            }
        }

        UpdatePath();
    }

    Vector3 GenerateTilePosition(int x, int y, Vector3 tileSize, Vector2 boardWorldSize)
    {
        return new Vector3(tileSize.x * x - boardWorldSize.x * 0.5f + tileSize.x * 0.5f, tileSize.y * y - boardWorldSize.y * 0.5f + tileSize.y * 0.5f, 0);
    }

    void UpdatePath()
    {
        ////// I don't like this. Plz fix //////
        foreach(var t in path)
        {
            t.SetColors(false);
        }
        ////////////////////////////////////////

        var oldPath = path;
        path.Clear();
        var localPath = new List<Tile>()
        {
            startTile
        };

        for (int i = 0; i < localPath.Count; ++i)
        {
            var newEntries = GetConnectedNeighbours(localPath, i);
            //foreach(var t in newEntries)
            //{
            //    t.SetColors(true);
            //}
            localPath.AddRange(newEntries);
        }

        if (localPath.Any(t => t.type == ETileType.End))
        {
            if (OnEndReached != null)
            {
                OnEndReached();
            }
        }

        //var removedFromPath = oldPath.Where(t => !localPath.Contains(t)).ToList();

        foreach (var t in localPath)
        {
            t.SetColors(true);
        }

        //foreach (var t in removedFromPath)
        //{
        //    t.SetColors(false);
        //}

        path = localPath;
    }

    List<Tile> GetNeighbours(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();

        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (Mathf.Abs(x) == Mathf.Abs(y))
                {
                    continue;
                }

                int checkX = tile.GridX + x;
                int checkY = tile.GridY + y;

                if (checkX >= 0 && checkX < xSize && checkY >= 0 && checkY < ySize)
                {
                    neighbours.Add(tiles[checkX, checkY]);
                }
            }
        }

        return neighbours;
    }

    List<Tile> GetConnectedNeighbours(List<Tile> path, int index)
    {
        var tile = path[index];
        return GetNeighbours(tile).Where(t => (t.Value == tile.Value + 1 ||
                                               t.Value == 0 && tile.Value == 9)
                                               && !path.Contains(t)).ToList();
    }
}

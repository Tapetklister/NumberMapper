using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public TileFactory tileFactory;
    public int xSize, ySize;
    public Vector2 tileValueRange;

    [HideInInspector] public Vector2 boardWorldSize;
    [HideInInspector] public Vector3 tileSize;

    Vector2[,] tilePositions;
    Tile[] tiles;

    private void Awake()
    {
        SetSize();
        Generate(xSize, ySize);
    }

    private void SetSize()
    {
        boardWorldSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) * 2;
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
        tiles = new Tile[xSize * ySize];
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
                tiles[i] = tileFactory.CreateAndInitializeTile(tilePositions[x, y], tileSize, transform, (int)UnityEngine.Random.Range(tileValueRange.x, tileValueRange.y), type);
            }
        }
    }

    Vector3 GenerateTilePosition(int x, int y, Vector3 tileSize, Vector2 screenSize)
    {
        return new Vector3(tileSize.x * x - screenSize.x * 0.5f + tileSize.x * 0.5f, tileSize.y * y - screenSize.y * 0.5f + tileSize.y * 0.5f, 0);
    }
}

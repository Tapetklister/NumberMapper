﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int xSize, ySize;
    public Tile tilePrefab;
    public Vector2 boardWorldSize;
    public Vector2 tileValueRange;
    public Vector2 tileSize;

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
        tileSize = new Vector2(boardWorldSize.x / xSize, boardWorldSize.y / ySize);
    }

    public void Generate(int xSize, int ySize)
    {
        if (xSize <= 0.0f || ySize <= 0.0f)
        {
            throw new Exception("Grid's X and Y size can't be less than 1");
        }

        if (tilePrefab == null)
        {
            throw new Exception("No tile prefab set in Grid.cs");
        }

        if (boardWorldSize == null)
        {
            throw new Exception("Board world size not set in Grid.cs");
        }

        if (boardWorldSize.x <= 0 || boardWorldSize.y <= 0)
        {
            throw new Exception("Screen size's X and Y values must be higher than 0");
        }

        tilePositions = new Vector2[xSize,ySize];
        tiles = new Tile[xSize * ySize];
        
        for (int i = 0, x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++, i++)
            {
                tilePositions[x,y] = GenerateTilePosition(x, y, tileSize, boardWorldSize);
                AddTile(i, tilePositions[x,y], tileSize);
            }
        }
    }

    private void AddTile(int index, Vector2 worldPosition, Vector2 tileSize)
    {
        tiles[index] = Instantiate(tilePrefab);
        tiles[index].Initialize(worldPosition, tileSize, transform, (int) UnityEngine.Random.Range(tileValueRange.x, tileValueRange.y));
    }

    Vector2 GenerateTilePosition(int x, int y, Vector2 tileSize, Vector2 screenSize)
    {
        return new Vector2(tileSize.x * x - screenSize.x * 0.5f + tileSize.x * 0.5f, tileSize.y * y - screenSize.y * 0.5f + tileSize.y * 0.5f);
    }

    private void OnDrawGizmos()
    {
        if (tilePositions == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        foreach(Vector2 pos in tilePositions)
        {
            Gizmos.DrawSphere(pos, 0.1f);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

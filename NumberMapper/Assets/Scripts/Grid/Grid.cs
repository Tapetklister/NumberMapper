using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int xSize, ySize;
    public Tile tilePrefab;
    public Vector2 screenSize;

    Vector2 tileSize;
    Vector2[] tilePositions;
    Tile[] tiles;

    private void Awake()
    {
        SetSize();
        Generate(xSize, ySize);
    }

    private void SetSize()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) * 2;
        tileSize = new Vector2(screenSize.x / xSize, screenSize.y / ySize);
    }

    public bool Generate(int xSize, int ySize)
    {
        if (xSize <= 0 || ySize <= 0 ||
            tilePrefab == null ||
            screenSize == null ||
            screenSize.x <= 0 || screenSize.y <= 0)
        {
            return false;
        }

        tilePositions = new Vector2[xSize * ySize];
        //tiles = new Tile[xSize * ySize];
        //
        //for (int i = 0, x = 0; x < xSize; x++)
        //{
        //    for (int y = 0; y < ySize; y++, i++)
        //    {
        //        tilePositions[i] = GenerateTilePosition(x, y, tileSize, screenSize);
        //        AddTile(i, tilePositions[i], tileSize);
        //    }
        //}

        return true;
    }

    private void AddTile(int index, Vector2 worldPosition, Vector2 tileSize)
    {
        tiles[index] = Instantiate(tilePrefab);
        tiles[index].Init(worldPosition, tileSize, transform);
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
        for(int i = 0; i < tilePositions.Length; i++)
        {
            Gizmos.DrawSphere(tilePositions[i], 0.1f);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

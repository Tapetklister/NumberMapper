using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using TMPro;

[TestFixture]
public class GridTests
{
    [TestCase(6, 8, true)]
    [TestCase(1, 12, true)]
    public void GeneratesGrid_PositiveNumbers_SuccessfullyCreatesGrid(int x, int y, bool expected)
    {
        GameObject grid = SetupGenericGrid();
        bool result = grid.GetComponent<Grid>().Generate(x, y);
        Assert.That(result, Is.EqualTo(expected));
    }

    [TestCase(-3, 4, false)]
    [TestCase(0, 0, false)]
    [TestCase(0, 12, false)]
    public void GeneratesGrid_NegativeNumbers_FailsToCreateGrid(int x, int y, bool expected)
    {
        GameObject grid = SetupGenericGrid();
        bool result = grid.GetComponent<Grid>().Generate(x, y);
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase(0, 0, 3, 3)]
    public void CreatesTile_PositiveSize_SuccessfullyCreatesTile(float posX, float posY, float sizeX, float sizeY)
    {
        GameObject tile = CreateTile(new Vector2(posX, posY), new Vector2(sizeX, sizeY), null);
        Assert.That(tile.GetComponent<Tile>(), !Is.Null);
    }
    
    GameObject SetupGenericGrid()
    {
        GameObject grid = new GameObject();
        GameObject tile = CreateTile(new Vector2(0, 0), new Vector2(1, 1), grid.transform);
        
        grid.AddComponent<Grid>();
        grid.GetComponent<Grid>().tilePrefab = tile.GetComponent<Tile>();
        grid.GetComponent<Grid>().screenSize = new Vector2(6, 10);

        return grid;
    }
    
    private GameObject CreateTile(Vector2 worldPosition, Vector2 size, Transform parent)
    {
        var tile = new GameObject("tile");
        tile.AddComponent<SpriteRenderer>();
        tile.AddComponent<Tile>();

        var text = new GameObject("text");
        text.transform.parent = tile.transform;
        text.AddComponent<TextMeshPro>();

        tile.GetComponent<Tile>().text = text.GetComponent<TextMeshPro>();

        tile.GetComponent<Tile>().Init(worldPosition, size, parent);
        return tile;
    }
}

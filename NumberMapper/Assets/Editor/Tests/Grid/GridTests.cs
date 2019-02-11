using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using TMPro;
using System;

[TestFixture]
public class GridTests
{
    [TestCase(6, 8)]
    [TestCase(4, 12)]
    public void GeneratesGrid_PositiveNumbers_SuccessfullyCreatesGrid(int x, int y)
    {
        GameObject grid = SetupGenericGrid();
        Assert.DoesNotThrow(() => grid.GetComponent<Grid>().Generate(x, y));
    }

    [TestCase(-3, 4, "Grid's X and Y size can't be less than 1")]
    [TestCase(0, 0, "Grid's X and Y size can't be less than 1")]
    [TestCase(0, 12, "Grid's X and Y size can't be less than 1")]
    public void GeneratesGrid_NegativeNumbers_FailsToCreateGrid(int x, int y, string expected)
    {
        GameObject grid = SetupGenericGrid();
        var result = Assert.Throws<Exception>(() => grid.GetComponent<Grid>().Generate(x,y));
        Assert.That(expected, Is.EqualTo(result.Message));
    }
    
    [TestCase(0, 0, 3, 3, 3)]
    public void CreatesTile_PositiveSize_SuccessfullyCreatesTile(float posX, float posY, float sizeX, float sizeZ, float sizeY)
    {
        Assert.DoesNotThrow(() => CreateTile(posX, posY, sizeX, sizeY, sizeZ, null));
    }

    [Test]
    public void CreatesGrid_PositiveSize_SuccessfullyCreatesGrid()
    {
        Assert.DoesNotThrow(() => SetupGenericGrid());
    }

    [TestCase(0, 0, 3, 3, 3)]
    public void CreatesStartTile_PositiveSize_SuccessfullyCreatesStartTile(float posX, float posY, float sizeX, float sizeZ, float sizeY)
    {
        GameObject tile = null;
        Assert.DoesNotThrow(() => tile = CreateTile(posX, posY, sizeX, sizeY, sizeZ, null, ETileType.Start));
        Assert.That(tile.GetComponent<Tile>().type == ETileType.Start);
    }

    [TestCase(0, 0, 3, 3, 3)]
    public void CreatesEndTile_PositiveSize_SuccessfullyCreatesStartTile(float posX, float posY, float sizeX, float sizeZ, float sizeY)
    {
        GameObject tile = null;
        Assert.DoesNotThrow(() => tile = CreateTile(posX, posY, sizeX, sizeY, sizeZ, null, ETileType.End));
        Assert.That(tile.GetComponent<Tile>().type == ETileType.End);
    }

    GameObject SetupGenericGrid()
    {
        GameObject grid = new GameObject();
        GameObject tile = CreateTile(1, 1, 1, 1, 1, grid.transform);
        
        grid.AddComponent<Grid>();
        grid.GetComponent<Grid>().tilePrefab = tile.GetComponent<Tile>();
        grid.GetComponent<Grid>().boardWorldSize = new Vector2(6, 10);
        grid.GetComponent<Grid>().tileSize = tile.transform.localScale;

        return grid;
    }
    
    private GameObject CreateTile(float posX, float posY, float sizeX, float sizeY, float sizeZ, Transform parent, ETileType type = ETileType.Base)
    {
        var tile = new GameObject("tile");
        tile.AddComponent<Tile>();

        var text = new GameObject("text");
        text.transform.parent = tile.transform;
        text.AddComponent<TextMeshPro>();

        tile.GetComponent<Tile>().text = text.GetComponent<TextMeshPro>();

        tile.GetComponent<Tile>().Initialize(new Vector2(posX, posY), new Vector3(sizeX, sizeY, sizeZ), parent, 0, type);
        return tile;
    }
}

﻿using System.Collections;
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

    [Test]
    public void TileFactory_CreateTile_ReturnsTile()
    {
        var factoryObj = CreateGameObjectWithComponents("factory", typeof(TileFactory));
        var tilePrefabObj = CreateGameObjectWithComponents("tilePrefab", typeof(Tile));

        var factory = factoryObj.GetComponent<TileFactory>();
        factory.baseTilePrefab = tilePrefabObj.GetComponent<Tile>();
        var tile = factory.CreateTile();

        Assert.That(tile, Is.Not.Null);
    }

    GameObject CreateGameObjectWithComponents(string objectName, params Type[] types)
    {
        GameObject obj = new GameObject(objectName);

        foreach(Type type in types)
        {
            obj.AddComponent(type);
        }
        return obj;
    }

    GameObject SetupGenericGrid()
    {
        var gridObj = CreateGameObjectWithComponents("grid", typeof(Grid));
        var grid = gridObj.GetComponent<Grid>();
        GameObject tile = CreateTile(1, 1, 1, 1, 1, grid.transform);
        
        grid.tilePrefab = tile.GetComponent<Tile>();
        grid.boardWorldSize = new Vector2(6, 10);
        grid.tileSize = tile.transform.localScale;

        return gridObj;
    }
    
    private GameObject CreateTile(float posX, float posY, float sizeX, float sizeY, float sizeZ, Transform parent, ETileType type = ETileType.Base)
    {
        var tileObj = CreateGameObjectWithComponents("tile", typeof(Tile));
        var textObj = CreateGameObjectWithComponents("text", typeof(TextMeshPro));
        
        textObj.transform.parent = tileObj.transform;

        var tile = tileObj.GetComponent<Tile>();
        tile.text = textObj.GetComponent<TextMeshPro>();

       tile.Initialize(new Vector2(posX, posY), new Vector3(sizeX, sizeY, sizeZ), parent, 0, type);
        return tileObj;
    }
}

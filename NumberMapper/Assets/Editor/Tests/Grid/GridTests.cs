using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;
using TMPro;
using System;
using UnityEngine.UI;

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
    public void GeneratesGrid_WithNegativeSizeVariables_FailsToCreateGrid(int x, int y, string expected)
    {
        GameObject grid = SetupGenericGrid();
        var result = Assert.Throws<Exception>(() => grid.GetComponent<Grid>().Generate(x,y));
        Assert.That(expected, Is.EqualTo(result.Message));
    }
    
    [TestCase(0, 0, 3, 3, 3)]
    public void TileFactory_TriesToCreateBaseTileWithPositiveSize_SuccessfullyCreatesBaseTile(float posX, float posY, float sizeX, float sizeZ, float sizeY)
    {
        Assert.DoesNotThrow(() => CreateTile(posX, posY, sizeX, sizeY, sizeZ, null));
    }

    [TestCase(0,0,-3,-3,-3, "Tile's X, Y and Z size must be larger than 0, but now they are -3,-3,-3")]
    public void TileFactoriy_TriesToCreateEndTileWithNegativeSize_FailsToCreateEndTile(float posX, float posY, float sizeX, float sizeZ, float sizeY, string expected)
    {
        var result = Assert.Throws<Exception>(() => CreateTile(posX, posY, sizeX, sizeZ, sizeY, null));
        Assert.That(expected, Is.EqualTo(result.Message));
    }

    [Test]
    public void CreatesGrid_PositiveSize_SuccessfullyCreatesGrid()
    {
        Assert.DoesNotThrow(() => SetupGenericGrid());
    }

    [TestCase(0, 0, 3, 3, 3)]
    public void TileFactory_TriesCreateStartTileWithPositiveSize_SuccessfullyCreatesStartTile(float posX, float posY, float sizeX, float sizeZ, float sizeY)
    {
        GameObject tileObj = null;
        Assert.DoesNotThrow(() => tileObj = CreateTile(posX, posY, sizeX, sizeY, sizeZ, null, ETileType.Start));
        var tile = tileObj.GetComponent<Tile>();

        Assert.That(tile.type == ETileType.Start);
        Assert.That(tile.text.sprite != null);
        Assert.That(tile.Value >= 0 && tile.GetComponent<Tile>().Value <= 9);
    }

    [TestCase(0, 0, 3, 3, 3)]
    public void TileFactory_TriesCreateEndTileWithPositiveSize_SuccessfullyCreatesEndTile(float posX, float posY, float sizeX, float sizeZ, float sizeY)
    {
        GameObject tileObj = null;
        Assert.DoesNotThrow(() => tileObj = CreateTile(posX, posY, sizeX, sizeY, sizeZ, null, ETileType.End));
        var tile = tileObj.GetComponent<Tile>();

        Assert.That(tile.type == ETileType.End);
        Assert.That(tile.text.sprite != null);
        Assert.That(tile.Value >= 0 && tile.Value <= 9);
    }

    [Test]
    public void TileFactory_CreateTile_ReturnsTile()
    {
        var factoryObj = CreateGameObjectWithComponents("factory", typeof(TileFactory));
        var tilePrefabObj = CreateGameObjectWithComponents("tilePrefab", typeof(Tile));

        var factory = factoryObj.GetComponent<TileFactory>();
        factory.baseTilePrefab = tilePrefabObj.GetComponent<Tile>();
        var tile = factory.CreateTile(ETileType.Base);

        Assert.That(tile, Is.Not.Null);
    }

    [TestCase(5, 104, 9)]
    [TestCase(9, 15, 6)]
    [TestCase(0, 10, 0)]
    public void Tile_ClickedMultipleTimes_ScoreIsCorrect(int initialValue, int timesClicked, int expected)
    {
        var tileObj = CreateGameObjectWithComponents("tile", typeof(Tile));
        var tile = tileObj.GetComponent<Tile>();

        for (int i = initialValue; i < timesClicked; i++)
        {
            tile.Clicked();
        }

        Assert.That(tile.Value, Is.EqualTo(expected));
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
        grid.tileFactory = SetupGenericFactory();

        grid.boardWorldSize = new Vector2(6, 10);
        grid.tileSize = grid.tileFactory.baseTilePrefab.transform.localScale;

        return gridObj;
    }
    
    private GameObject CreateTile(float posX, float posY, float sizeX, float sizeY, float sizeZ, Transform parent, ETileType type = ETileType.Base)
    {
        var tileObj = CreateGameObjectWithComponents("tile", typeof(Tile));
        var textObj = CreateGameObjectWithComponents("text", typeof(Image));
        
        textObj.transform.SetParent(tileObj.transform);

        var tile = tileObj.GetComponent<Tile>();
        tile.text = textObj.GetComponent<Image>();
        tile.type = type;

       tile.Initialize(new Vector2(posX, posY), new Vector3(sizeX, sizeY, sizeZ), parent, 0);
        return tileObj;
    }

    TileFactory SetupGenericFactory()
    {
        var baseTile = CreateTile(0, 0, 1, 1, 1, null, ETileType.Base).GetComponent<Tile>();
        var startTile = CreateTile(0, 0, 1, 1, 1, null, ETileType.Start).GetComponent<Tile>();
        var endTile = CreateTile(0, 0, 1, 1, 1, null, ETileType.End).GetComponent<Tile>();
        
        var factoryObj = CreateGameObjectWithComponents("factory", typeof(TileFactory));
        var factory = factoryObj.GetComponent<TileFactory>();

        factory.baseTilePrefab = baseTile;
        factory.startTilePrefab = startTile;
        factory.endTilePrefab = endTile;

        return factory;


    }
}

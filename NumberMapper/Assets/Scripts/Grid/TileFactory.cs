using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour {

    public Tile baseTilePrefab;
    public Tile startTilePrefab;
    public Tile endTilePrefab;

    public Tile CreateTile(ETileType type = ETileType.Base)
    {
        switch (type)
        {
            case ETileType.Base:
                return Instantiate(baseTilePrefab);
            case ETileType.Start:
                return Instantiate(startTilePrefab);
            case ETileType.End:
                return Instantiate(endTilePrefab);
            default:
                return Instantiate(baseTilePrefab);
        }
    }

    public Tile CreateAndInitializeTile(Vector2 worldPosition, Vector3 tileSize, Transform parent, int value = 0, int gridX = 0, int gridY = 0, ETileType type = ETileType.Base)
    {
        var tile = CreateTile(type);
        tile.Initialize(worldPosition, tileSize, parent, value, gridX, gridY);
        return tile;
    }
}

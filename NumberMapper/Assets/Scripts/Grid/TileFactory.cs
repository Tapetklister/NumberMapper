using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour {

    public Tile baseTilePrefab;

    public Tile CreateTile()
    {
        return baseTilePrefab;
    }
}

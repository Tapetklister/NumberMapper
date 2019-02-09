using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Tile : MonoBehaviour
{
    public ETileType type;

    internal void Init(Vector2 worldPosition, Vector2 tileSize, Transform parent)
    {
        transform.parent = parent;
        transform.position = worldPosition;
        transform.localScale = tileSize;
    }
}

public enum ETileType
{
    Base
}
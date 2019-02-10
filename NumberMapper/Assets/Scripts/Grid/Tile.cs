using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public ETileType type;

    public int Value { get; set; }

    public TextMeshPro text;

    List<Tile> connected;

    private void Awake()
    {
        connected = new List<Tile>();
    }

    public void Initialize(Vector2 worldPosition, Vector2 tileSize, Transform parent, int value = 0)
    {
        transform.parent = parent;
        transform.position = worldPosition;
        transform.localScale = tileSize;
        Value = value;


        if (tileSize.x <= 0 || tileSize.y <= 0)
        {
            throw new Exception("Tile's X and Y size must be larger than 0, but now they are " + tileSize.x + "," + tileSize.y);
        }

        if (text != null)
        {
            text.text = value.ToString();
        }
        else
        {
            throw new Exception("Tile's text variable is not set.");
        }
    }
}

public enum ETileType
{
    Base
}
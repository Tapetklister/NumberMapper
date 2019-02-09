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

    public void Init(Vector2 worldPosition, Vector2 tileSize, Transform parent, int value = 0)
    {
        transform.parent = parent;
        transform.position = worldPosition;
        transform.localScale = tileSize;
        Value = value;

        text.text = value.ToString();
    }
}

public enum ETileType
{
    Base
}
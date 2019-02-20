using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    public delegate void EventHandler();
    public event EventHandler clickedEvent;
    public static EventHandler OnStaticClickedEvent;

    public ETileType type;

    public int Value { get; set; }
    public int GridX { get; set; }
    public int GridY { get; set; }

    public Image text;
    public Animator animator;

    List<Tile> connected;

    private void Awake()
    {
        connected = new List<Tile>();
    }

    public void Initialize(Vector2 worldPosition, Vector3 tileSize, Transform parent, int value = 0, int gridX = 0, int gridY = 0)
    {
        if (parent != null)
        {
            transform.parent = parent;
        }
        
        transform.position = worldPosition;
        transform.localScale = tileSize;
        Value = value;
        GridX = gridX;
        GridY = gridY;

        if (tileSize.x <= 0 || tileSize.y <= 0 || tileSize.z <= 0)
        {
            throw new Exception("Tile's X, Y and Z size must be larger than 0, but now they are " + tileSize.x + "," + tileSize.y + "," + tileSize.z);
        }

        if (text != null)
        {
            text.sprite = Resources.Load<Sprite>("Sprites/Number" + Value);
        }
        else
        {
            throw new Exception("Tile's text variable is not set.");
        }
    }

    private void OnMouseDown()
    {
        Clicked();
    }

    public void Clicked()
    {
        Value++;

        if (Value > 9)
        {
            Value = 0;
        }

        if (text != null)
        {
            Invoke("UpdateValueLabel", 0.3f);
        }
        if (animator != null)
        {
            animator.SetTrigger("Clicked");
        }
        if (clickedEvent != null)
        {
            clickedEvent();
        }
        if (OnStaticClickedEvent != null)
        {
            OnStaticClickedEvent();
        }
    }

    void UpdateValueLabel()
    {
        text.sprite = Resources.Load<Sprite>("Sprites/Number" + Value);
    }

    public void SetColors(bool connected)
    {
        text.color = connected ? Color.yellow : Color.white;
    }
}

public enum ETileType
{
    Base,
    Start,
    End
}
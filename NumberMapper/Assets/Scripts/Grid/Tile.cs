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
    public MeshRenderer renderer;
    public Animator animator;

    List<Tile> connected;

    private void Awake()
    {
        connected = new List<Tile>();
    }

    public void Initialize(Vector2 worldPosition, Vector3 tileSize, Transform parent, int value = 0)
    {
        if (parent != null)
        {
            transform.parent = parent;
        }
        
        transform.position = worldPosition;
        transform.localScale = tileSize;
        Value = value;

        if (tileSize.x <= 0 || tileSize.y <= 0 || tileSize.z <= 0)
        {
            throw new Exception("Tile's X, Y and Z size must be larger than 0, but now they are " + tileSize.x + "," + tileSize.y);
        }

        if (text != null)
        {
            text.text = value.ToString();
        }
        else
        {
            throw new Exception("Tile's text variable is not set.");
        }

        SetColor(type);
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
    }

    void UpdateValueLabel()
    {
        text.text = Value.ToString();
    }

    void SetColor(ETileType type)
    {
        if (renderer == null)
        {
            return;
        }

        switch(type)
        {
            case ETileType.Base:
                break;
            case ETileType.Start:
                renderer.material.color = Color.red;
                break;
            case ETileType.End:
                renderer.material.color = Color.green;
                break;
            default:
                break;
        }
    }
}

public enum ETileType
{
    Base,
    Start,
    End
}
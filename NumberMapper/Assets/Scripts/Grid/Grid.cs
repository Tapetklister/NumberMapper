using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

    public int xSize, ySize;

    public Vector2 tileSize;
    public Vector2[] tilePositions;
    public Vector2 screenSize;

    private void Awake()
    {
        SetSize();
        Generate(xSize, ySize);
    }

    private void SetSize()
    {
        screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)) * 2;
        tileSize = new Vector2(screenSize.x / xSize, screenSize.y / ySize);
    }

    public bool Generate(int xSize, int ySize)
    {
        if (xSize <= 0 || ySize <= 0)
        {
            return false;
        }

        tilePositions = new Vector2[(xSize + 1) * (ySize + 1)];
        
        for (int i = 0, x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++, i++)
            {
                tilePositions[i] = GenerateTilePosition(x, y, tileSize, screenSize);
                GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                obj.transform.position = tilePositions[i];
            }
        }

        return true;
    }

    Vector2 GenerateTilePosition(int x, int y, Vector2 tileSize, Vector2 screenSize)
    {
        return new Vector2(tileSize.x * x - screenSize.x * 0.5f + tileSize.x * 0.5f, tileSize.y * y - screenSize.y * 0.5f + tileSize.y * 0.5f);
    }

    private void OnDrawGizmos()
    {
        if (tilePositions == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;
        for(int i = 0; i < tilePositions.Length; i++)
        {
            Gizmos.DrawSphere(tilePositions[i], 0.1f);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    Grid grid;

    [SerializeField]
    MainMenu menu;

    [SerializeField]
    Scorekeeper scorekeeper;

    private void Awake()
    {
        menu.singleplayerButton.onClick.AddListener(StartGame);
        //menu.restartButton.onClick.AddListener(StartGame);
        //menu.mainMenuButton.onClick.AddListener(menu.ShowMainMenu);
        Tile.OnStaticClickedEvent += scorekeeper.IncrementScore;
        Grid.OnEndReached += scorekeeper.SetTextFinish;
    }

    public void StartGame()
    {
        menu.gameObject.SetActive(false);
        grid.gameObject.SetActive(true);
        grid.Generate(grid.xSize, grid.ySize);
    }
}

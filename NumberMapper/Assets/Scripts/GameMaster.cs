using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    [SerializeField]
    Grid grid;

    [SerializeField]
    Menu menu;

    private void Awake()
    {
        menu.singleplayerButton.onClick.AddListener(StartGame);
        menu.restartButton.onClick.AddListener(StartGame);
        menu.mainMenuButton.onClick.AddListener(menu.ShowMainMenu);
        Grid.OnEndReached += menu.ShowFinishMenu;
    }

    void StartGame()
    {
        menu.Hide();
        grid.gameObject.SetActive(true);
        grid.Generate(grid.xSize, grid.ySize);
    }
}

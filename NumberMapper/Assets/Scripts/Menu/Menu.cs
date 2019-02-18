using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EMenuType
{
    Main,
    Finish
}

public class Menu : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject finishCanvas;

    public Button singleplayerButton;
    public Button multiplayerButton;

    public Button restartButton;
    public Button mainMenuButton;

    private void Awake()
    {
        finishCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void Hide()
    {
        finishCanvas.SetActive(false);
        mainMenuCanvas.SetActive(false);
    }

    public void ShowMainMenu()
    {
        finishCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void ShowFinishMenu()
    {
        finishCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
    }

    public void StartGame()
    {

    }
}

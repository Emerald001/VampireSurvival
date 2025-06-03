using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void Btn_Continue()
    {
        // Load Game
        GameManager.Instance.StartGame();
    }

    public void Btn_StartGame()
    {
        GUIManager.Instance.ShowNewGameStartScreen(true);
    }

    public void Btn_Options()
    {
        // Open options menu
        Debug.Log("Options button clicked");
    }

    public void Btn_Credits()
    {
        // Open credits menu
        Debug.Log("Credits button clicked");
    }

    public void Btn_ExitGame()
    {
        Application.Quit();
    }
}

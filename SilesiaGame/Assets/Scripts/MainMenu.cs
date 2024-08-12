using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D interactCursor;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter()
    {
        Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

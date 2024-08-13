using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D interactCursor;

    [SerializeField] private AudioSource _wind;
    [SerializeField] private AudioMixer mixer;
    private AudioManager audioManager;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        audioManager = AudioManager.instance;
        StartCoroutine(windSounds());
        StartCoroutine(Flapping()); 
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
        audioManager._inGame.TransitionTo(1f);

    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator windSounds()
    {
        while (true)
        {
            AudioManager.instance.Play("SmallGust");
            yield return new WaitForSeconds(UnityEngine.Random.Range(4f, 6f));

        }
    }

    IEnumerator Flapping()
    {
        while (true)
        {
            AudioManager.instance.Play("ClothFlapping");
            yield return new WaitForSeconds(33.936f);
        }
    }
}

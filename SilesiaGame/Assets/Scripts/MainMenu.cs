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
    private float flapDelay; 

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
            flapDelay = UnityEngine.Random.Range(4f, 6f);
            yield return new WaitForSeconds(flapDelay);

        }
    }

    IEnumerator Flapping()
    {
        yield return new WaitForSeconds(0.5f); 
        while (true)
        {
            AudioManager.instance.Play("ClothFlap1" /*+ UnityEngine.Random.Range(1, 4)*/); 
            yield return new WaitForSeconds(flapDelay);
        }
    }
}

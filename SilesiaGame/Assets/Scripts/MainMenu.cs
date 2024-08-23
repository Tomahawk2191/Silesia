using DG.Tweening;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D interactCursor;

    private AudioManager audioManager;
    private float flapDelay;
    public static MainMenu instance;
    private GameObject gameObject; 

    public void Awake()
    {
        audioManager = AudioManager.instance;
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
        }
        else instance = this;

    }

    public void Start()
    {
        audioManager.mixer.SetFloat("volume", 5);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        //StartCoroutine(audioManager.PlayMainMenuSounds()); 
        StartCoroutine(windSounds());
        StartCoroutine(Flapping());
    }

    /*public void ReturnToMenu()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
        StartCoroutine(windSounds());
        StartCoroutine(Flapping());
    }*/

    public void OnPointerEnter()
    {
        Cursor.SetCursor(interactCursor, Vector2.zero, CursorMode.Auto);
        audioManager.Play("UIHover"); 
    }

    public void OnPointerExit()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }

    public void PlayGame()
    {
        audioManager.Play("UIClick"); 
        SceneManager.LoadScene(1);
        audioManager._inGame.TransitionTo(2f);
        StopAllCoroutines(); 
    }

    public void QuitGame()
    {
        audioManager.Play("UIClick");
#if UNITY_EDITOR
///
#else
        Application.Quit();
#endif
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
        yield return new WaitForSeconds(0.75f); 
        while (true)
        {
            AudioManager.instance.Play("ClothFlap1" /*+ UnityEngine.Random.Range(1, 4)*/); 
            yield return new WaitForSeconds(flapDelay);
        }
    }
}

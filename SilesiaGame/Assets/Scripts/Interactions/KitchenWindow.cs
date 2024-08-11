using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenWindow : MonoBehaviour
{
    //private static int interactions = 0;
    public static KitchenWindow instance;
    private static bool windowOpen = false; 

    private static Animator _animator;

    private static Vector3 windowPos;
    private AudioManager audioManager;


    private void Start()
    {
        audioManager = AudioManager.instance;
        windowPos = audioManager.GetWindowPos();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void OpenWindow()
    {
        Debug.Log("openWindow");
        _animator.SetTrigger("OpenWindow");
        Progress.instance.OpenWindow();
        StartCoroutine(PlayWindowSoundOnDelay("OpenWindow", 0)); 

    }

    IEnumerator PlayWindowSoundOnDelay(string key, float delay)
    {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.Play(key, windowPos);
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
        }
        else
        {
            instance = this;
            _animator = transform.parent.GetComponent<Animator>();
            windowPos = AudioManager.instance.GetWindowPos();

        }
    }
}

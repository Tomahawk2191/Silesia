using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenWindow : MonoBehaviour
{
    //private static int interactions = 0;
    private static KitchenWindow Instance { get; set; }

    private static Animator _animator;

    private static Vector3 windowPos;
    private AudioManager audioManager;


    private void Start()
    {
        audioManager = AudioManager.instance;
        windowPos = audioManager.GetWindowPos();
        _animator = gameObject.GetComponent<Animator>();
    }

    public static IEnumerator OpenWindow()
    {
        Debug.Log("openWindow");
        _animator.SetTrigger("OpenWindow");
        Progress.instance.OpenWindow();
        yield return new WaitForSeconds(1);
        AudioManager.instance.Play("OpenWindow", windowPos);

    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Two instances of the window");
        }
        else
        {
            Instance = this;
            _animator = transform.parent.GetComponent<Animator>();
            windowPos = AudioManager.instance.GetWindowPos();

        }
    }
}

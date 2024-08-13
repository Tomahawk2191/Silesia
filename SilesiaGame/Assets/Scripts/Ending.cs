using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Ending : MonoBehaviour
{
    private RawImage _rawImage;

    private Object[] _textures;

    [SerializeField] private GameObject _credits;
    [SerializeField] private AudioSource _pigeonSound;
    void Start()
    {
        _rawImage = gameObject.GetComponent<RawImage>();
        _textures = Resources.LoadAll("EndingJPGs",typeof(Texture));
        Debug.Log(_textures.Length);
        StartCoroutine(PlayEnding());

    }

    IEnumerator PlayEnding()
    {
        int frameCount = 0;
        foreach (Texture tex in _textures)
        {
            if (frameCount == 44)
            {
                _pigeonSound.Play();
            }
            _rawImage.texture = tex;
            yield return new WaitForSeconds(1f/15);
            frameCount++;
        }
        
        gameObject.GetComponent<Animator>().SetTrigger("EndOfEnding");
        _credits.GetComponent<Animator>().SetTrigger("EndOfEnding");
        yield return new WaitForSeconds(160);
        SceneManager.LoadSceneAsync(0);
    }
}

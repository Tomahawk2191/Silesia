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
    void Start()
    {
        _rawImage = gameObject.GetComponent<RawImage>();
        _textures = Resources.LoadAll("EndingJPGs",typeof(Texture));
        Debug.Log(_textures.Length);
        StartCoroutine(PlayEnding());

    }

    IEnumerator PlayEnding()
    {

        foreach (Texture tex in _textures)
        {
            _rawImage.texture = tex;
            yield return new WaitForSeconds(1f/9);
        }
        
        gameObject.GetComponent<Animator>().SetTrigger("EndOfEnding");
        _credits.GetComponent<Animator>().SetTrigger("EndOfEnding");
        yield return new WaitForSeconds(150);
        SceneManager.LoadSceneAsync(0);
    }
}

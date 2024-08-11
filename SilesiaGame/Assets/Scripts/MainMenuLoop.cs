using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

using Object = UnityEngine.Object;

public class MainMenuLoop : MonoBehaviour
{
    private RawImage _rawImage;

    private Object[] _textures;

    [SerializeField] private GameObject _credits;
    void Start()
    {
        _rawImage = gameObject.GetComponent<RawImage>();
        _textures = Resources.LoadAll("MainMenuLoopJPGs", typeof(Texture));
        StartCoroutine(PlayMenu());
    }

    IEnumerator PlayMenu()
    {
        while (true)
        {
            foreach (Texture tex in _textures)
            {
                _rawImage.texture = tex;
                yield return new WaitForSeconds(1f / 3);
            }
        }
    }
}

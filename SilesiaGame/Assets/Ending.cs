using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        /*String num = "000";
        for (int i = 1; i <= 44; i++)
        {
            if (i > 0 && i <= 9)
            {
                num = "00" + i;
            }
            else
            {
                num = "0" + i;
            }

            _rawImage.texture = Resources.Load("/EndingJPGs/ezgif-frame-"+num) as Texture;
        }*/

        foreach (Texture tex in _textures)
        {
            _rawImage.texture = tex;
            yield return new WaitForSeconds(1f/9);
        }
        
        gameObject.GetComponent<Animator>().SetTrigger("EndOfEnding");
        _credits.GetComponent<Animator>().SetTrigger("EndOfEnding");
    }
}

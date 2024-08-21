using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class Ending : MonoBehaviour
{
    private RawImage _rawImage;

    private Object[] _textures;

    [SerializeField] public AudioMixer mixer;
    [SerializeField] private GameObject _credits;
    [SerializeField] private AudioSource _pigeonSound;
    void Start()
    {
        mixer.SetFloat("volume", -10f);
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
        yield return new WaitForSeconds(156.5f);
        AudioManager.instance._mainMenu.TransitionTo(3f);
        yield return new WaitForSeconds(3.5f);
        //float volume;
        //mixer.GetFloat("volume", out volume);
        //DOTween.To(() => volume, x => volume = x, 5, 3f).SetEase(Ease.InOutCubic).OnUpdate(() => mixer.SetFloat("volume", volume));
        //yield return new WaitForSeconds(3f); 
        Destroy(AudioManager.instance.transform.gameObject); 
        SceneManager.LoadSceneAsync(0);
        //AudioManager.instance.BackToMenu();
    }
}

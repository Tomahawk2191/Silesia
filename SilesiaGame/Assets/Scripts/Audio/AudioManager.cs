using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // locations for the objects to be instantiated on start
    [SerializeField]
    private Transform Position_Stove;
    [SerializeField]
    private Transform Position_Window;
    [SerializeField]
    private Transform Position_Pigeon;
    [SerializeField]
    private Transform Position_Center; 


    public static AudioManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.mixerGroup;
            s.source.spatialBlend = s.spatialBlend;

        }
    }

    private void Start()
    {
        Play("BackgroundMusic");
        Play("AmbientHum");
        Play("RainLoop1");

        Play("Fire", Position_Stove.position);
        Play("Pigeon", Position_Pigeon.position);

    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));


        s.source.Play();
    }

    /*public void PlayThere(string sound, Vector3 position)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));


        s.source.PlayClipAtPoint(s.source.clip, position); 

    }*/

    public void Play(string sound, Vector3 position)
    {
        GameObject go = new GameObject(sound + "Audio");
        go.transform.position = position;
        //Debug.Log("Created temp Location"); 
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + sound + " not found!");
            return;
        }
        //Debug.Log("Searched for keyword clip"); 
        AudioSource source = (AudioSource)go.AddComponent(typeof(AudioSource));
        source.clip = s.clip;

        source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));
        source.loop = s.loop;
        source.outputAudioMixerGroup = s.mixerGroup;
        source.spatialBlend = 1.0f;
        source.maxDistance = 20;

        source.Play();

        // Note: timeScale > 1 means that game time is accelerated. However, the sounds play at their normal speed,
        // so we need to postpone the point in time, when the sound is stopped.
        // Conversly, when timescale approaches 0, the inaccuracies of float precision mean that it kills the sound early
        // Also when timescale is 0, the object is destroyed immediately.
        // Note: The behaviour here means that when the timescale is 0, GameObjects will pile up until the timescale
        // is taken above 0 again.
        if (!s.loop)
        Destroy(go, s.clip.length * (Time.timeScale < 0.01f ? 0.01f : Time.timeScale));

    }

    /*static public void PlayClipAtPoint(AudioClip clip, Vector3 position, [UnityEngine.Internal.DefaultValue("1.0F")] float volume)
    {
        GameObject go = new GameObject("One shot audio");
        go.transform.position = position;
        AudioSource source = (AudioSource)go.AddComponent(typeof(AudioSource));
        source.clip = clip;
        source.spatialBlend = 1.0f;
        source.volume = volume;
        source.Play();

        // Note: timeScale > 1 means that game time is accelerated. However, the sounds play at their normal speed,
        // so we need to postpone the point in time, when the sound is stopped.
        // Conversly, when timescale approaches 0, the inaccuracies of float precision mean that it kills the sound early
        // Also when timescale is 0, the object is destroyed immediately.
        // Note: The behaviour here means that when the timescale is 0, GameObjects will pile up until the timescale
        // is taken above 0 again.
        Destroy(go, clip.length * (Time.timeScale < 0.01f ? 0.01f : Time.timeScale));
    }*/

}

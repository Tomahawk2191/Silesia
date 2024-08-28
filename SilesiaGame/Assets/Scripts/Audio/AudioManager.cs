using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    // locations for the objects to be instantiated on start
    [SerializeField]
    private Transform position_Stove;
    [SerializeField]
    private Transform position_Window;
    [SerializeField]
    private Transform position_Pigeon;
    [SerializeField]
    private Transform position_BedDoor;
    [SerializeField]
    private Transform position_LivingDoor;
    [SerializeField]
    private Transform position_TV;
    [SerializeField]
    private Transform position_Center;


    public static AudioManager instance;

    public Sound[] sounds;

    [SerializeField] public AudioMixer mixer;
    [SerializeField] public AudioMixerSnapshot _mainMenu;
    [SerializeField] public AudioMixerSnapshot _inGame;
    [SerializeField] public AudioMixerSnapshot _pause;
    [SerializeField] public AudioMixerSnapshot _endLetter;
    [SerializeField] public AudioMixerSnapshot _endCredits;

    private float flapDelay;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);

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
        /* ALL OF THESE ARE MOVED INTO Progress.PlayIntroLetter() SO THAT THEY DON"T RUN UNTIL THE MAIN GAME SCENE LOADS
        Play("AmbientHum");
        Play("RainLoop1");

        Play("Fire", position_Stove.position);
        Play("PigeonCoo", position_Pigeon.position);
        Play("TVStatic", position_TV.position);
        */

    }

    /*public IEnumerator PlayMainMenuSounds()
    {
        while (true)
        {
            Play("SmallGust");
            flapDelay = UnityEngine.Random.Range(3.25f, 5.25f);
            yield return new WaitForSeconds(0.75f);
            Play("ClothFlap1" /*+ UnityEngine.Random.Range(1, 4));
            yield return new WaitForSeconds(flapDelay);
        }

    }*/

    public void PlayMainSceneSounds()
    {
        Play("AmbientHum");
        Play("RainLoop1");

        Play("Fire", position_Stove.position);
        Play("PigeonCoo", position_Pigeon.position);
        Play("TVStatic", position_TV.position);
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

    /*public void BackToMenu()
    {
        StartCoroutine(ReturnToMenu());
    }

    IEnumerator ReturnToMenu()
    {
        yield return new WaitForSeconds(3f);
        MainMenu.instance.ReturnToMenu();
    }*/


    // GETTERS AND SETTERS

    public Vector3 GetBedDoorPos()
    {
        return position_BedDoor.position;
    }

    public Vector3 GetLivingDoorPos()
    {
        return position_LivingDoor.position;
    }

    public Vector3 GetWindowPos()
    {
        return position_Window.position;
    }


}

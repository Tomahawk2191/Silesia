using System.Collections;
using UnityEngine;

public class BedroomDoor : MonoBehaviour
{
    private static int interactions = 0;
    private static BedroomDoor Instance { get; set; }

    private static Animator _animator;
    private static bool bedDoorOpen = false;

    private static Vector3 bedDoorPos;
    private static Vector3 livingDoorPos;
    private AudioManager audioManager;
    public static BedroomDoor instance;


    private void Start()
    {
        audioManager = AudioManager.instance;
        bedDoorPos = audioManager.GetBedDoorPos();
        livingDoorPos = audioManager.GetLivingDoorPos();
        _animator = gameObject.GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        Debug.Log("openThedoor");
        bedDoorOpen = true;
        StartCoroutine(PlayDoorSoundOnDelay("DoorOpen", 0.5f));
        Destroy(BedDoorLock.instance); 

    }

    IEnumerator PlayDoorSoundOnDelay(string key, float delay)
    {
        AudioManager.instance.Play(key, bedDoorPos);
        yield return new WaitForSeconds(delay);
        _animator.SetTrigger("OpenDoor");
        
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
            bedDoorPos = AudioManager.instance.GetBedDoorPos();
            livingDoorPos = AudioManager.instance.GetLivingDoorPos();
        }
    }

}

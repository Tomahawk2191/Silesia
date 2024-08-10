using System;
using UnityEngine;

public class LivingroomDoor : MonoBehaviour
{
    private static int interactions = 0;
    private static LivingroomDoor Instance { get; set; }

    private static Animator _animator;
    public static bool bedDoorOpen = false;

    private static Vector3 bedDoorPos;
    private static Vector3 livingDoorPos;
    private AudioManager audioManager;

    [SerializeField] private GameObject player;


    private void Start()
    {
        audioManager = AudioManager.instance;
        bedDoorPos = audioManager.GetBedDoorPos();
        livingDoorPos = audioManager.GetLivingDoorPos();
        _animator = gameObject.GetComponent<Animator>();
    }

    public static void OpenDoor()
    {
        Debug.Log("openThedoor");
        _animator.SetTrigger("OpenDoor");
        //PlayDoorSound();
        AudioManager.instance.Play("DoorOpen", livingDoorPos);

    }

    /*
    private void Update()
    {
        if (bedDoorOpen)
        {
            Debug.Log(" rarara " + player.transform.position + "    " + transform.position + "    " + Vector3.Distance(player.transform.position, transform.position));
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                bedDoorOpen = false;
                OpenDoor();
            }
        }
    }



    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("Two instances of the door");
        }
        else
        {
            Instance = this;
            _animator = transform.parent.GetComponent<Animator>();
            bedDoorPos = AudioManager.instance.GetBedDoorPos();
            livingDoorPos = AudioManager.instance.GetLivingDoorPos();
        }
    }*/
}

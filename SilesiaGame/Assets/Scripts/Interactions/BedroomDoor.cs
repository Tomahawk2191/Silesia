using UnityEngine;

public class BedroomDoor : MonoBehaviour
{
    private static int interactions = 0;
    private static BedroomDoor Instance { get; set; }

    private static Animator _animator;
    private static bool bedDoorOpen = false;

    //[SerializeField] private GameObject _player;
    //[SerializeField] private GameObject _bedroomDoor;
    //private AudioManager audioManager = FindObjectOfType<AudioManager>();

    private static Vector3 bedDoorPos = AudioManager.instance.GetBedDoorPos();
    private static Vector3 livingDoorPos = AudioManager.instance.GetLivingDoorPos();

    [SerializeField] private Transform position_LivingDoor;



    public static void OpenDoor()
    {
        Debug.Log("openThedoor");
        _animator.SetTrigger("OpenDoor");
        //PlayDoorSound();
        if (!bedDoorOpen)
        {
            AudioManager.instance.Play("DoorOpen", bedDoorPos);
            bedDoorOpen = true;
        }
        AudioManager.instance.Play("DoorOpen", livingDoorPos);

    }

    public static void newInteraction()
    {
        interactions++;
        if (interactions == 1)
        {
            Debug.Log("openThedoor");
            _animator.SetTrigger("OpenDoor");
        }
    }

    private void Update()
    {
        /*if (Vector3.Distance(_bedroomDoor.transform.position, _player.transform.position) < 5f)
        {
            newInteraction();
        }*/
    }

    /*private void Awake()
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

    private void PlayDoorSound()
    {

    }
}

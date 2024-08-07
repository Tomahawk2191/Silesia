using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    private static int interactions = 0;
    private static DoorOpeningScript Instance { get; set; }

    private static Animator _animator;
    private static bool bedDoorOpen = false;

    //[SerializeField] private GameObject _player;
    //[SerializeField] private GameObject _bedroomDoor;
    //private AudioManager audioManager = FindObjectOfType<AudioManager>();

    private Vector3 bedDoorPos = FindObjectOfType<AudioManager>().GetBedDoorPos();

    private Vector3 livingDoorPos = FindAnyObjectByType<AudioManager>().GetLivingDoorPos();


    [SerializeField] private Transform position_LivingDoor;



    public void OpenDoor()
    {
        Debug.Log("openThedoor");
        _animator.SetTrigger("OpenDoor");
        //PlayDoorSound();
        if (!bedDoorOpen)
        {
            FindObjectOfType<AudioManager>().Play("DoorOpen", bedDoorPos);
            bedDoorOpen = true;
        }
        FindObjectOfType<AudioManager>().Play("DoorOpen", livingDoorPos);

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
        }
    }

    private void PlayDoorSound()
    {

    }
}

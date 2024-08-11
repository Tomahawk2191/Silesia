using System.Collections;
using UnityEngine;

public class LivingroomDoor : MonoBehaviour
{
    private static int interactions = 0;
    private static LivingroomDoor Instance { get; set; }
    public static LivingroomDoor instance; 

    private static Animator _animator;
    public static bool bedDoorOpen = false;

    private static Vector3 bedDoorPos;
    private static Vector3 livingDoorPos;
    private AudioManager audioManager;

    [SerializeField] private GameObject player;

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
        StartCoroutine(PlayDoorSoundOnDelay("DoorOpen", 0.5f));

    }

    IEnumerator PlayDoorSoundOnDelay(string key, float delay)
    {
        AudioManager.instance.Play(key, livingDoorPos);
        yield return new WaitForSeconds(delay);
        _animator.SetTrigger("OpenDoor");

    }


    private void Update()
    {
        if (bedDoorOpen)
        {
            //Debug.Log(" rarara " + player.transform.position + "    " + transform.position + "    " + Vector3.Distance(player.transform.position, transform.position));
            if (Vector3.Distance(player.transform.position, transform.position) < 5)
            {
                bedDoorOpen = false;
                Popup.Instance.KeyPopup();
                OpenDoor();
            }
        }
    }

}

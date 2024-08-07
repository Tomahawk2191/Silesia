using UnityEngine;
using UnityEngine.Audio;

public class Progress : MonoBehaviour
{
    // Progress Tracking vals
    [SerializeField] // comment out later, just visible for testing
    private float itemsCollected = 0f;
    [SerializeField] // comment out later, just visible for testing
    private float percentcomplete = 0f;
    [SerializeField] // comment out later, just visible for testing
    private float totalItems;
    [SerializeField] public AudioMixer mixer;
    private bool windowOpen;

    [SerializeField] float numKitchen;
    [SerializeField] float numBedroom;
    [SerializeField] float numLivingRoom;

    // Stored vals for calling other things
    private AudioManager audioManager;
    private Vector3 windowPos;

    public static Progress instance;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

    }

    private void Start()
    {
        totalItems = GameObject.FindGameObjectsWithTag("Artifact").Length;
        itemsCollected = 0f;
        percentcomplete = 0f;
        windowOpen = false;
        audioManager = AudioManager.instance; 
        windowPos = audioManager.GetWindowPos();
    }

    public void Increment()
    {
        itemsCollected = itemsCollected + 1f;
        if (totalItems == 0f)
        {
            Debug.LogWarning("No Artifacts in Scene");
            return;
        }
        percentcomplete = Mathf.Floor(100 * itemsCollected / totalItems);
        PigeonVol();

        // CONDITION TRIGGERS AT CERTAIN VALUES
        if (itemsCollected == numKitchen - 2f && windowOpen)
            audioManager.Play("SmallGust", windowPos);

        if (itemsCollected == numKitchen - 1f && windowOpen)
            audioManager.Play("MedGust", windowPos);


        // bedroom door opening
        if (itemsCollected == numKitchen)
        {
            Debug.Log("Finished kitchen"); 
            audioManager.Play("BigGust", windowPos);
            Debug.Log("Played BigGust"); 
            new WaitForSeconds(5f); // SET BACK TO A REASONABLE VALUE< THIS IS JUST FOR NOW
            Debug.Log("Calling OpenDoor"); 
            BedroomDoor.OpenDoor(); /* INSERT DOOR OPEN TRIGER*/
            Debug.Log("Called OpenDoor"); 
        }
        // living room door opening
        if (itemsCollected == numKitchen + numBedroom) { /* INSERT DOOR OPEN TRIGER*/}

        // release pigeons
        if (itemsCollected == totalItems) { /* INSERT DOOR OPEN TRIGER*/}
    }
    // set pigeons volume relative to game progress
    private void PigeonVol()
    {
        mixer.SetFloat("pigeonVol", (percentcomplete / 100 * 15) - 15);
    }

    // GETTERS AND SETTERS
    public void OpenWindow()
    {
        windowOpen = true;
    }

    public float GetPercent()
    {
        return percentcomplete;
    }

    public float GetAbsolute()
    {
        return itemsCollected;
    }



}

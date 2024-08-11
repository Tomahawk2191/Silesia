using System;
using UnityEditor;
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
    [SerializeField] float incompleteCushion = 2f;  

    [SerializeField] float numKitchen;
    [SerializeField] float numBedroom;
    [SerializeField] float numLivingRoom;

    [SerializeField] private GameObject windowKitchen;

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
        itemsCollected += 1f;
        if (totalItems == 0f)
        {
            Debug.LogWarning("No Artifacts in Scene");
            return;
        }
        percentcomplete = Mathf.Floor(100 * itemsCollected / totalItems);
        PigeonVol();

        // CONDITION TRIGGERS AT CERTAIN VALUES
        if (itemsCollected <= numKitchen - (2f + incompleteCushion) && windowOpen)
            audioManager.Play("SmallGust", windowPos);

        if (itemsCollected == numKitchen - (1f + incompleteCushion) && windowOpen)
            audioManager.Play("MidGust", windowPos);


        // bedroom door opening
        if (itemsCollected == numKitchen - incompleteCushion)
        {
            if (!windowOpen) KitchenWindow.instance.OpenWindow(); 
            Debug.Log("Finished kitchen"); 
            audioManager.Play("BigGust", windowPos);
            Debug.Log("Played BigGust"); 
            Debug.Log("Calling OpenDoor"); 
            BedroomDoor.instance.OpenDoor(); /* INSERT DOOR OPEN TRIGER*/
            //windowKitchen.GetComponent<Animator>().SetTrigger("OpenWindow");

            Debug.Log("Called OpenDoor"); 
        }
        /*// living room door opening
        if (itemsCollected == numKitchen + numBedroom) 
        {
            Debug.Log("Finished Bedroom"); 
            //audioManager.Play("BigGust", windowPos);
            //Debug.Log("Played BigGust"); 
            //new WaitForSeconds(5f); // SET BACK TO A REASONABLE VALUE< THIS IS JUST FOR NOW
            Debug.Log("Calling OpenDoor"); 
            LivingroomDoor.OpenDoor(); /* INSERT DOOR OPEN TRIGER#1#
            Debug.Log("Called OpenDoor"); 
        }*/

        // release pigeons
        if (itemsCollected >= totalItems) 
        {
            GameObject.Find("CAGE").GetComponentInChildren<BasicBig>().setAbleToUse(true);
            LetterScript.instance.playOutroLetter();
        }

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

    public bool GetWindowStatus()
    {
        return windowOpen;
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

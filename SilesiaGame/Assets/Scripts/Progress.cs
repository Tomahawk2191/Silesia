using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Progress : MonoBehaviour
{
    [SerializeField] 
    private float itemsCollected = 0f;
    [SerializeField]
    private float percentcomplete = 0f;
    [SerializeField]
    private float totalItems;
    [SerializeField] public AudioMixer mixer;

    [SerializeField] float numKitchen;
    [SerializeField] float numBedroom;
    [SerializeField] float numLivingRoom;

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

        // bedroom door opening
        if (itemsCollected == numKitchen) BedroomDoor.OpenDoor(); /* INSERT DOOR OPEN TRIGER*/

        // living room door opening
        if (itemsCollected == numKitchen + numBedroom) { /* INSERT DOOR OPEN TRIGER*/}

        // release pigeons
        if (itemsCollected == totalItems - 1) { /* INSERT DOOR OPEN TRIGER*/}
    }

    public float GetPercent()
    {
        return percentcomplete;
    }

    public float GetAbsolute()
    {
        return itemsCollected; 
    }

    private void PigeonVol()
    {
        mixer.SetFloat("pigeonVol", (percentcomplete / 100 * 15) -15); 
    }




}

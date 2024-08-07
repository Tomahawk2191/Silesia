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
    }

    public float GetPercent()
    {
        return percentcomplete;
    }

    public float GetAbsolute()
    {
        return itemsCollected; 
    }

    private void Update()
    {
        PigeonVol(); 
    }

    private void PigeonVol()
    {
        mixer.SetFloat("pigeonVol", (percentcomplete / 100 * 15) -15); 
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Play("PigeonCoo",
            gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

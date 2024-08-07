using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("PigeonCoo", gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class billboardScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //this is a very complex and advanced script
    void Update()
    {
        transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));
    }
}

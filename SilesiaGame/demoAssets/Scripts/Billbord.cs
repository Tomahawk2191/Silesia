using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billbord : MonoBehaviour
{
    [SerializeField]
    private float size;

    void Update()
    {
        var target = Camera.main.transform;

        transform.LookAt(target, Vector3.up);
        var scale = Vector3.Distance(target.position, transform.position);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}

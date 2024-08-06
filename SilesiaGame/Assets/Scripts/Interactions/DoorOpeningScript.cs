using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DoorOpeningScript : MonoBehaviour
{
    private static int interactions = 0;
    private static DoorOpeningScript Instance { get; set; }

    private static Animator _animator;

    [SerializeField] private GameObject _player;

    [SerializeField] private GameObject _bedroomDoor;
    

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
        if (Vector3.Distance(_bedroomDoor.transform.position, _player.transform.position) < 5f)
        {
            newInteraction();
        }
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
}

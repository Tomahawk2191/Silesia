using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public static PlayerInput input;
    [SerializeField] private float distance = 8f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    Rigidbody rb;

    public static PlayerInteract Instance { get; set; }

    public static Interactable selectedInteractable;
    public event EventHandler<OnSelectedArtefactChangedEventArgs> OnSelectedArtefactChanged;

    public Transform holdPt;


    public class OnSelectedArtefactChangedEventArgs : EventArgs
    {
        public Interactable selectedArtefact;
    }
    void Awake()
    {
        if (Instance != null)
            Debug.LogError("There are two players???");
        Instance = this;
        input = new PlayerInput();
    }
    void Start()
    {
        playerUI = PlayerUI.Instance;
        input.OnInteraction += GameInput_OnInteraction;
        rb = GetComponent<Rigidbody>();
    }


    // method that is used for interactions every time the PlayerInput triggers the event;
    //the selected interactable object is set in the update
    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact(this);
        }
    }


    // Update is called once per frame 
    // using the raycast it check if there is an interactable object in front of the camera and sets it as the selected interactable
    // also triggers event for each interactable object to check if it is the one looked at, if so -> turns on the outline
    void Update()
    {

        //playerUI.UpdateText(String.Empty);
        playerUI.ShowNormalCursor();

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            var facedInteractable = hitInfo.collider.GetComponent<Interactable>();
            if (facedInteractable != null && Vector3.Distance(facedInteractable.transform.position, rb.position) < 100)
            {
                //playerUI.UpdateText("Press LMB to interact");
                playerUI.ShowInteractCursor();
                if (facedInteractable != selectedInteractable)
                {
                    SetSelectedArtefact(facedInteractable);
                }
                
            }
        }
        else
        {
            SetSelectedArtefact(null);
        }
        
        

    }


    private void SetSelectedArtefact(Interactable artefact)
    {
        selectedInteractable = artefact;
        OnSelectedArtefactChanged?.Invoke(this, new OnSelectedArtefactChangedEventArgs()
        {
            selectedArtefact = artefact
        });
    }

}
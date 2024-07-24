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

    public class OnSelectedArtefactChangedEventArgs : EventArgs
    {
        public Interactable selectedArtefact;
    }
    // Start is called before the first frame update
    //input = PlayerInput, made with InputSystem package
    // += stands for listening to the event triggered by PlayerInput,
    //once it is triggered the GameInput_OnInteraction is executed
    //playerUI - test thing, replace with onHover system later
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


    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }


    // Update is called once per frame 
    void Update()
    {

        playerUI.UpdateText(String.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            var facedInteractable = hitInfo.collider.GetComponent<Interactable>();
            if (facedInteractable != null && Vector3.Distance(facedInteractable.transform.position, rb.position) < 100)
            {
                playerUI.UpdateText("Press E to interact");
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
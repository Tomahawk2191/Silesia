using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerMovementInteract : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;

    public float groundDrag; 

    
    public Transform orientation;

    float horizontalInput; 
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    [SerializeField] private Camera cam;
    public static PlayerInput input;
    [SerializeField] private float distance = 8f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;

    public static PlayerMovementInteract Instance { get; set; }

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
        if(Instance != null)
            Debug.LogError("There are two players???");
        Instance = this;
        input = GetComponent<PlayerInput>();
    }
    void Start()
    {
        playerUI = GetComponent<PlayerUI>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        input.OnInteraction += GameInput_OnInteraction;
        playerUI = GetComponent<PlayerUI>();
    }
    

    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        if (selectedInteractable != null)
        {
            selectedInteractable.Interact();
        }
    }

    // FixedUpdate handles all physics-related movement
    private void FixedUpdate()
    {
        MovePlayer();
    }

    private Interactable getSelectedInteractable()
    {
        return selectedInteractable;
    }


    // Update is called once per frame
    //lines 74-83 for hovering
    void Update()
    {
        // ground check 
        playerUI.UpdateText(String.Empty);
        MyInput();
        // handle drag
        rb.drag = groundDrag;
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            var facedInteractable = hitInfo.collider.GetComponentInParent<Interactable>();
            if (facedInteractable != null && Vector3.Distance(facedInteractable.transform.position, rb.position)<100)
            {
                Debug.Log("found it");
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

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical"); 
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); 
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


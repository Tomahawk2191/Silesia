using System;
using UnityEngine;

public class PlayerMovementInteract : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed = 6f;

    [SerializeField]
    private float groundDrag = 5f;

    [SerializeField]
    private Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;

    [SerializeField] private Camera cam;
    [SerializeField]
    private PlayerInput input;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;

    private Interactable facedInteractable;
    // Start is called before the first frame update
    //input = PlayerInput, made with InputSystem package
    // += stands for listening to the event triggered by PlayerInput,
    //once it is triggered the GameInput_OnInteraction is executed
    //playerUI - test thing, replace with onHover system later
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        input.OnInteraction += GameInput_OnInteraction;
        playerUI = GetComponent<PlayerUI>();
    }

    private void GameInput_OnInteraction(object sender, EventArgs e)
    {
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            facedInteractable = hitInfo.collider.GetComponent<Interactable>();
            if (facedInteractable != null)
            {
                facedInteractable.Interact();
            }
        }
    }

    // FixedUpdate handles all physics-related movement
    private void FixedUpdate()
    {
        MovePlayer();
    }


    // Update is called once per frame
    //lines 74-83 for hovering
    void Update()
    {
        // ground check 

        MyInput();
        // handle drag
        rb.drag = groundDrag;


        playerUI.UpdateText(String.Empty);

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            facedInteractable = hitInfo.collider.GetComponent<Interactable>();
            if (facedInteractable != null)
            {
                playerUI.UpdateText("Press E to interact");
            }
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
        //                                      pos fwd                               pos right


        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

}


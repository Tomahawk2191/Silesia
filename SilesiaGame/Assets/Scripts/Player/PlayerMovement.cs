using UnityEngine;

public class PlayerMovement : MonoBehaviour
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


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        rb.drag = groundDrag;

    }

    // FixedUpdate handles all physics-related movement
    private void FixedUpdate()
    {
        MovePlayer();
    }


    // Update is called once per frame
    void Update()
    {

        MyInput();

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

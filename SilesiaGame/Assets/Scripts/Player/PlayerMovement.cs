using System;
using Unity.Mathematics;
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
    private static bool canMove = true;
    
    // The Oscillator to create slight lurching in the movement
    [SerializeField] float oscillationVal = 0f;
    [SerializeField] float oscillationFreq = 8f; 
    float lurchVal = 0f;
    [SerializeField] float lurchStrength = 0.05f; 


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
        
        // update the oscillation value
        oscillationVal = MathF.Sin(oscillationFreq * Time.time);
        lurchVal = oscillationVal * moveSpeed /* + UnityEngine.Random.Range(-1f, 1f)*/  * lurchStrength ;

        MyInput();

    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {

        if (canMove)
        {
            // calculate movement direction
            moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
            //                                      pos fwd                               pos right

            rb.AddForce(moveDirection.normalized * (moveSpeed + lurchVal) * 10f, ForceMode.Force); 
        }
    }

    public static void setCanMove(bool value)
    {
        canMove = value;
    }

    public static bool getCanMove()
    {
        return canMove;
    }
}

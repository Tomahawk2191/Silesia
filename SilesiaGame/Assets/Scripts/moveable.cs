using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class moveable : MonoBehaviour
{
    [SerializeField] Transform p1, p2;
    [SerializeField] float cycleLength = 2;
    [SerializeField] Rigidbody playerRb;
    [SerializeField] PlayerCam playerCam;
    bool focused = false;

    [SerializeField] float sensX, sensY = 100;

    Vector3 startPos;
    Vector3 startRot;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        startRot = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {            
            transform.DOMove(p1.position, cycleLength);
            transform.DORotate(p1.eulerAngles, cycleLength);
            FreezeControlls();
            focused = true;
        }
        if (Input.GetMouseButtonDown(1))
        {
            transform.DOMove(startPos, cycleLength);
            transform.DORotate(startRot, cycleLength);
            UnfreezeControlls();
            focused= false;
        }

        if (focused)
        {
            float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
            float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

            transform.Rotate(playerCam.transform.up, -mouseX, Space.World);
            transform.Rotate(playerCam.transform.right, mouseY, Space.World);
        }
    }

    void FreezeControlls()
    {
        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        playerCam.enabled = false;
    }

    void UnfreezeControlls()
    {
        playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        playerCam.enabled = true;
    }
}

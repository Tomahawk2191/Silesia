using System;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public static PlayerInput input;
    [SerializeField] private float distance = 5f;
    [SerializeField] private LayerMask mask;
    private PlayerUI playerUI;
    Rigidbody rb;
    public GameObject objPos;

    public static PlayerInteract Instance { get; set; }

    public static Interactable selectedInteractable;
    public event EventHandler<OnSelectedArtefactChangedEventArgs> OnSelectedArtefactChanged;
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
            selectedInteractable.TriggerDialogue();
            selectedInteractable.Interact();
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

        if (selectedInteractable != null)
        {
            selectedInteractable.gameObject.GetComponent<Outline>().enabled = false;
        }

        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            var facedInteractable = hitInfo.collider.GetComponent<Interactable>();
            if (facedInteractable != null && Vector3.Distance(facedInteractable.transform.position, rb.position) < 100)
            {
                if (facedInteractable.getAbleToUse())
                {
                    playerUI.ShowInteractCursor();
                    if (facedInteractable != selectedInteractable)
                    {
                        SetSelectedArtefact(facedInteractable);
                    }

                    if (facedInteractable.gameObject.GetComponent<Outline>() != null)
                    {
                        facedInteractable.gameObject.GetComponent<Outline>().enabled = true;
                    }
                    else
                    {
                        Outline outline = facedInteractable.gameObject.AddComponent<Outline>();
                        outline.enabled = true;
                        facedInteractable.gameObject.GetComponent<Outline>().OutlineColor = Color.magenta;
                        facedInteractable.gameObject.GetComponent<Outline>().OutlineWidth = 15.0f;
                    }
                }

                if (DialogueManager.currentObject != null)
                {
                    playerUI.HideAllCursors();
                }

            }
            else
            {
                SetSelectedArtefact(null);
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
    public void blockPlayerForDialogue()
    {
        PlayerMovement.setCanMove(false);
        PlayerCam.setCanMoveCamera(false);
    }
    public void unblockPlayerFromDialogue()
    {
        PlayerMovement.setCanMove(true);
        PlayerCam.setCanMoveCamera(true);
    }
}
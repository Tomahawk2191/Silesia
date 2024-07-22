using DG.Tweening;
using System.IO;
using TMPro;
using UnityEngine;

public class DialogueManager: MonoBehaviour
{
    [SerializeField] public TextAsset sourceFile;
    string[] dialogueLines;
    int currentLine;
    [SerializeField] TextMeshProUGUI textPrefab;
    [SerializeField] float fadeDuration;
    [SerializeField] float fadeDistance;
    [SerializeField] Transform textSpawnPoint;
    
    [SerializeField]public bool inDialogue;
    private bool inAnimation;
    private bool shouldEndDialog;

    TextMeshProUGUI gameText;
    //GameObject textObjectIn;

    //TextMeshProUGUI gameTextOut;
    //GameObject textObjectOut;

    void Start()
    {
        InputManager.Instance.OnInteract += NextLine;
    }

    // Update is called once per frame
    void Update()
    {
       /* if (Input.GetMouseButtonDown(1) && !inDialogue)
        {
            InitializeDialogue();
        }*/
    }

    public void NextLine()
    {
        if (!inDialogue || inAnimation)
        {
            return;
        }
        
        inAnimation = true;

        Animate(0f, true);

        if (!shouldEndDialog)
        {
            ShowNextLine();
            Animate(1f, false);
        }
    }

    private void Animate(float fadeTarget, bool old)
    {
        if (gameText == null)
        {
            return;
        }
        var outSeq = DOTween.Sequence();
        outSeq.Insert(0, gameText.DOFade(fadeTarget, fadeDuration).SetEase(Ease.InOutSine));
        var targetY = gameText.transform.position.y;
        var tempMove = gameText.transform.DOMoveY(targetY + fadeDistance, fadeDuration);
        outSeq.Insert(0, tempMove);
        if (old)
        {
            tempMove.SetEase(Ease.OutQuad);
            //Important line for the lambda
            var go = gameText.gameObject;
            outSeq.AppendCallback(() => Destroy(go));
            if (shouldEndDialog)
            {
                outSeq.AppendCallback(() => 
                {
                    this.inDialogue = false;
                    this.shouldEndDialog = false;
                    this.inAnimation = false;
                });
            }
        } else
        {

            tempMove.SetEase(Ease.InOutQuad);
            outSeq.AppendCallback(() => this.inAnimation = false);
        }
        outSeq.Play();
    }

    void ShowNextLine()
    {
        gameText = Instantiate(textPrefab, textSpawnPoint);
        gameText.transform.localPosition = new Vector3(0f, -fadeDistance, 0f);

        if (currentLine < dialogueLines.Length) {
            gameText.text = dialogueLines[currentLine++];
        }
        if (currentLine == dialogueLines.Length)
        {
            shouldEndDialog = true;
        }
    }

    public void InitializeDialogue()
    {
        dialogueLines = sourceFile.text.Split("\n");
        inDialogue = true;
        currentLine = 0;
        //ShowNextLine();
    }

    public void InitializeFirstDialogue()
    {
        dialogueLines = sourceFile.text.Split("\n");
        inDialogue = true;
        currentLine = 0;
        ShowNextLine();
    }
}


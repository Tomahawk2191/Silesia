using System.Collections;
using System.Collections.Generic;
using DefaultNamespace.Interactions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LetterScript : MonoBehaviour
{
    [SerializeField] private Interactable startDialogue;
    [SerializeField] private float waitTimeInIntroSeconds = 18f;
    [SerializeField] private float waitTimeInOutroSeconds = 19f;
    [SerializeField] private float waitTimeOutIntroSeconds = 6f;
    [SerializeField] private float waitTimeOutOutroSeconds = 3f;

    [SerializeField] private float scrollIntroSpeed = 4f;
    [SerializeField] private float scrollOutroSpeed = 5f;

    [SerializeField] private FullScreenPassRendererFeature _renderer;

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    
    [SerializeField] private Material _introMaterial;
    [SerializeField] private Material _outroMaterial;

    [SerializeField] private GameObject _backGround;
    // Start is called before the first frame update
    void Start()
    {
        _renderer.SetActive(false);
        _skinnedMeshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        _skinnedMeshRenderer.material = _introMaterial;
        playIntroLetter();
    }
    
    public void playIntroLetter()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        PlayerInteract playerInteract =
            player.GetComponent<PlayerInteract>();
        player.transform.position = new Vector3(11f, 4f, 45f);
        playerInteract.blockPlayerForDialogue();
        PlayerInteract.input.BlockInputForInteraction();
        GameObject introLetter = GameObject.Find("IntroLetter");

        introLetter.transform.localPosition = new Vector3(0.1f,1.16f,0.64f);
        StartCoroutine(ScrollLetter(introLetter, playerInteract));
    }

    IEnumerator ScrollLetter(GameObject introLetter, PlayerInteract playerInteract)
    {
        AudioManager.instance.Play("IntroLetter"); 
        yield return new WaitForSeconds(waitTimeInIntroSeconds);

        while (introLetter.transform.localPosition.y < 1.9f)
        {
            introLetter.transform.Translate(Vector3.up * Time.deltaTime * scrollIntroSpeed / 100);
            yield return null;
        }

        yield return new WaitForSeconds(waitTimeOutIntroSeconds);
        _renderer.SetActive(true);

        transform.DOLocalMoveZ(3.5f, 1.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Folded");
        AudioManager.instance.Play("LetterFold");
        yield return new WaitForSeconds(1.25f);

        transform.DOLocalMoveY(-1.2f, 1.5f).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(1.5f);
        _skinnedMeshRenderer.enabled = false;

        playerInteract.unblockPlayerFromDialogue();
        PlayerInteract.input.EnableInputForInteraction();
        DialogueManager.Instance.StartDialogue(startDialogue);
    //StartCoroutine(ScrollOutroLetter(introLetter, playerInteract));
    }

    IEnumerator ScrollOutroLetter(GameObject introLetter, PlayerInteract playerInteract)
    {
        playerInteract.blockPlayerForDialogue();
        _skinnedMeshRenderer.enabled = true;
        _skinnedMeshRenderer.material = _outroMaterial;
        
        transform.DOLocalMoveY(1.16f, 1.5f).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(1.5f);
        
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Folded");
        AudioManager.instance.Play("LetterFold"); 
        yield return new WaitForSeconds(1.25f);

        
        transform.DOLocalMoveZ(0.64f, 1.5f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(1.5f);
        
        _renderer.SetActive(false);

        AudioManager.instance.Play("OutroLetter"); 
        yield return new WaitForSeconds(waitTimeInOutroSeconds);
        while (introLetter.transform.localPosition.y < 1.9f)
        {
            introLetter.transform.Translate(Vector3.up * Time.deltaTime * scrollOutroSpeed/100);
            yield return null;
        }
        
        yield return new WaitForSeconds(waitTimeOutOutroSeconds);
        
        _backGround.GetComponent<Animator>().SetTrigger("FadeOut");
        
        yield return new WaitForSeconds(7f);
        
        SceneManager.LoadSceneAsync(2);
        
        
    }

    
}

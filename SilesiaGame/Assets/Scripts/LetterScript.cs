using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LetterScript : MonoBehaviour
{
    [SerializeField]
    private Interactable startDialogue;
    private float waitTimeInIntroSeconds = 7f;
    private float waitTimeInOutroSeconds = 11f;
    private float waitTimeOutIntroSeconds = 3f;
    private float waitTimeOutOutroSeconds = 1f;
    [SerializeField] public AudioMixer mixer;

    [SerializeField] bool introToggle = true;


    //[SerializeField] private float scrollIntroSpeed = 2.9f;
    private float scrollIntroSpeed = 3f;
    private float scrollOutroSpeed = 2.25f;

    [SerializeField] private FullScreenPassRendererFeature _renderer;

    private SkinnedMeshRenderer _skinnedMeshRenderer;

    [SerializeField] private GameObject _cursor;

    [SerializeField] private GameObject _table;

    [SerializeField] private Material _introMaterial;
    [SerializeField] private Material _outroMaterial;

    [SerializeField] private GameObject _backGround;

    [SerializeField] private GameObject screenToDisable;


    public static LetterScript instance;
    // Start is called before the first frame update
    void Start()
    {
        screenToDisable.SetActive(true);
        if (instance != null)
        {
            Destroy(gameObject);
            instance = this;
        }
        else
        {
            instance = this;
        }

        _renderer.SetActive(false);
        _skinnedMeshRenderer = transform.GetChild(0).GetComponent<SkinnedMeshRenderer>();
        _skinnedMeshRenderer.material = _introMaterial;
        if (introToggle)
        {
            playIntroLetter();

        }
        else
        {
            _skinnedMeshRenderer.enabled = false;
            transform.DOLocalMoveY(1.9f, 0.1f);
            transform.DOLocalMoveZ(3.5f, 0.1f);
            transform.DOLocalMoveY(-1.2f, 0.1f);
            transform.GetChild(0).GetComponent<Animator>().SetTrigger("Folded");
            _renderer.SetActive(true);
        }
    }

    public void playIntroLetter()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        PlayerInteract playerInteract = player.GetComponent<PlayerInteract>();
        player.transform.position = new Vector3(11f, 4f, 45f);
        playerInteract.blockPlayerForDialogue();
        _cursor.transform.localScale = Vector3.zero;
        PlayerInteract.input.BlockInputForInteraction();
        GameObject introLetter = GameObject.Find("IntroLetter");

        introLetter.transform.localPosition = new Vector3(0.1f, 1.16f, 0.64f);
        StartCoroutine(ScrollLetter(introLetter, playerInteract));
    }

    public void playOutroLetter()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        PlayerInteract playerInteract = player.GetComponent<PlayerInteract>();
        playerInteract.blockPlayerForDialogue();
        _cursor.transform.localScale = Vector3.zero;

        PlayerInteract.input.BlockInputForInteraction();
        GameObject outroLetter = GameObject.Find("IntroLetter");
        screenToDisable.SetActive(false);

        StartCoroutine(ScrollOutroLetter(outroLetter, playerInteract));
    }

    IEnumerator ScrollLetter(GameObject introLetter, PlayerInteract playerInteract)
    {
        PlayerUI.Instance.HideAllCursors();
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
        Progress.instance.SetAbsolute(-1f);
        yield return new WaitForSeconds(1.5f);
        _skinnedMeshRenderer.enabled = false;

        playerInteract.unblockPlayerFromDialogue();
        _cursor.transform.localScale = Vector3.one;

        PlayerInteract.input.EnableInputForInteraction();
        Popup.Instance.LMBPopup();
        DialogueManager.Instance.StartDialogue(startDialogue);
        //StartCoroutine(ScrollOutroLetter(introLetter, playerInteract));
    }

    IEnumerator ScrollOutroLetter(GameObject outroLetter, PlayerInteract playerInteract)
    {
        playerInteract.blockPlayerForDialogue();
        _skinnedMeshRenderer.enabled = true;
        _skinnedMeshRenderer.material = _outroMaterial;

        Vector3 pos = transform.localPosition;
        Debug.Log(pos);
        pos.z = pos.z - 1.2f;
        transform.localPosition = pos;
        Debug.Log(transform.localPosition);

        transform.parent.Find("FPS Cam").transform.DOLocalRotate(Vector3.zero, 3f).SetEase(Ease.InOutCubic);
        transform.parent.transform.DOLocalMove(new Vector3(3.73534656f, 0.116072178f, 11.2674656f), 3f).SetEase(Ease.InOutCubic);
        transform.parent.transform.parent.Find("Player").transform.DOLocalMove(new Vector3(3.73534656f, 0.116072178f - 0.625f, 11.2674656f), 3f).SetEase(Ease.InOutCubic);
        yield return new WaitForSeconds(2f);
        _table.SetActive(false);
        yield return new WaitForSeconds(1f);


        transform.DOLocalMoveY(1.16f, 1.5f).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(1.5f);

        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Folded");
        AudioManager.instance._endLetter.TransitionTo(1f);
        Debug.Log("Dropped to end letter levels");
        AudioManager.instance.Play("LetterFold");
        yield return new WaitForSeconds(1.25f);

        transform.DOLocalMoveZ(0.64f, 1.5f).SetEase(Ease.InCubic);
        float volume;
        mixer.GetFloat("pigeonVol", out volume);
        DOTween.To(() => volume, x => volume = x, -25f, 1.5f).SetEase(Ease.InOutCubic).OnUpdate(() => mixer.SetFloat("pigeonVol", volume));
        yield return new WaitForSeconds(1.5f);

        _renderer.SetActive(false);
        GameObject.Find("TV_UNWRAPPED").transform.GetChild(2).GetComponent<Renderer>().material = default;

        AudioManager.instance.Play("OutroLetter");
        yield return new WaitForSeconds(waitTimeInOutroSeconds);

        while (outroLetter.transform.localPosition.y < 1.9f)
        {
            outroLetter.transform.Translate(Vector3.up * Time.deltaTime * scrollOutroSpeed / 100);
            yield return null;
        }

        yield return new WaitForSeconds(waitTimeOutOutroSeconds + 5f);

        _backGround.GetComponent<Animator>().SetTrigger("FadeOut");
        AudioManager.instance._endCredits.TransitionTo(7f);
        float pVol;
        mixer.GetFloat("pigeonVol", out pVol);
        DOTween.To(() => pVol, x => pVol = x, -80f, 1.5f).SetEase(Ease.InOutCubic).OnUpdate(() => mixer.SetFloat("pigeonVol", pVol));
        //yield return new WaitForSeconds(1.5f);
        yield return new WaitForSeconds(7f);
        SceneManager.LoadScene(2);
    }
}

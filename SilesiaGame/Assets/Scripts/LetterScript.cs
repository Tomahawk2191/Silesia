using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LetterScript : MonoBehaviour
{
    [SerializeField] private float waitTimeSeconds = 3f;

    [SerializeField] private float scrollSpeed = 5f;

    [SerializeField] private FullScreenPassRendererFeature _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer.SetActive(false);
        playIntroLetter();
    }
    
    public void playIntroLetter()
    {
        GameObject player = GameObject.Find("Player").gameObject;
        PlayerInteract playerInteract =
            player.GetComponent<PlayerInteract>();
        player.transform.position = new Vector3(11f, 4f, 45f);
        playerInteract.blockPlayerForDialogue();
        
        GameObject introLetter = GameObject.Find("IntroLetter");

        introLetter.transform.localPosition = new Vector3(0.1f,1.16f,0.64f);
        StartCoroutine(ScrollLetter(introLetter, playerInteract));

    }

    IEnumerator ScrollLetter(GameObject introLetter, PlayerInteract playerInteract)
    {
        yield return new WaitForSeconds(waitTimeSeconds);
        while (introLetter.transform.localPosition.y < 1.9f)
        {
            introLetter.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed/100);
            yield return null;
        }
        
        yield return new WaitForSeconds(waitTimeSeconds);
        _renderer.SetActive(true);
        
        transform.DOLocalMoveZ(3.5f, 1.5f).SetEase(Ease.OutCubic);
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Folded");
        AudioManager.instance.Play("LetterFold"); 
        yield return new WaitForSeconds(1.25f);
        
        transform.DOLocalMoveY(-1.2f, 1.5f).SetEase(Ease.InOutExpo);
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
        
        playerInteract.unblockPlayerFromDialogue();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterScript : MonoBehaviour
{
    [SerializeField] private float waitTimeSeconds = 3f;

    [SerializeField] private float scrollSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        playIntroLetter();
    }
    
    public void playIntroLetter()
    {
        Debug.Log("oorah");
        GameObject player = GameObject.Find("Player").gameObject;
        PlayerInteract playerInteract =
            player.GetComponent<PlayerInteract>();
        player.transform.position = new Vector3(84.9300003f, 3.61249995f, -2.66000009f);
        playerInteract.blockPlayerForDialogue();
        
        GameObject introLetter = GameObject.Find("IntroLetter");

        introLetter.transform.position = new Vector3(83.3259964f,6.4f,-2.3900001f);
        StartCoroutine(ScrollLetter(introLetter, playerInteract));

    }

    IEnumerator ScrollLetter(GameObject introLetter, PlayerInteract playerInteract)
    {
        yield return new WaitForSeconds(waitTimeSeconds);
        while (introLetter.transform.position.y < 8.4f)
        {
            introLetter.transform.Translate(Vector3.up * Time.deltaTime * scrollSpeed/100);
            yield return null;
        }
        yield return new WaitForSeconds(waitTimeSeconds);
        
        var startPos = transform.position;
        var targetPos = transform.position + new Vector3(-7.3f, 0, 0);
        var distance = Vector3.Distance(startPos, targetPos);
        float velocity = 7f;
        var duration = distance / velocity;

        var timePassed = 0f;
        while(timePassed < duration)
        {
            var factor = timePassed / duration;
            
            transform.position = Vector3.Lerp(startPos, targetPos, EaseInOut(factor));
            yield return null;
            timePassed += Time.deltaTime;
        }
        transform.position = targetPos;
        
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Folded");
        yield return new WaitForSeconds(1.25f);
        startPos = transform.position;
        targetPos = transform.position + new Vector3(0, -10f, 0);
        distance = Vector3.Distance(startPos, targetPos);
        velocity = 10f;
        duration = distance / velocity;

        timePassed = 0f;
        while(timePassed < duration)
        {
            var factor = timePassed / duration;
            
            transform.position = Vector3.Lerp(startPos, targetPos, EaseInOut(factor));
            yield return null;
            timePassed += Time.deltaTime;
        }
        transform.position = targetPos;
        gameObject.SetActive(false);
        playerInteract.unblockPlayerFromDialogue();
    }
    
    public static float EaseIn(float t)
    {
        return t*t*t;
    }
    
    public static float EaseOut(float t)
    {
        return Flip(EaseIn(Flip(t)));
    }
    
    static float Flip(float x)
    {
        return 1 - x;
    }
    
    public static float EaseInOut(float t)
    {
        return Mathf.Lerp(EaseIn(t), EaseOut(t), t);
    }
    
    public void playOutroLetter()
    {
        PlayerInteract playerInteract =
            transform.parent.transform.parent.transform.Find("Player").GetComponent<PlayerInteract>();
        playerInteract.unblockPlayerFromDialogue();
        
    }
}

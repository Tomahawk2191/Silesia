using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class PoiManager : MonoBehaviour
{
    public static PoiManager Instance { get; private set; }
    [SerializeField] DialogueManager dialogueManagerScript;

    private Stack<POI> POIs;

    [SerializeField]
    private POI mainPOI;
    [SerializeField]
    private CinemachineBrain brain;

    private void Start()
    {
        Assert.IsNull(Instance);
        Instance = this;
        POIs = new Stack<POI>();
        InputManager.Instance.OnBack += Pop;
        Push(mainPOI);
    }

    public void Push(POI poi)
    {
        if (brain.IsBlending)
        {
            Debug.Log("Already Moving");
            return;
        }
        if (POIs.Count > 0)
        {
            var prev = POIs.Peek();
            prev.Depart();
        }
        POIs.Push(poi);
        poi.Arrive();
    }

    public void Pop()
    {
        if (brain.IsBlending)
        {
            Debug.Log("Already Moving");
            return;
        }
        if (dialogueManagerScript.inDialogue)
        {
            Debug.Log("In Dialogue");
            return;
        }
        if (POIs.Count > 1)
        {
            POIs.Pop().Depart();
            POIs.Peek().Arrive();
        }
        else
        {
            Debug.Log("Attempting to Pop last POI");
        }
    }
}

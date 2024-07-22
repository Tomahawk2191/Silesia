using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class DialogueManagerScr : MonoBehaviour
{
    public static DialogueManagerScr _instance;

    //singleton bullshit
    public static DialogueManagerScr Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<DialogueManagerScr>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("HelloSingleton");
                    _instance = singletonObject.AddComponent<DialogueManagerScr>();

                    DontDestroyOnLoad(singletonObject);
                }
            }
            return _instance;
        }
    }
    //singleton bullshit
    private void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    StreamReader reader;

    string path;

    bool closed = true;

    public void Start()
    {
        path = getPath();
        loadReader();
    }

    //gets path for a txt dialogue
    public string getPath()
    {
        return "Assets/Resources/Dialogue.txt"; // <- this NEEDS TO BE SOFTCODED!!!!!!        
    }


    public void loadReader()
    {
        closed = false;
        reader = new StreamReader(path);
    }
    public string getLine()
    {
        if (!closed)
        {
            string outStr = reader.ReadLine();
            if (outStr == null)
            {
                reader.Close();
                closed = true;
            }

            return outStr;
        }
        else { return null; }
    }
}

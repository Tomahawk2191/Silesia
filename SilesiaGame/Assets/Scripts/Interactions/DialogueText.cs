using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueText {
    public TextAsset[] originText;
    public Speaker speaker;
    private static Color textColor = Color.magenta;
    private static readonly Color grandfatherColor = Color.yellow;
    private static readonly Color grandsonColor = Color.blue;

    public enum Speaker
    {
        Grandfather, // yellowed and italic
        Grandson // white and straight
    }

    public DialogueText(Speaker speaker, params TextAsset[] originText)
    {
        this.originText = originText;
        this.speaker = speaker;

        switch (speaker)
        {
            case Speaker.Grandfather:
                textColor = grandfatherColor;
                break;
            case Speaker.Grandson:
                textColor = grandsonColor;
                break;
            default:
                Debug.LogError("illegal enum value" + speaker);
                break;
        }
    }

    /**
     * Split sentences in a TextAsset into an array of strings for processing.
     */
    public string[] getSentencesInDialogue(TextAsset text)
    {
        return text.ToString().Split('\n');
    }

    public static Color getTextColor()
    {
        return textColor;
    }
}
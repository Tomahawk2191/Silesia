using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;


//this type of script is a datadump. Here we will store all the information regarding the artefact, like the text, related artefacts, if its enabled at the beginging and so on.
[CreateAssetMenu(fileName = "ArtefactData", menuName = "ScriptableObjects/InteractableObjectData", order = 1)]
public class InteractableSO : ScriptableObject
{
    public bool basicState;
    public bool collectable;
    public int id;
    public bool isBig;
    [SerializeField] public DialogueText text;


    [Serializable]
    public class DialogueText
    {
        [SerializeField]
        public List<ComboSpeakerText> speakerText;

        private static readonly Color grandfatherColor = new Color(242, 212, 166);

        private static readonly Color grandsonColor = Color.blue;

        public enum Speaker
        {
            Grandfather, // yellowed and italic
            Grandson // white and straight
        }

        [Serializable]
        public class ComboSpeakerText
        {
            [SerializeField] public TextAsset dialogueText;
            [SerializeField] public Speaker speaker;

            public Color getTextColor()
            {
                //Debug.Log("color speaker: " + speaker);
                switch (speaker)
                {
                    case Speaker.Grandfather:
                        //Debug.Log("grandfather speaker");
                        return grandfatherColor;
                    case Speaker.Grandson:
                        return grandsonColor;
                    default:
                        Debug.LogError("illegal enum value" + speaker);
                        return Color.clear;
                }
            }

            public String[] getTextAsset()
            {
                return dialogueText.ToString().Split('\n');
            }
        }

        public List<ComboSpeakerText> getComboSpeakerTexts()
        {
            return speakerText;
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        [Header("Grandfather")]
        [SerializeField]
        private static Color grandfatherColor = new Color(0.9490196f, 0.8036447f, 0.3921569f); // TMPro uses 0-1 RGB for some reason, this sucks
        private static FontStyles grandfatherFontStyle = FontStyles.Bold;
        private static FontWeight grandfatherFontWeight = FontWeight.Black;

        [Header("Grandson")]
        [SerializeField]
        private static Color grandsonColor = new Color(0.6690548f, 0.8247591f, 0.9150943f);
        private static FontStyles grandsonFontStyle = FontStyles.Bold;
        private static FontWeight grandsonFontWeight = FontWeight.Medium;

        public enum Speaker
        {
            Grandfather, // yellowed and italic
            Grandson // white and straight
        }

        [Serializable]
        public class ComboSpeakerText
        {
            [SerializeField]
            public TextAsset dialogueText;
            public Speaker speaker;

            public Color getTextColor()
            {
                switch (speaker)
                {
                    case Speaker.Grandfather:
                        return grandfatherColor;
                    case Speaker.Grandson:
                        return grandsonColor;
                    default:
                        Debug.LogError("illegal enum value" + speaker);
                        return Color.clear;
                }
            }

            public FontStyles GetFontStyle()
            {
                switch (speaker)
                {
                    case Speaker.Grandfather:
                        return grandfatherFontStyle;
                    case Speaker.Grandson:
                        return grandsonFontStyle;
                    default:
                        Debug.LogError("illegal enum value: " + speaker);
                        return FontStyles.Normal;
                }
            }

            public FontWeight GetFontWeight()
            {
                switch (speaker)
                {
                    case Speaker.Grandfather:
                        return grandfatherFontWeight;
                    case Speaker.Grandson:
                        return grandsonFontWeight;
                    default:
                        Debug.LogError("illegal enum value: " + speaker);
                        return FontWeight.Regular;
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

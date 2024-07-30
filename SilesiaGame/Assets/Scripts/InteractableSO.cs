using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using JetBrains.Annotations;
using UnityEngine;


//this type of script is a datadump. Here we will store all the information regarding the artefact, like the text, related artefacts, if its enabled at the beginging and so on.
[CreateAssetMenu(fileName = "ArtefactData", menuName = "ScriptableObjects/InteractableObjectData", order = 1)]
public class InteractableSO : ScriptableObject

{
    public TextAsset text;
    public bool basicState;
    public bool collectable;
}

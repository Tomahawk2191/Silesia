using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtefactData", menuName = "ScriptableObjects/InteractableObjectData", order = 1)]
public class InteractableSO : ScriptableObject

{
    public TextAsset text;
    public bool basicState;
}

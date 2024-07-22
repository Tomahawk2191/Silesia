using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a base class for scripts that will be ran after interaction that needs to change other objects in game.
//those are actions like opening doors, enabling objects, spawning new objects
public abstract class Interaction : ScriptableObject
{
    public abstract void interact();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalObjectInteractable : Interactable
{
    // Start is called before the first frame update
    public override void Interact()
    {
        JournalManager.openedJournal = true;
        JournalManager.currentlyJournal = true;
    }
}

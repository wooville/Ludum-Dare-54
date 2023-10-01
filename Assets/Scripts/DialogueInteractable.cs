using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteractable : Interactable
{
    [SerializeField]
    private DialogueObject _dialogueObject;

    public override void Interact()
    {
        DialogueUI.initiateDialogueDelegate?.Invoke(_dialogueObject);
        // base.Interact();
    }

    public override void ExitInteractArea(Collider2D other){
        base.ExitInteractArea(other);
        DialogueUI.endDialogueDelegate?.Invoke();
    }
}

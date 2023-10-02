using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerInteractable : DialogueInteractable
{
    [SerializeField] private DialogueObject _dialogueObjectBeforeSwap;
    [SerializeField] private DialogueObject _dialogueObjectAfterSwap;
    public delegate void SoulSwapDialogueDelegate();
    public static SoulSwapDialogueDelegate soulSwapDialogueDelegate;
    private bool _swapDialogue = false;

    private void Start() {
        soulSwapDialogueDelegate += SwapDialogue;
    }

    public override void Interact()
    {
        if (_swapDialogue){
            DialogueUI.initiateDialogueDelegate?.Invoke(_dialogueObjectAfterSwap);
        } else {
            DialogueUI.initiateDialogueDelegate?.Invoke(_dialogueObjectBeforeSwap);
        }
    }

    private void SwapDialogue(){
        _swapDialogue = true;
    }
}

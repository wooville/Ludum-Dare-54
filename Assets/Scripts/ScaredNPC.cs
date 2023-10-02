using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredNPC : DialogueInteractable
{
    [SerializeField] Animator animator;

    private void Start() {
        DialogueUI.endDialogueDelegate += HideAnimation;
    }

    public override void Interact()
    {
        base.Interact();
        animator.SetBool("isActive", true);
    }

    private void HideAnimation(){
        animator.SetBool("isActive", false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaredNPC : DialogueInteractable
{
    [SerializeField] Animator animator;
    public override void EnterInteractArea(Collider2D other){
        base.ExitInteractArea(other);
        animator.SetBool("isActive", true);
    }

    public override void ExitInteractArea(Collider2D other){
        base.ExitInteractArea(other);
        animator.SetBool("isActive", false);
        Debug.Log("test");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingGate : Interactable
{
    [SerializeField] private EndGameUI.ENDINGS _ending;
    [SerializeField] private DialogueObject _endingDialogue;
    private bool unlocked = false;

    private void Start() {
        Player.pickupDelegate += CheckPickupForKey;
    }

    private void CheckPickupForKey(Interaction.PICKUPS pickup){
        if (pickup == Interaction.PICKUPS.KEY1){
            unlocked = true;
        }
    }

    public override void Interact()
    {
        if (unlocked){
            EndGameUI.initiateGameEndDelegate?.Invoke(_endingDialogue, _ending);
        }
        
    }
}

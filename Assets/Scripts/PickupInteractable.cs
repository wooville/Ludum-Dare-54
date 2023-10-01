using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractable : Interactable
{
    [SerializeField] private Interaction.PICKUPS _pickup = Interaction.PICKUPS.NONE;
    [SerializeField] private DialogueObject _pickupInfo;

    public override void Interact()
    {
        Player.pickupDelegate?.Invoke(_pickup);
        PickupInfoUI.initiatePickupDelegate?.Invoke(_pickupInfo);
        base.Interact();
    }
}

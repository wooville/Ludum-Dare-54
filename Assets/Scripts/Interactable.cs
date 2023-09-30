using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private Interaction.PICKUPS pickup = Interaction.PICKUPS.NONE;
    [SerializeField]
    private GameObject interactionIndicator;
    public bool isInteractable {get;set;} = false;
    // protected Label buttonPromptLabel;

    // Start is called before the first frame update
    void Start()
    {
        // get ui component to be toggled
        
    }

    public virtual void Interact()
    {
        if (pickup != Interaction.PICKUPS.NONE){
            Player.pickupDelegate?.Invoke(pickup);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")){
            isInteractable = true;
            interactionIndicator.SetActive(true);
        }
    }
    
    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag.Equals("Player")){
            isInteractable = false;
            interactionIndicator.SetActive(false);
        }
    }
}

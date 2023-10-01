using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject interactionIndicator;
    
    public bool isInteractable {get;set;} = false;

    public virtual void Interact()
    {
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
